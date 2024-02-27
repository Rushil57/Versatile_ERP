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
    public class PurchaseEntryController : BaseController
    {
        private readonly IPurchaseEntryService _purchaseEntryData;
        private readonly IERP_CommonService _erpCommonServiceData;
        private readonly IAccountPartyMasterService _accountPartyMasterData;
        private readonly IInventoryMasterService _inventoryMasterData;
        private readonly IOtherChargesMasterService _otherChargesMasterData;
        private readonly IInventoryGSTDetailsService _inventoryGSTDetailsData;
        private readonly ICompanyService _companyData;
        private readonly ITransactionService _transactionData;
        public PurchaseEntryController(IPurchaseEntryService purchaseEntryData, IERP_CommonService erpCommonServiceData, IAccountPartyMasterService accountPartyMasterData, IOtherChargesMasterService otherChargesMasterData, IInventoryMasterService inventoryMasterData, IInventoryGSTDetailsService inventoryGSTDetailsData, ICompanyService companyData, ITransactionService transactionData)
        {
            _purchaseEntryData = purchaseEntryData;
            _erpCommonServiceData = erpCommonServiceData;
            _accountPartyMasterData = accountPartyMasterData;
            _inventoryMasterData = inventoryMasterData;
            _otherChargesMasterData = otherChargesMasterData;
            _inventoryGSTDetailsData = inventoryGSTDetailsData;
            _companyData = companyData;
            _transactionData = transactionData;
        }
        #region PurchaseEntry
        [Route("api/PurchaseEntry/GetAllPurchaseEntry")]
        [HttpGet]
        public async Task<JsonResult> GetAllPurchaseEntry()
        {
            var purchaseEntrylist = await _purchaseEntryData.GetAllPurchaseEntry();
            return Json(purchaseEntrylist);
        }
        [Route("api/PurchaseEntry/GetPurchaseEntryByKey")]
        //[HttpGet]
        public async Task<JsonResult> GetPurchaseEntryByKey(string PENo)
        {
            PurchaseEntryViewModel model = new PurchaseEntryViewModel();
            model.PurchaseEntryDetailsData = await _purchaseEntryData.GetPurchaseEntryDetailsByPENo(PENo);
            model.PEOtherChargesDetailsData = await _purchaseEntryData.GetPEOtherChargesDetailsByPENo(PENo);
            return Json(model);
        }
        [Route("api/PurchaseEntry/PrintPurchaseEntry")]
        //[HttpGet]
        public async Task<ActionResult> PrintPurchaseEntry(string PENo)
        {
            PEInfoViewModel model = new PEInfoViewModel();
            model.PurchaseEntryData = await _purchaseEntryData.GetPurchaseEntryByKey(PENo);
            model.CompanyData = await _companyData.GetAllCompany();
            model.PurchaseEntryDetails = await _purchaseEntryData.GetPurchaseEntryDetailsByPENo(PENo);
            model.PEOtherChargesDetails = await _purchaseEntryData.GetPEOtherChargesDetailsByPENo(PENo);
            var totalAmount = (int)model.PurchaseEntryData.TotalAmount;
            model.TotalAmountinWord = ERP_Utitlity.NumberToWords(totalAmount);
            return View(model);
        }
        [Route("api/PurchaseEntry/SavePurchaseEntry")]
        [HttpPost]
        public async Task<JsonResult> SavePurchaseEntry(PurchaseEntryViewModel model)
        {
            JsonResponseData retObj = new JsonResponseData();
            var sessionEmployee = HttpContext.Session.GetObject<EmployeeMasterDto>(AppConstants.SessionKey.CURRENT_USER);
            var hasRights = IsHasRight(AppPages.PurchaseEntry, RoleActions.InsertUpdate);
            if (hasRights == true)
            {
                try
                {
                    List<string> serialIdList = new List<string>();
                    model.PurchaseEntryDetails.Select(x => x.SerialNos.Split(';').Select(z => z))
                        .ToList().ForEach(t => { t.ToList().ForEach(j => { serialIdList.Add("'"+j+"'"); }); });
                    if(serialIdList.GroupBy(n => n).Any(c => c.Count() > 1))
                    {
                        retObj.IsError = true;
                        retObj.ErrorMessage = "Duplicate Serial Number! Please enter different Serial Number!";
                        return Json(retObj);
                    }
                    string serialIdData = String.Join(',', serialIdList);
                    string peno = "'" + model.PENo + "'";
                    var serialCount = await _purchaseEntryData.CheckValidItemSerialNos(serialIdData, peno);

                    if(serialCount == "0")
                    {
                        PurchaseEntryDto dtoObj = new PurchaseEntryDto();
                        if (model.PageMode == "New")
                        {
                            dtoObj.Created_Date = DateTime.Now;
                            dtoObj.CreatedBy = sessionEmployee.UserId;
                            retObj.IsError = false;
                            retObj.SuccessMessage = "Record inserted successfully !";
                        }
                        else
                        {
                            var purchasereturncount = await _purchaseEntryData.GetPurchaseReturnEntryByPENo(model.PENo);
                            if(purchasereturncount == null)
                            {
                                dtoObj.Created_Date = model.Created_Date;
                                dtoObj.Modified_Date = DateTime.Now;
                                dtoObj.CreatedBy = model.CreatedBy;
                                dtoObj.ModifiedBy = sessionEmployee.UserId;
                                retObj.IsError = false;
                                retObj.SuccessMessage = "Record updated successfully !";
                            }
                            else
                            {
                                retObj.IsError = true;
                                retObj.ErrorMessage = "You can not update it! Purchase Return Entry is already added.";
                                return Json(retObj);
                            }
                        }
                        dtoObj.PENo = model.PENo;
                        if (!string.IsNullOrEmpty(model.PurchaseDateString))
                        {
                            dtoObj.PurchaseDate = new DateTime(Convert.ToInt32(model.PurchaseDateString.Split("/")[2]), Convert.ToInt32(model.PurchaseDateString.Split("/")[1]), Convert.ToInt32(model.PurchaseDateString.Split("/")[0]));
                        }

                        dtoObj.SupplierInvoiceNo = model.SupplierInvoiceNo;
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

                        dtoObj.TotalQty = model.PurchaseEntryDetails.Sum(x => x.Qty);
                        var lastInsertedId = await _purchaseEntryData.InsertPurchaseEntry(dtoObj);
                        if (lastInsertedId == "Duplicate")
                        {
                            retObj.IsError = true;
                            retObj.ErrorMessage = "Supplier Invoice Number already exist !";
                            return Json(retObj);
                        }
                        else
                        {
                            if (model.PageMode != "New")
                            {
                                await _purchaseEntryData.DeletePEItemSerialNosDetailsByPENo(model.PENo);
                                await _purchaseEntryData.DeletePurchaseEntryDetailsByPENo(model.PENo);
                            }

                            //delete data from stockmovement and stockmovement serialdetail
                            await _transactionData.DeleteStockMovementByTranCode(model.PENo);
                            // complete

                            foreach (PurchaseEntryDetailsViewModel item in model.PurchaseEntryDetails)
                            {
                                var inventoryData = await _inventoryMasterData.GetInventoryByKey(item.InventoryId);
                                PurchaseEntryDetailsDto purchaseDetailsObj = new PurchaseEntryDetailsDto();
                                // purchaseDetailsObj.Id = item.Id;
                                purchaseDetailsObj.PENo = lastInsertedId;
                                purchaseDetailsObj.InventoryId = item.InventoryId;
                                purchaseDetailsObj.Qty = item.Qty;
                                purchaseDetailsObj.Rate = item.Rate;
                                purchaseDetailsObj.GrossAmt = item.GrossAmt;
                                purchaseDetailsObj.Discount = item.Discount;
                                purchaseDetailsObj.DiscAmt = item.DiscAmt;
                                //purchaseDetailsObj.UnitId = item.UnitId;
                                purchaseDetailsObj.Tax = item.Tax;
                                purchaseDetailsObj.TaxAmt = item.TaxAmt;
                                purchaseDetailsObj.TotalAmount = item.TotalAmount;
                                purchaseDetailsObj.SerialNos = item.SerialNos;
                                var lastinsrteddetailId = await _purchaseEntryData.InsertPurchaseEntryDetails(purchaseDetailsObj);

                                StockMovementDto stockMovementObj = new StockMovementDto();
                                stockMovementObj.InventoryId = item.InventoryId;
                                stockMovementObj.BranchId = model.BranchId;
                                stockMovementObj.TranCode = model.PENo;
                                stockMovementObj.TranId = model.PENo;
                                stockMovementObj.TranDetailId = lastinsrteddetailId;
                                stockMovementObj.FYId = 1;
                                stockMovementObj.UnitId = inventoryData.UnitId;  
                                stockMovementObj.TranRate = item.Rate;
                                stockMovementObj.TranQty = item.Qty;
                                stockMovementObj.TranAmount = item.TotalAmount;
                                stockMovementObj.TranBook = AppConstants.Tran.PurchaseInvoice;
                                stockMovementObj.TranType = AppConstants.Tran.Debit;
                                stockMovementObj.Created_Date = DateTime.Now;
                                stockMovementObj.Modified_Date = DateTime.Now;
                                stockMovementObj.InsertFrom = "PurchaseInvoice";
                                stockMovementObj.SerialNos = purchaseDetailsObj.SerialNos.Split(';').ToList();
                                await _transactionData.InsertUpdateStockMovement(stockMovementObj);

                                foreach (var serialNo in purchaseDetailsObj.SerialNos.Split(';'))
                                {
                                    await _purchaseEntryData.InsertPurchaseEntryItemSerialNosDetails(purchaseDetailsObj.PENo, purchaseDetailsObj.InventoryId, serialNo);
                                }
                            }
                            if (model.PageMode != "New")
                            {
                                await _purchaseEntryData.DeletePEOtherChargesDetailsByPENo(model.PENo);
                            }
                            if (model.PEOtherChargesDetails != null)
                            {
                                foreach (PEOtherChargesDetailsViewModel item in model.PEOtherChargesDetails)
                                {
                                    PEOtherChargesDetailsDto peOtherChargeObj = new PEOtherChargesDetailsDto();
                                    //peOtherChargeObj.Id = item.Id;
                                    peOtherChargeObj.PENo = item.PENo;
                                    peOtherChargeObj.Amount = item.Amount;
                                    peOtherChargeObj.OtherChargeId = item.OtherChargeId;
                                    peOtherChargeObj.FinalAmount = item.FinalAmount;
                                    await _purchaseEntryData.InsertPEOtherChargesDetails(peOtherChargeObj);
                                }
                            }
                            retObj.ResponseDataList.Add(_erpCommonServiceData.GetIdByTable("PurchaseEntry"));
                            return Json(retObj);
                        }
                    }
                    else
                    {
                        retObj.IsError = true;
                        retObj.ErrorMessage = "Duplicate Serial Number! Please enter different Serial Number!";
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
        [Route("api/PurchaseEntry/DeletePurchaseEntryByKey")]
        //[HttpDelete("{id}")]
        public async Task<JsonResult> DeletePurchaseEntryByKey(string PENo)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.PurchaseEntry, RoleActions.Delete);
            if (hasRights == true)
            {
                try
                {
                    var purchasereturncount = await _purchaseEntryData.GetPurchaseReturnEntryByPENo(PENo);
                    if(purchasereturncount == null)
                    {
                        var peOtherCharges = await _purchaseEntryData.GetPEOtherChargesDetailsByPENo(PENo);
                        if (peOtherCharges != null)
                        {
                            await _purchaseEntryData.DeletePEOtherChargesDetailsByPENo(PENo);
                        }
                        var peDetailsObj = await _purchaseEntryData.GetPurchaseEntryDetailsByPENo(PENo);
                        if (peDetailsObj != null)
                        {
                            await _purchaseEntryData.DeletePEItemSerialNosDetailsByPENo(PENo);
                            await _purchaseEntryData.DeletePurchaseEntryDetailsByPENo(PENo);
                        }
                        await _purchaseEntryData.DeletePurchaseEntryByKey(PENo);
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data deleted Successfully!";
                        await _transactionData.DeleteStockMovementByTranCode(PENo);
                        return Json(retObj);
                    }
                    else
                    {
                        retObj.IsError = true;
                        retObj.ErrorMessage = "You can not delete it! Purchase Return Entry is already added.";
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
        #endregion
    }
}
