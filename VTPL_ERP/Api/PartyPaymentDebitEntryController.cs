using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VTPL_ERP.Models.Master;
using ERP.DAL.Abstract;
using ERP.DAL.Dto;
using VTPL_ERP.Util;
using VTPL_ERP.Models;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Security.Cryptography;
using VTPL_ERP.Controllers;
using static VTPL_ERP.Util.AppConstants;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VTPL_ERP.Api
{
    //[Route("api/[controller]")]
    public class PartyPaymentDebitEntryController : BaseController
    {
        private readonly IPartyPaymentDebitEntryService _partyPaymentDebitEntryData;
        private readonly IAccountPartyMasterService _accountPartyMasterData;
        private readonly IEmployeeMasterService _employeeMasterData;
        private readonly ISalesEntryService _salesEntryData;
        private readonly IERP_CommonService _erpCommonServiceData;
        public PartyPaymentDebitEntryController(IPartyPaymentDebitEntryService partyPaymentDebitEntryData, IERP_CommonService erpCommonServiceData, IAccountPartyMasterService accountPartyMasterData, IEmployeeMasterService employeeMasterData, ISalesEntryService salesEntryData)
        {
            _partyPaymentDebitEntryData = partyPaymentDebitEntryData;
            _accountPartyMasterData = accountPartyMasterData;
            _employeeMasterData = employeeMasterData;
            _salesEntryData = salesEntryData;
            _erpCommonServiceData = erpCommonServiceData;
        }
        #region PartyPaymentDebitEntry
        [Route("api/PartyPaymentDebitEntry/GetAllPartyPaymentDebitEntry")]
        [HttpGet]
        public async Task<JsonResult> GetAllPartyPaymentDebitEntry()
        {
            var partyPaymentDebitEntrylist = await _partyPaymentDebitEntryData.GetAllPartyPaymentDebitEntry();
            return Json(partyPaymentDebitEntrylist);
        }

        [Route("api/PartyPaymentDebitEntry/GetPartyPaymentDebitEntryByKey")]
        //[HttpGet("{id}")]
        public async Task<JsonResult> GetPartyPaymentDebitEntryByKey(string id)
        {
            var partyPaymentDebitEntryObj = await _partyPaymentDebitEntryData.GetPartyPaymentDebitEntryByKey(id);
            return Json(partyPaymentDebitEntryObj);
        }

        [Route("api/PartyPaymentDebitEntry/DeletePartyPaymentDebitEntryByKey")]
        //[HttpDelete("{id}")]
        public async Task<JsonResult> DeletePartyPaymentDebitEntryByKey(string id)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.PartyPaymentDebitEntry, RoleActions.Delete);
            if (hasRights == true)
            {
                try
                {
                    var ppeDetailsObj = await _partyPaymentDebitEntryData.GetPartyPaymentDebitEntryByKey(id);
                    if (ppeDetailsObj.PartyPaymentDebitEntryDetailsData != null)
                    {
                        await _partyPaymentDebitEntryData.DeletePartyPaymentDebitEntryDetailsByReceiptNo(id);
                    }
                    await _partyPaymentDebitEntryData.DeletePartyPaymentDebitEntryByKey(id);
                    retObj.IsError = false;
                    retObj.SuccessMessage = "Data deleted Successfully!";
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
        [Route("api/PartyPaymentDebitEntry/SavePartyPaymentDebitEntry")]
        [HttpPost]
        public async Task<JsonResult> SavePartyPaymentDebitEntry(PartyPaymentDebitEntryViewModel model)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.PartyPaymentDebitEntry, RoleActions.InsertUpdate);
            if (hasRights == true)
            {
                try
                {
                    PartyPaymentDebitEntryDto dtoObj = new PartyPaymentDebitEntryDto();
                    if (model.PageMode == "New")
                    {
                        dtoObj.Created_Date = DateTime.Now;
                    }
                    else
                    {
                        dtoObj.Created_Date = model.Created_Date;
                        dtoObj.Modified_Date = DateTime.Now;
                    }
                    dtoObj.ReceiptNo = model.ReceiptNo;
                    if (!string.IsNullOrEmpty(model.PaymentDateDisplay))
                    {
                        dtoObj.PaymentDate = new DateTime(Convert.ToInt32(model.PaymentDateDisplay.Split("/")[2]), Convert.ToInt32(model.PaymentDateDisplay.Split("/")[1]), Convert.ToInt32(model.PaymentDateDisplay.Split("/")[0]));
                    }
                    if (!string.IsNullOrEmpty(model.ChequeDateDisplay))
                    {
                        dtoObj.ChequeDate = new DateTime(Convert.ToInt32(model.ChequeDateDisplay.Split("/")[2]), Convert.ToInt32(model.ChequeDateDisplay.Split("/")[1]), Convert.ToInt32(model.ChequeDateDisplay.Split("/")[0]));
                    }
                    dtoObj.CompanyBankId = model.CompanyBankId;
                    dtoObj.AccountPartyId = model.AccountPartyId;
                    dtoObj.SalesEntryId = model.SalesEntryId;
                    dtoObj.Amount = model.Amount;
                    dtoObj.Narration = model.Narration;
                    dtoObj.APCurrentBalance = model.APCurrentBalance;
                    dtoObj.AgstRef = model.AgstRef;
                    dtoObj.CompanyCurrentBalance = model.CompanyCurrentBalance;
                    dtoObj.TotalPaidAmount = model.TotalPaidAmount;
                    dtoObj.PaymentType = model.PaymentType;
                    dtoObj.ChequeNo = model.ChequeNo;
                    dtoObj.Bank = model.Bank;
                    dtoObj.Branch = model.Branch;
                    dtoObj.Remarks = model.Remarks;
                    var lastInsertedId = await _partyPaymentDebitEntryData.InsertUpdatePartyPaymentDebitEntry(dtoObj);
                    if (model.PageMode != "New")
                    {
                        await _partyPaymentDebitEntryData.DeletePartyPaymentDebitEntryDetailsByReceiptNo(model.ReceiptNo);
                    }
                    foreach (PartyPaymentDebitEntryDetailsViewModel item in model.PartyPaymentDebitEntryDetails)
                    {
                        PartyPaymentDebitEntryDetailsDto partyPaymentDebitEntryDetailsObj = new PartyPaymentDebitEntryDetailsDto();
                        // partyPaymentDebitEntryDetailsObj.Id = item.Id;
                        partyPaymentDebitEntryDetailsObj.ReceiptNo = model.ReceiptNo;
                        partyPaymentDebitEntryDetailsObj.InvoiceNo = item.InvoiceNo;
                        partyPaymentDebitEntryDetailsObj.Amount = item.Amount;
                        await _partyPaymentDebitEntryData.InsertPartyPaymentDebitEntryDetails(partyPaymentDebitEntryDetailsObj);
                    }
                    if (lastInsertedId != null)
                    {
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data Saved Successfully!";
                    }
                    else
                    {
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data Updated Successfully!";
                    }
                    retObj.ResponseDataList.Add(_erpCommonServiceData.GetIdByTable("PartyPaymentDebitEntry"));
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
        [Route("api/PartyPaymentDebitEntry/GetIdByTable")]
        [HttpGet]
        public async Task<JsonResult> GetIdByTable()
        {
            var receiptId = await _erpCommonServiceData.GetIdByTable("PartyPaymentDebitEntry");
            return Json(receiptId);
        }
        [Route("api/PartyPaymentDebitEntry/GetPendingBillDetails")]
        public async Task<JsonResult> GetPendingBillDetails(int SupplierId)
        {
            JsonResponseData retObj = new JsonResponseData();
            try
            {
                var salesEntryData = await _salesEntryData.GetAllSalesEntryBySupplierId(SupplierId);

                if (salesEntryData.Count != 0)
                {
                    PartyPaymentDebitEntryViewModel model = new PartyPaymentDebitEntryViewModel();
                    model.SalesEntryData = salesEntryData;
                    return Json(model);
                }
                else
                {
                    retObj.IsError = true;
                    retObj.ErrorMessage = "Pending Bill detail is not available";
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
        #endregion
    }
}
