using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VTPL_ERP.Controllers;
using ERP.DAL.Abstract;
using VTPL_ERP.Models;
using VTPL_ERP.Util;
using ERP.DAL.Dto;
using Newtonsoft.Json;
using static VTPL_ERP.Util.AppConstants;
using VTPL_ERP.Models.Master;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VTPL_ERP.Api
{
    //[Route("api/[controller]")]
    public class StockTransferController : BaseController
    {
        private readonly IPurchaseEntryService _purchaseEntryData;
        private readonly IERP_CommonService _erpCommonServiceData;
        private readonly IAccountPartyMasterService _accountPartyMasterData;
        private readonly IInventoryMasterService _inventoryMasterData;
        private readonly IOtherChargesMasterService _otherChargesMasterData;
        private readonly IInventoryGSTDetailsService _inventoryGSTDetailsData;
        private readonly ICompanyService _companyData;
        private readonly ITransactionService _transactionData;
        private readonly IStockTransferService _stockTransferData;
        public StockTransferController(IStockTransferService stockTransferData, IPurchaseEntryService purchaseEntryData, IERP_CommonService erpCommonServiceData, IAccountPartyMasterService accountPartyMasterData, IOtherChargesMasterService otherChargesMasterData, IInventoryMasterService inventoryMasterData, IInventoryGSTDetailsService inventoryGSTDetailsData, ICompanyService companyData, ITransactionService transactionData)
        {
            _stockTransferData = stockTransferData;
            _purchaseEntryData = purchaseEntryData;
            _erpCommonServiceData = erpCommonServiceData;
            _accountPartyMasterData = accountPartyMasterData;
            _inventoryMasterData = inventoryMasterData;
            _otherChargesMasterData = otherChargesMasterData;
            _inventoryGSTDetailsData = inventoryGSTDetailsData;
            _companyData = companyData;
            _transactionData = transactionData;
        }
        #region StockTransfer
        [Route("api/StockTransfer/SaveStockTransfer")]
        [HttpPost]
        public async Task<JsonResult> SaveStockTransfer(StockTransferViewModel model)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.StockTransfer, RoleActions.InsertUpdate);
            if (hasRights == true)
            {
                try
                {
                    StockTransferDto dtoObj = new StockTransferDto();
                    retObj.IsError = false;
                    retObj.SuccessMessage = "Record inserted successfully !";
                        
                    StockTransferSourceDto srcData = new StockTransferSourceDto();
                    srcData.Id = model.StockJournalNo;
                    srcData.BranchId = model.StockTransferSourceObj.BranchId;
                    srcData.TotalQty = model.StockTransferSourceObj.TotalQty;
                    srcData.TotalAmount = model.StockTransferSourceObj.TotalAmount;
                    srcData.Narration = model.StockTransferSourceObj.Narration;
                    if (!string.IsNullOrEmpty(model.StockTransferDateString))
                    {
                        srcData.StockTransferDate = new DateTime(Convert.ToInt32(model.StockTransferDateString.Split("/")[2]), Convert.ToInt32(model.StockTransferDateString.Split("/")[1]), Convert.ToInt32(model.StockTransferDateString.Split("/")[0]));
                    }
                    var lastInsertedSrcId = await _stockTransferData.InsertUpdateStockTransferSource(srcData);

                    StockTransferDestinationDto dstData = new StockTransferDestinationDto();
                    dstData.StockTransferId = model.StockJournalNo;
                    dstData.BranchId = model.StockTransferDestinationObj.BranchId;
                    dstData.TotalQty = model.StockTransferDestinationObj.TotalQty;
                    dstData.TotalAmount = model.StockTransferDestinationObj.TotalAmount;
                    dstData.Narration = model.StockTransferDestinationObj.Narration;
                    if (!string.IsNullOrEmpty(model.StockTransferDateString))
                    {
                        dstData.StockTransferDate = new DateTime(Convert.ToInt32(model.StockTransferDateString.Split("/")[2]), Convert.ToInt32(model.StockTransferDateString.Split("/")[1]), Convert.ToInt32(model.StockTransferDateString.Split("/")[0]));
                    }
                    var lastInsertedDstId = await _stockTransferData.InsertUpdateStockTransferDestination(dstData);

                            foreach (StockTransferSourceDetailsViewModel item in model.StockTransferSourceDetails)
                            {
                                var inventoryData = await _inventoryMasterData.GetInventoryByKey(item.InventoryId);
                                StockTransferSourceDetailsDto srcDetailsObj = new StockTransferSourceDetailsDto();
                                 // srcDetailsObj.Id = item.Id;
                                srcDetailsObj.StockTransferId = lastInsertedSrcId;
                                srcDetailsObj.InventoryId = item.InventoryId;
                                srcDetailsObj.Qty = item.Qty;
                                srcDetailsObj.Rate = item.Rate;
                                srcDetailsObj.Amount = item.Amount;
                                srcDetailsObj.SerialNos = item.SerialNos;
                                var lastinsrtedsrcdetailId = await _stockTransferData.InsertUpdateStockTransferSourceDetails(srcDetailsObj);

                                StockMovementDto stockMovementObj = new StockMovementDto();
                                stockMovementObj.InventoryId = item.InventoryId;
                                stockMovementObj.BranchId = model.StockTransferSourceObj.BranchId;
                                stockMovementObj.TranCode = model.StockJournalNo;
                                stockMovementObj.TranId = model.StockJournalNo;
                                stockMovementObj.TranDetailId = 0;
                                stockMovementObj.FYId = 1;
                                stockMovementObj.UnitId = inventoryData.UnitId;
                                stockMovementObj.TranRate = item.Rate;
                                stockMovementObj.TranQty = item.Qty;
                                stockMovementObj.TranAmount = item.Amount;
                                stockMovementObj.TranBook = AppConstants.Tran.StockTransfer;
                                stockMovementObj.TranType = AppConstants.Tran.Credit;
                                stockMovementObj.Created_Date = DateTime.Now;
                                stockMovementObj.Modified_Date = DateTime.Now;
                                stockMovementObj.InsertFrom = "StockTransfer";
                                stockMovementObj.SerialNos = srcDetailsObj.SerialNos.Split(';').ToList();
                                await _transactionData.InsertUpdateStockMovement(stockMovementObj);


                                foreach (var serialNo in srcDetailsObj.SerialNos.Split(';'))
                                {
                                    await _stockTransferData.InsertStockTransferSourceItemSerialNosDetails(srcDetailsObj.StockTransferId, srcDetailsObj.InventoryId, serialNo);
                                }
                            }
                            foreach (StockTransferDestinationDetailsViewModel item in model.StockTransferDestinationDetails)
                            {
                                var inventoryData = await _inventoryMasterData.GetInventoryByKey(item.InventoryId);
                                StockTransferDestinationDetailsDto dstDetailsObj = new StockTransferDestinationDetailsDto();
                                // dstDetailsObj.Id = item.Id;
                                dstDetailsObj.StockTransferId = lastInsertedDstId;
                                dstDetailsObj.InventoryId = item.InventoryId;
                                dstDetailsObj.Qty = item.Qty;
                                dstDetailsObj.Rate = item.Rate;
                                dstDetailsObj.Amount = item.Amount;
                                dstDetailsObj.SerialNos = item.SerialNos;
                                var lastinsrteddstdetailId = await _stockTransferData.InsertUpdateStockTransferDestinationDetails(dstDetailsObj);

                                StockMovementDto stockMovementObj = new StockMovementDto();
                                stockMovementObj.InventoryId = item.InventoryId;
                                stockMovementObj.BranchId = model.StockTransferDestinationObj.BranchId;
                                stockMovementObj.TranCode = model.StockJournalNo;
                                stockMovementObj.TranId = model.StockJournalNo;
                                stockMovementObj.TranDetailId = 0;
                                stockMovementObj.FYId = 1;
                                stockMovementObj.UnitId = inventoryData.UnitId;
                                stockMovementObj.TranRate = item.Rate;
                                stockMovementObj.TranQty = item.Qty;
                                stockMovementObj.TranAmount = item.Amount;
                                stockMovementObj.TranBook = AppConstants.Tran.StockTransfer;
                                stockMovementObj.TranType = AppConstants.Tran.Debit;
                                stockMovementObj.Created_Date = DateTime.Now;
                                stockMovementObj.Modified_Date = DateTime.Now;
                                stockMovementObj.InsertFrom = "StockTransfer";
                                stockMovementObj.SerialNos = dstDetailsObj.SerialNos.Split(';').ToList();
                                await _transactionData.InsertUpdateStockMovement(stockMovementObj);


                                foreach (var serialNo in dstDetailsObj.SerialNos.Split(';'))
                                {
                                    await _stockTransferData.InsertStockTransferDestinationItemSerialNosDetails(dstDetailsObj.StockTransferId, dstDetailsObj.InventoryId, serialNo);
                                }
                            }
                            retObj.ResponseDataList.Add(_erpCommonServiceData.GetIdByTable("StockTransfer"));
                            return Json(retObj);
                }
                catch (Exception ex)
                {
                    retObj.IsError = true;
                    retObj.ErrorMessage = ex.Message;
                    return Json(retObj);
                }
            }
            else
            {
                retObj.IsError = true;
                retObj.ErrorMessage = AppConstants.Messages.Not_Authorized;
                return Json(retObj);
            }
        }
        #endregion
    }
}
