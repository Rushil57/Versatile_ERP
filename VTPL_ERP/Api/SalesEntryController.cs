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
    public class SalesEntryController : BaseController
    {
        private readonly ISalesEntryService _salesEntryData;
        private readonly IERP_CommonService _erpCommonServiceData;
        private readonly IAccountPartyMasterService _accountPartyMasterData;
        private readonly IInventoryMasterService _inventoryMasterData;
        private readonly IOtherChargesMasterService _otherChargesMasterData;
        private readonly IInventoryGSTDetailsService _inventoryGSTDetailsData;
        private readonly ICompanyService _companyData;
        private readonly ITransactionService _transactionData;
        public SalesEntryController(ISalesEntryService salesEntryData, IERP_CommonService erpCommonServiceData, IAccountPartyMasterService accountPartyMasterData, IOtherChargesMasterService otherChargesMasterData, IInventoryMasterService inventoryMasterData, IInventoryGSTDetailsService inventoryGSTDetailsData, ICompanyService companyData, ITransactionService transactionData)
        {
            _salesEntryData = salesEntryData;
            _erpCommonServiceData = erpCommonServiceData;
            _accountPartyMasterData = accountPartyMasterData;
            _inventoryMasterData = inventoryMasterData;
            _otherChargesMasterData = otherChargesMasterData;
            _inventoryGSTDetailsData = inventoryGSTDetailsData;
            _companyData = companyData;
            _transactionData = transactionData;
        }
        #region SalesEntry
        [Route("api/SalesEntry/GetAllSalesEntry")]
        [HttpGet]
        public async Task<JsonResult> GetAllSalesEntry()
        {
            var salesEntrylist = await _salesEntryData.GetAllSalesEntry();
            return Json(salesEntrylist);
        }
        [Route("api/SalesEntry/GetSalesEntryByKey")]
        //[HttpGet]
        public async Task<JsonResult> GetSalesEntryByKey(string SalesEntryNo)
        {
            SalesEntryViewModel model = new SalesEntryViewModel();
            model.SalesEntryDetailsData = await _salesEntryData.GetSalesEntryDetailsBySalesEntryNo(SalesEntryNo);
            model.SEOtherChargesDetailsData = await _salesEntryData.GetSEOtherChargesDetailsBySalesEntryNo(SalesEntryNo);
            return Json(model);
        }
        [Route("api/SalesEntry/PrintSalesEntry")]
        //[HttpGet]
        public async Task<ActionResult> PrintSalesEntry(string SalesEntryNo)
        {
            SEInfoViewModel model = new SEInfoViewModel();
            model.SalesEntryData = await _salesEntryData.GetSalesEntryByKey(SalesEntryNo);
            model.CompanyData = await _companyData.GetAllCompany();
            model.SalesEntryDetails = await _salesEntryData.GetSalesEntryDetailsBySalesEntryNo(SalesEntryNo);
            model.SEOtherChargesDetails = await _salesEntryData.GetSEOtherChargesDetailsBySalesEntryNo(SalesEntryNo);
            var totalAmount = (int)model.SalesEntryData.TotalAmount;
            model.TotalAmountinWord = ERP_Utitlity.NumberToWords(totalAmount);
            return View(model);
        }
        
        [Route("api/SalesEntry/SaveSalesEntry")]
        [HttpPost]
        public async Task<JsonResult> SaveSalesEntry(SalesEntryViewModel model)
        {
            JsonResponseData retObj = new JsonResponseData();
            var sessionEmployee = HttpContext.Session.GetObject<EmployeeMasterDto>(AppConstants.SessionKey.CURRENT_USER);
            var hasRights = IsHasRight(AppPages.SalesEntry, RoleActions.InsertUpdate);
            if (hasRights == true)
            {
                try
                {
                    SalesEntryDto dtoObj = new SalesEntryDto();
                    if (model.PageMode == "New")
                    {
                        dtoObj.Created_Date = DateTime.Now;
                        dtoObj.CreatedBy = sessionEmployee.UserId;
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Record inserted successfully !";
                    }
                    else
                    {
                        var salesreturncount = await _salesEntryData.GetSalesReturnEntryBySalesEntryNo(model.SalesEntryNo);
                        if(salesreturncount == null)
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
                            retObj.ErrorMessage = "You can not update it! Sales Return Entry is already added.";
                            return Json(retObj);
                        }
                    }
                    dtoObj.SalesEntryNo = model.SalesEntryNo;
                    if (!string.IsNullOrEmpty(model.SalesDateString))
                    {
                        dtoObj.SalesEntryDate = new DateTime(Convert.ToInt32(model.SalesDateString.Split("/")[2]), Convert.ToInt32(model.SalesDateString.Split("/")[1]), Convert.ToInt32(model.SalesDateString.Split("/")[0]));
                    }

                    dtoObj.RefNo = model.RefNo;
                    dtoObj.BranchId = model.BranchId;
                    dtoObj.SupplierId = model.SupplierId;
                    //dtoObj.TotalQty = model.TotalQty;
                    dtoObj.CurrentBalance = model.CurrentBalance;
                    dtoObj.SalesAccount = model.SalesAccount;
                    dtoObj.DispatchedBy = model.DispatchedBy;
                    dtoObj.DispatchedDocateNo = model.DispatchedDocateNo;
                    dtoObj.Destination = model.Destination;
                    dtoObj.Narration = model.Narration;
                    dtoObj.Amount = model.Amount;
                    dtoObj.OtherCharges = model.OtherCharges;
                    dtoObj.DiscAmount = model.DiscAmount;
                    dtoObj.SGST = model.SGST;
                    dtoObj.CGST = model.CGST;
                    dtoObj.TotalAmount = model.TotalAmount;
                    dtoObj.RoundOff = model.RoundOff;
                    dtoObj.PaymentStatus = model.PaymentStatus;
                    dtoObj.TotalQty = model.SalesEntryDetails.Sum(x => x.Qty);
                    var lastInsertedId = await _salesEntryData.InsertSalesEntry(dtoObj);
                    if (lastInsertedId == "Duplicate")
                    {
                        retObj.IsError = true;
                        retObj.ErrorMessage = "Ref Number already exist !";
                        return Json(retObj);
                    }
                    else
                    {
                        if (model.PageMode != "New")
                        {
                            await _salesEntryData.DeleteSEItemSerialNosDetailsBySalesEntryNo(model.SalesEntryNo);
                            await _salesEntryData.DeleteSalesEntryDetailsBySalesEntryNo(model.SalesEntryNo);
                        }

                        //delete data from stockmovement and stockmovement serialdetail
                        await _transactionData.DeleteStockMovementByTranCode(model.SalesEntryNo);
                        // complete

                        foreach (SalesEntryDetailsViewModel item in model.SalesEntryDetails)
                        {
                            SalesEntryDetailsDto salesDetailsObj = new SalesEntryDetailsDto();
                            // salesDetailsObj.Id = item.Id;
                            salesDetailsObj.SalesEntryNo = lastInsertedId;
                            salesDetailsObj.InventoryId = item.InventoryId;
                            salesDetailsObj.Qty = item.Qty;
                            salesDetailsObj.Rate = item.Rate;
                            salesDetailsObj.GrossAmt = item.GrossAmt;
                            salesDetailsObj.Discount = item.Discount;
                            salesDetailsObj.DiscAmt = item.DiscAmt;
                            salesDetailsObj.UnitId = item.UnitId;
                            salesDetailsObj.Tax = item.Tax;
                            salesDetailsObj.TaxAmt = item.TaxAmt;
                            salesDetailsObj.TotalAmount = item.TotalAmount;
                            salesDetailsObj.SerialNos = item.SerialNos;
                            var lastinsrteddetailId = await _salesEntryData.InsertSalesEntryDetails(salesDetailsObj);

                            StockMovementDto stockMovementObj = new StockMovementDto();
                            stockMovementObj.InventoryId = item.InventoryId;
                            stockMovementObj.BranchId = model.BranchId;
                            stockMovementObj.TranCode = model.SalesEntryNo;
                            stockMovementObj.TranId = model.SalesEntryNo;
                            stockMovementObj.TranDetailId = lastinsrteddetailId;
                            stockMovementObj.FYId = 1;
                            stockMovementObj.UnitId = item.UnitId;
                            stockMovementObj.TranRate = item.Rate;
                            stockMovementObj.TranQty = item.Qty;
                            stockMovementObj.TranAmount = item.TotalAmount;
                            stockMovementObj.TranBook = AppConstants.Tran.SalesInvoice;
                            stockMovementObj.TranType = AppConstants.Tran.Credit;
                            stockMovementObj.Created_Date = DateTime.Now;
                            stockMovementObj.Modified_Date = DateTime.Now;
                            stockMovementObj.InsertFrom = "SalesInvoice";
                            stockMovementObj.SerialNos = salesDetailsObj.SerialNos.Split(';').ToList();
                            await _transactionData.InsertUpdateStockMovement(stockMovementObj);

                            foreach (var serialNo in salesDetailsObj.SerialNos.Split(';'))
                            {
                                await _salesEntryData.InsertSalesEntryItemSerialNosDetails(salesDetailsObj.SalesEntryNo, salesDetailsObj.InventoryId, serialNo);
                            }
                        }
                        if (model.PageMode != "New")
                        {
                            await _salesEntryData.DeleteSEOtherChargesDetailsBySalesEntryNo(model.SalesEntryNo);
                        }
                        if (model.SEOtherChargesDetails != null)
                        {
                            foreach (SEOtherChargesDetailsViewModel item in model.SEOtherChargesDetails)
                            {
                                SEOtherChargesDetailsDto seOtherChargeObj = new SEOtherChargesDetailsDto();
                                //seOtherChargeObj.Id = item.Id;
                                seOtherChargeObj.SalesEntryNo = item.SalesEntryNo;
                                seOtherChargeObj.Amount = item.Amount;
                                seOtherChargeObj.OtherChargeId = item.OtherChargeId;
                                seOtherChargeObj.FinalAmount = item.FinalAmount;
                                await _salesEntryData.InsertSEOtherChargesDetails(seOtherChargeObj);
                            }
                        }

                        retObj.ResponseDataList.Add(_erpCommonServiceData.GetIdByTable("SalesEntry"));
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
        [Route("api/SalesEntry/DeleteSalesEntryByKey")]
        //[HttpDelete("{id}")]
        public async Task<JsonResult> DeleteSalesEntryByKey(string SalesEntryNo)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.SalesEntry, RoleActions.Delete);
            if (hasRights == true)
            {
                try
                {
                    var salesreturncount = await _salesEntryData.GetSalesReturnEntryBySalesEntryNo(SalesEntryNo);
                    if(salesreturncount == null)
                    {
                        var seOtherCharges = await _salesEntryData.GetSEOtherChargesDetailsBySalesEntryNo(SalesEntryNo);
                        if (seOtherCharges != null)
                        {
                            await _salesEntryData.DeleteSEOtherChargesDetailsBySalesEntryNo(SalesEntryNo);
                        }
                        var seDetailsObj = await _salesEntryData.GetSalesEntryDetailsBySalesEntryNo(SalesEntryNo);
                        if (seDetailsObj != null)
                        {
                            await _salesEntryData.DeleteSEItemSerialNosDetailsBySalesEntryNo(SalesEntryNo);
                            await _salesEntryData.DeleteSalesEntryDetailsBySalesEntryNo(SalesEntryNo);
                        }
                        await _salesEntryData.DeleteSalesEntryByKey(SalesEntryNo);
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data deleted Successfully!";
                        await _transactionData.DeleteStockMovementByTranCode(SalesEntryNo);
                        return Json(retObj);
                    }
                    else
                    {
                        retObj.IsError = true;
                        retObj.ErrorMessage = "You can not delete it! Sales Return Entry is already added.";
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

        [Route("api/SalesEntry/GetSerialIdsByInventoryId")]
        //[HttpGet]
        public async Task<JsonResult> GetSerialIdsByInventoryId(int inventoryId, int branchId)
        {
            var serialIds = await _salesEntryData.GetAvailableSerialIdByInventoryId(inventoryId, branchId);
            
            return Json(serialIds);
        }
        #endregion

    }
}
