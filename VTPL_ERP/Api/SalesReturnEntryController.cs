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
    public class SalesReturnEntryController : BaseController
    {
        private readonly ISalesReturnEntryService _salesReturnEntryData;
        private readonly IERP_CommonService _erpCommonServiceData;
        private readonly IAccountPartyMasterService _accountPartyMasterData;
        private readonly IInventoryMasterService _inventoryMasterData;
        private readonly IOtherChargesMasterService _otherChargesMasterData;
        private readonly IInventoryGSTDetailsService _inventoryGSTDetailsData;
        private readonly ICompanyService _companyData;
        private readonly ITransactionService _transactionData;
        public SalesReturnEntryController(ISalesReturnEntryService salesReturnEntryData, IERP_CommonService erpCommonServiceData, IAccountPartyMasterService accountPartyMasterData, IOtherChargesMasterService otherChargesMasterData, IInventoryMasterService inventoryMasterData, IInventoryGSTDetailsService inventoryGSTDetailsData, ICompanyService companyData, ITransactionService transactionData)
        {
            _salesReturnEntryData = salesReturnEntryData;
            _erpCommonServiceData = erpCommonServiceData;
            _accountPartyMasterData = accountPartyMasterData;
            _inventoryMasterData = inventoryMasterData;
            _otherChargesMasterData = otherChargesMasterData;
            _inventoryGSTDetailsData = inventoryGSTDetailsData;
            _companyData = companyData;
            _transactionData = transactionData;
        }
        #region SalesReturnEntry
        [Route("api/SalesReturnEntry/GetAllSalesReturnEntry")]
        [HttpGet]
        public async Task<JsonResult> GetAllSalesReturnEntry()
        {
            var salesReturnEntrylist = await _salesReturnEntryData.GetAllSalesReturnEntry();
            return Json(salesReturnEntrylist);
        }
        [Route("api/SalesReturnEntry/GetSalesReturnEntryByKey")]
        //[HttpGet]
        public async Task<JsonResult> GetSalesReturnEntryByKey(string CreditNoteNo)
        {
            SalesReturnEntryViewModel model = new SalesReturnEntryViewModel();
            model.SalesReturnEntryDetailsData = await _salesReturnEntryData.GetSalesReturnEntryDetailsByCreditNoteNo(CreditNoteNo);
            model.SalesReturnEntryOtherChargesDetailsData = await _salesReturnEntryData.GetSalesReturnEntryOtherChargesDetailsByCreditNoteNo(CreditNoteNo);
            return Json(model);
        }
        [Route("api/SalesReturnEntry/PrintSalesReturnEntry")]
        //[HttpGet]
        public async Task<ActionResult> PrintSalesReturnEntry(string CreditNoteNo)
        {
            SalesReturnEntryInfoViewModel model = new SalesReturnEntryInfoViewModel();
            model.SalesReturnEntryData = await _salesReturnEntryData.GetSalesReturnEntryByKey(CreditNoteNo);
            model.CompanyData = await _companyData.GetAllCompany();
            model.SalesReturnEntryDetails = await _salesReturnEntryData.GetSalesReturnEntryDetailsByCreditNoteNo(CreditNoteNo);
            model.SalesReturnEntryOtherChargesDetails = await _salesReturnEntryData.GetSalesReturnEntryOtherChargesDetailsByCreditNoteNo(CreditNoteNo);
            var totalAmount = (int)model.SalesReturnEntryData.TotalAmount;
            model.TotalAmountinWord = ERP_Utitlity.NumberToWords(totalAmount);
            return View(model);
        }
        [Route("api/SalesReturnEntry/SaveSalesReturnEntry")]
        [HttpPost]
        public async Task<JsonResult> SaveSalesReturnEntry(SalesReturnEntryViewModel model)
        {
            JsonResponseData retObj = new JsonResponseData();
            var sessionEmployee = HttpContext.Session.GetObject<EmployeeMasterDto>(AppConstants.SessionKey.CURRENT_USER);
            var hasRights = IsHasRight(AppPages.SalesReturnEntry, RoleActions.InsertUpdate);
            if (hasRights == true)
            {
                try
                {
                    SalesReturnEntryDto dtoObj = new SalesReturnEntryDto();
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
                        var listSerialNo = await _salesReturnEntryData.GetSerialNosByCreditNoteNo(model.CreditNoteNo);
                        listSerialNo.ForEach(j => { serialIdList.Add("'" + j + "'"); });
                        string serialIdData = String.Join(',', serialIdList);
                        var countSerial = await _salesReturnEntryData.IsSalesReturnCanDelete(serialIdData, model.CreditNoteNo);

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
                            await _salesReturnEntryData.DeleteSalesReturnEntryItemSerialNosDetailsByCreditNoteNo(model.CreditNoteNo);
                            await _salesReturnEntryData.DeleteSalesReturnEntryOtherChargesDetailsByCreditNoteNo(model.CreditNoteNo);
                            await _salesReturnEntryData.DeleteSalesReturnEntryDetailsByCreditNoteNo(model.CreditNoteNo);
                            retObj.IsError = false;
                            retObj.SuccessMessage = "Record updated successfully !";
                        }
                    }
                    dtoObj.CreditNoteNo = model.CreditNoteNo;
                    if (!string.IsNullOrEmpty(model.SalesReturnDateString))
                    {
                        dtoObj.CreditNoteDate = new DateTime(Convert.ToInt32(model.SalesReturnDateString.Split("/")[2]), Convert.ToInt32(model.SalesReturnDateString.Split("/")[1]), Convert.ToInt32(model.SalesReturnDateString.Split("/")[0]));
                    }
                    dtoObj.OriginalSalesEntryId = model.OriginalSalesEntryId;
                    dtoObj.BranchId = model.BranchId;
                    dtoObj.SupplierId = model.SupplierId;
                    //dtoObj.TotalQty = model.TotalQty;
                    dtoObj.CurrentBalance = model.CurrentBalance;
                    dtoObj.SalesAccount = model.SalesAccount;
                    dtoObj.Narration = model.Narration;
                    dtoObj.Amount = model.Amount;
                    dtoObj.OtherCharges = model.OtherCharges;
                    dtoObj.DiscAmount = model.DiscAmount;
                    dtoObj.SGST = model.SGST;
                    dtoObj.CGST = model.CGST;
                    dtoObj.TotalAmount = model.TotalAmount;
                    dtoObj.RoundOff = model.RoundOff;
                    dtoObj.RefNo = model.RefNo;
                    dtoObj.Destination = model.Destination;
                    dtoObj.DispatchedBy = model.DispatchedBy;
                    dtoObj.DispatchedDocateNo = model.DispatchedDocateNo;

                    dtoObj.TotalQty = model.SalesReturnEntryDetails.Sum(x => x.Qty);
                    var lastInsertedId = await _salesReturnEntryData.InsertSalesReturnEntry(dtoObj);

                    //delete data from stockmovement and stockmovement serialdetail
                    await _transactionData.DeleteStockMovementByTranCode(model.CreditNoteNo);
                    // complete

                    foreach (SalesReturnEntryDetailsViewModel item in model.SalesReturnEntryDetails)
                    {
                        SalesReturnEntryDetailsDto salesReturnDetailsObj = new SalesReturnEntryDetailsDto();
                        // salesReturnDetailsObj.Id = item.Id;
                        salesReturnDetailsObj.CreditNoteNo = lastInsertedId;
                        salesReturnDetailsObj.InventoryId = item.InventoryId;
                        salesReturnDetailsObj.Qty = item.Qty;
                        salesReturnDetailsObj.Rate = item.Rate;
                        salesReturnDetailsObj.GrossAmt = item.GrossAmt;
                        salesReturnDetailsObj.Discount = item.Discount;
                        salesReturnDetailsObj.DiscAmt = item.DiscAmt;
                        salesReturnDetailsObj.UnitId = item.UnitId;
                        salesReturnDetailsObj.Tax = item.Tax;
                        salesReturnDetailsObj.TaxAmt = item.TaxAmt;
                        salesReturnDetailsObj.TotalAmount = item.TotalAmount;
                        salesReturnDetailsObj.SerialNos = item.SerialNos;
                        var lastinsrteddetailId = await _salesReturnEntryData.InsertSalesReturnEntryDetails(salesReturnDetailsObj);

                        StockMovementDto stockMovementObj = new StockMovementDto();
                        stockMovementObj.InventoryId = item.InventoryId;
                        stockMovementObj.BranchId = model.BranchId;
                        stockMovementObj.TranCode = model.CreditNoteNo;
                        stockMovementObj.TranId = model.CreditNoteNo;
                        stockMovementObj.TranDetailId = lastinsrteddetailId;
                        stockMovementObj.FYId = 1;
                        stockMovementObj.UnitId = item.UnitId;
                        stockMovementObj.TranRate = item.Rate;
                        stockMovementObj.TranQty = item.Qty;
                        stockMovementObj.TranAmount = item.TotalAmount;
                        stockMovementObj.TranBook = AppConstants.Tran.SalesReturn;
                        stockMovementObj.TranType = AppConstants.Tran.Debit;
                        stockMovementObj.Created_Date = DateTime.Now;
                        stockMovementObj.Modified_Date = DateTime.Now;
                        stockMovementObj.InsertFrom = "SalesReturn";
                        stockMovementObj.SerialNos = salesReturnDetailsObj.SerialNos.Split(';').ToList();
                        await _transactionData.InsertUpdateStockMovement(stockMovementObj);

                        foreach (var serialNo in salesReturnDetailsObj.SerialNos.Split(';'))
                        {
                            await _salesReturnEntryData.InsertSalesReturnEntryItemSerialNosDetails(salesReturnDetailsObj.CreditNoteNo, salesReturnDetailsObj.InventoryId, serialNo);
                        }
                    }
                    if (model.SalesReturnEntryOtherChargesDetails != null)
                    {
                        foreach (SalesReturnEntryOtherChargesDetailsViewModel item in model.SalesReturnEntryOtherChargesDetails)
                        {
                            SalesReturnEntryOtherChargesDetailsDto seOtherChargeObj = new SalesReturnEntryOtherChargesDetailsDto();
                            //seOtherChargeObj.Id = item.Id;
                            seOtherChargeObj.CreditNoteNo = item.CreditNoteNo;
                            seOtherChargeObj.Amount = item.Amount;
                            seOtherChargeObj.OtherChargeId = item.OtherChargeId;
                            seOtherChargeObj.FinalAmount = item.FinalAmount;
                            await _salesReturnEntryData.InsertSalesReturnEntryOtherChargesDetails(seOtherChargeObj);
                        }
                    }

                    retObj.ResponseDataList.Add(_erpCommonServiceData.GetIdByTable("SalesReturnEntry"));
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
        [Route("api/SalesReturnEntry/DeleteSalesReturnEntryByKey")]
        //[HttpDelete("{id}")]
        public async Task<JsonResult> DeleteSalesReturnEntryByKey(string CreditNoteNo)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.SalesReturnEntry, RoleActions.Delete);
            if (hasRights == true)
            {
                try
                {
                    List<string> serialIdList = new List<string>();
                    var listSerialNo = await _salesReturnEntryData.GetSerialNosByCreditNoteNo(CreditNoteNo);
                    listSerialNo.ForEach(j => { serialIdList.Add("'" + j + "'"); });
                    string serialIdData = String.Join(',', serialIdList);
                    var countSerial = await _salesReturnEntryData.IsSalesReturnCanDelete(serialIdData, CreditNoteNo);
                    if (countSerial > 0)
                    {
                        retObj.IsError = true;
                        retObj.ErrorMessage = "You can not delete it! Data is in use.";
                        return Json(retObj);
                    }
                    else
                    {
                        var seOtherCharges = await _salesReturnEntryData.GetSalesReturnEntryOtherChargesDetailsByCreditNoteNo(CreditNoteNo);
                        if (seOtherCharges != null)
                        {
                            await _salesReturnEntryData.DeleteSalesReturnEntryOtherChargesDetailsByCreditNoteNo(CreditNoteNo);
                        }
                        var seDetailsObj = await _salesReturnEntryData.GetSalesReturnEntryDetailsByCreditNoteNo(CreditNoteNo);
                        if (seDetailsObj != null)
                        {
                            await _salesReturnEntryData.DeleteSalesReturnEntryItemSerialNosDetailsByCreditNoteNo(CreditNoteNo);
                            await _salesReturnEntryData.DeleteSalesReturnEntryDetailsByCreditNoteNo(CreditNoteNo);
                        }
                        await _salesReturnEntryData.DeleteSalesReturnEntryByKey(CreditNoteNo);
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data deleted Successfully!";
                        await _transactionData.DeleteStockMovementByTranCode(CreditNoteNo);
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
        [Route("api/SalesReturnEntry/GetInValidSerialIdsForSalesReturnByInventoryId")]
        //[HttpGet]
        public async Task<JsonResult> GetInValidSerialIdsForSalesReturnByInventoryId(int inventoryId, string CreditNoteNo)
        {
            var serialIds = await _salesReturnEntryData.GetInValidSerialIdsForSalesReturnByInventoryId(inventoryId, CreditNoteNo);

            return Json(serialIds);
        }
        #endregion

    }
}
