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
   // [Route("api/[controller]")]
    public class PurchaseReturnEntryController : BaseController
    {
        private readonly IPurchaseReturnEntryService _purchaseReturnEntryData;
        private readonly IERP_CommonService _erpCommonServiceData;
        private readonly IAccountPartyMasterService _accountPartyMasterData;
        private readonly IInventoryMasterService _inventoryMasterData;
        private readonly IOtherChargesMasterService _otherChargesMasterData;
        private readonly IInventoryGSTDetailsService _inventoryGSTDetailsData;
        private readonly ICompanyService _companyData;
        private readonly ITransactionService _transactionData;
        public PurchaseReturnEntryController(IPurchaseReturnEntryService purchaseReturnEntryData, IERP_CommonService erpCommonServiceData, IAccountPartyMasterService accountPartyMasterData, IOtherChargesMasterService otherChargesMasterData, IInventoryMasterService inventoryMasterData, IInventoryGSTDetailsService inventoryGSTDetailsData, ICompanyService companyData, ITransactionService transactionData)
        {
            _purchaseReturnEntryData = purchaseReturnEntryData;
            _erpCommonServiceData = erpCommonServiceData;
            _accountPartyMasterData = accountPartyMasterData;
            _inventoryMasterData = inventoryMasterData;
            _otherChargesMasterData = otherChargesMasterData;
            _inventoryGSTDetailsData = inventoryGSTDetailsData;
            _companyData = companyData;
            _transactionData = transactionData;
        }
        #region PurchaseReturnEntry
        [Route("api/PurchaseReturnEntry/GetAllPurchaseReturnEntry")]
        [HttpGet]
        public async Task<JsonResult> GetAllPurchaseReturnEntry()
        {
            var purchaseReturnEntrylist = await _purchaseReturnEntryData.GetAllPurchaseReturnEntry();
            return Json(purchaseReturnEntrylist);
        }
        [Route("api/PurchaseReturnEntry/GetPurchaseReturnEntryByKey")]
        //[HttpGet]
        public async Task<JsonResult> GetPurchaseReturnEntryByKey(string DebitNoteNo)
        {
            PurchaseReturnEntryViewModel model = new PurchaseReturnEntryViewModel();
            model.PurchaseReturnEntryDetailsData = await _purchaseReturnEntryData.GetPurchaseReturnEntryDetailsByDebitNoteNo(DebitNoteNo);
            model.PurchaseReturnEntryOtherChargesDetailsData = await _purchaseReturnEntryData.GetPurchaseReturnEntryOtherChargesDetailsByDebitNoteNo(DebitNoteNo);
            return Json(model);
        }
        [Route("api/PurchaseReturnEntry/PrintPurchaseReturnEntry")]
        //[HttpGet]
        public async Task<ActionResult> PrintPurchaseReturnEntry(string DebitNoteNo)
        {
            PurchaseReturnEntryInfoViewModel model = new PurchaseReturnEntryInfoViewModel();
            model.PurchaseReturnEntryData = await _purchaseReturnEntryData.GetPurchaseReturnEntryByKey(DebitNoteNo);
            model.CompanyData = await _companyData.GetAllCompany();
            model.PurchaseReturnEntryDetails = await _purchaseReturnEntryData.GetPurchaseReturnEntryDetailsByDebitNoteNo(DebitNoteNo);
            model.PurchaseReturnEntryOtherChargesDetails = await _purchaseReturnEntryData.GetPurchaseReturnEntryOtherChargesDetailsByDebitNoteNo(DebitNoteNo);
            var totalAmount = (int)model.PurchaseReturnEntryData.TotalAmount;
            model.TotalAmountinWord = ERP_Utitlity.NumberToWords(totalAmount);
            return View(model);
        }
        [Route("api/PurchaseReturnEntry/SavePurchaseReturnEntry")]
        [HttpPost]
        public async Task<JsonResult> SavePurchaseReturnEntry(PurchaseReturnEntryViewModel model)
        {
            JsonResponseData retObj = new JsonResponseData();
            var sessionEmployee = HttpContext.Session.GetObject<EmployeeMasterDto>(AppConstants.SessionKey.CURRENT_USER);
            var hasRights = IsHasRight(AppPages.PurchaseReturnEntry, RoleActions.InsertUpdate);
            if (hasRights == true)
            {
                try
                {
                    PurchaseReturnEntryDto dtoObj = new PurchaseReturnEntryDto();
                    if (model.PageMode == "New")
                    {
                        dtoObj.Created_Date = DateTime.Now;
                        dtoObj.CreatedBy = sessionEmployee.UserId;
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Record inserted successfully !";
                    }
                    else
                    {
                        List<string> serialIdList = new List<string>();
                        var listSerialNo = await _purchaseReturnEntryData.GetSerialNosByDebitNoteNo(model.DebitNoteNo);
                        listSerialNo.ForEach(j => { serialIdList.Add("'" + j + "'"); });
                        string serialIdData = String.Join(',', serialIdList);
                        var countSerial = await _purchaseReturnEntryData.IsPurchaseReturnCanDelete(serialIdData, model.DebitNoteNo);

                        if(countSerial > 0)
                        {
                            retObj.IsError = true;
                            retObj.ErrorMessage = "You can not update it! Data is in use.";
                            return Json(retObj);
                        }
                        else
                        {
                            dtoObj.Created_Date = model.Created_Date;
                            dtoObj.Modified_Date = DateTime.Now;
                            dtoObj.CreatedBy = model.CreatedBy;
                            dtoObj.ModifiedBy = sessionEmployee.UserId;
                            await _purchaseReturnEntryData.DeletePurchaseReturnEntryItemSerialNosDetailsByDebitNoteNo(model.DebitNoteNo);
                            await _purchaseReturnEntryData.DeletePurchaseReturnEntryOtherChargesDetailsByDebitNoteNo(model.DebitNoteNo);
                            await _purchaseReturnEntryData.DeletePurchaseReturnEntryDetailsByDebitNoteNo(model.DebitNoteNo);
                            retObj.IsError = false;
                            retObj.SuccessMessage = "Record updated successfully !";
                        }
                    }
                    dtoObj.DebitNoteNo = model.DebitNoteNo;
                    if (!string.IsNullOrEmpty(model.PurchaseReturnDateString))
                    {
                        dtoObj.DebitNoteDate = new DateTime(Convert.ToInt32(model.PurchaseReturnDateString.Split("/")[2]), Convert.ToInt32(model.PurchaseReturnDateString.Split("/")[1]), Convert.ToInt32(model.PurchaseReturnDateString.Split("/")[0]));
                    }
                    dtoObj.OriginalPurchaseEntryId = model.OriginalPurchaseEntryId;
                    dtoObj.BranchId = model.BranchId;
                    dtoObj.SupplierId = model.SupplierId;
                    //dtoObj.TotalQty = model.TotalQty;
                    dtoObj.CurrentBalance = model.CurrentBalance;
                    dtoObj.PurchaseAccount = model.PurchaseAccount;
                    dtoObj.Narration = model.Narration;
                    dtoObj.Amount = model.Amount;
                    dtoObj.OtherCharges = model.OtherCharges;
                    dtoObj.DiscAmount = model.DiscAmount;
                    dtoObj.SGST = model.SGST;
                    dtoObj.CGST = model.CGST;
                    dtoObj.TotalAmount = model.TotalAmount;
                    dtoObj.RoundOff = model.RoundOff;

                    dtoObj.TotalQty = model.PurchaseReturnEntryDetails.Sum(x => x.Qty);
                    var lastInsertedId = await _purchaseReturnEntryData.InsertPurchaseReturnEntry(dtoObj);

                    //delete data from stockmovement and stockmovement serialdetail
                    await _transactionData.DeleteStockMovementByTranCode(model.DebitNoteNo);
                    // complete

                    foreach (PurchaseReturnEntryDetailsViewModel item in model.PurchaseReturnEntryDetails)
                    {
                        var inventoryData = await _inventoryMasterData.GetInventoryByKey(item.InventoryId);
                        PurchaseReturnEntryDetailsDto purchaseReturnDetailsObj = new PurchaseReturnEntryDetailsDto();
                        // purchaseDetailsObj.Id = item.Id;
                        purchaseReturnDetailsObj.DebitNoteNo = lastInsertedId;
                        purchaseReturnDetailsObj.InventoryId = item.InventoryId;
                        purchaseReturnDetailsObj.Qty = item.Qty;
                        purchaseReturnDetailsObj.Rate = item.Rate;
                        purchaseReturnDetailsObj.GrossAmt = item.GrossAmt;
                        purchaseReturnDetailsObj.Discount = item.Discount;
                        purchaseReturnDetailsObj.DiscAmt = item.DiscAmt;
                        // purchaseReturnDetailsObj.UnitId = item.UnitId;
                        purchaseReturnDetailsObj.Tax = item.Tax;
                        purchaseReturnDetailsObj.TaxAmt = item.TaxAmt;
                        purchaseReturnDetailsObj.TotalAmount = item.TotalAmount;
                        purchaseReturnDetailsObj.SerialNos = item.SerialNos;
                        var lastinsrteddetailId = await _purchaseReturnEntryData.InsertPurchaseReturnEntryDetails(purchaseReturnDetailsObj);

                        StockMovementDto stockMovementObj = new StockMovementDto();
                        stockMovementObj.InventoryId = item.InventoryId;
                        stockMovementObj.BranchId = model.BranchId;
                        stockMovementObj.TranCode = model.DebitNoteNo;
                        stockMovementObj.TranId = model.DebitNoteNo;
                        stockMovementObj.TranDetailId = lastinsrteddetailId;
                        stockMovementObj.FYId = 1;
                        stockMovementObj.UnitId = inventoryData.UnitId;
                        stockMovementObj.TranRate = item.Rate;
                        stockMovementObj.TranQty = item.Qty;
                        stockMovementObj.TranAmount = item.TotalAmount;
                        stockMovementObj.TranBook = AppConstants.Tran.PurchaseReturn;
                        stockMovementObj.TranType = AppConstants.Tran.Credit;
                        stockMovementObj.Created_Date = DateTime.Now;
                        stockMovementObj.Modified_Date = DateTime.Now;
                        stockMovementObj.InsertFrom = "PurchaseReturn";
                        stockMovementObj.SerialNos = purchaseReturnDetailsObj.SerialNos.Split(';').ToList();
                        await _transactionData.InsertUpdateStockMovement(stockMovementObj);

                        foreach (var serialNo in purchaseReturnDetailsObj.SerialNos.Split(';'))
                        {
                            await _purchaseReturnEntryData.InsertPurchaseReturnEntryItemSerialNosDetails(purchaseReturnDetailsObj.DebitNoteNo, purchaseReturnDetailsObj.InventoryId, serialNo);
                        }
                    }
                    if (model.PurchaseReturnEntryOtherChargesDetails != null)
                    {
                        foreach (PurchaseReturnEntryOtherChargesDetailsViewModel item in model.PurchaseReturnEntryOtherChargesDetails)
                        {
                            PurchaseReturnEntryOtherChargesDetailsDto peOtherChargeObj = new PurchaseReturnEntryOtherChargesDetailsDto();
                            //peOtherChargeObj.Id = item.Id;
                            peOtherChargeObj.DebitNoteNo = item.DebitNoteNo;
                            peOtherChargeObj.Amount = item.Amount;
                            peOtherChargeObj.OtherChargeId = item.OtherChargeId;
                            peOtherChargeObj.FinalAmount = item.FinalAmount;
                            await _purchaseReturnEntryData.InsertPurchaseReturnEntryOtherChargesDetails(peOtherChargeObj);
                        }
                    }

                    retObj.ResponseDataList.Add(_erpCommonServiceData.GetIdByTable("PurchaseReturnEntry"));
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
        [Route("api/PurchaseReturnEntry/DeletePurchaseReturnEntryByKey")]
        //[HttpDelete("{id}")]
        public async Task<JsonResult> DeletePurchaseReturnEntryByKey(string DebitNoteNo)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.PurchaseReturnEntry, RoleActions.Delete);
            if (hasRights == true)
            {
                try
                {
                    List<string> serialIdList = new List<string>();
                    var listSerialNo = await _purchaseReturnEntryData.GetSerialNosByDebitNoteNo(DebitNoteNo);
                    listSerialNo.ForEach(j => { serialIdList.Add("'" + j + "'"); });
                    string serialIdData = String.Join(',', serialIdList);
                    var countSerial = await _purchaseReturnEntryData.IsPurchaseReturnCanDelete(serialIdData, DebitNoteNo);
                    if(countSerial > 0)
                    {
                        retObj.IsError = true;
                        retObj.ErrorMessage = "You can not delete it! Data is in use.";
                        return Json(retObj);
                    }
                    else
                    {
                        var peOtherCharges = await _purchaseReturnEntryData.GetPurchaseReturnEntryOtherChargesDetailsByDebitNoteNo(DebitNoteNo);
                        if (peOtherCharges != null)
                        {
                            await _purchaseReturnEntryData.DeletePurchaseReturnEntryOtherChargesDetailsByDebitNoteNo(DebitNoteNo);
                        }
                        var peDetailsObj = await _purchaseReturnEntryData.GetPurchaseReturnEntryDetailsByDebitNoteNo(DebitNoteNo);
                        if (peDetailsObj != null)
                        {
                            await _purchaseReturnEntryData.DeletePurchaseReturnEntryItemSerialNosDetailsByDebitNoteNo(DebitNoteNo);
                            await _purchaseReturnEntryData.DeletePurchaseReturnEntryDetailsByDebitNoteNo(DebitNoteNo);
                        }
                        await _purchaseReturnEntryData.DeletePurchaseReturnEntryByKey(DebitNoteNo);
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data deleted Successfully!";
                        await _transactionData.DeleteStockMovementByTranCode(DebitNoteNo);
                        return Json(retObj);
                    }
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
        [Route("api/PurchaseReturnEntry/GetInValidSerialIdsForPurchReturnByInventoryId")]
        //[HttpGet]
        public async Task<JsonResult> GetInValidSerialIdsForPurchReturnByInventoryId(int inventoryId, string PRENo)
        {
            var serialIds = await _purchaseReturnEntryData.GetInValidSerialIdsForPurchReturnByInventoryId(inventoryId,PRENo);

            return Json(serialIds);
        }
        #endregion

    }
}
