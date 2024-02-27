using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.DAL.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VTPL_ERP.Models;
using VTPL_ERP.Util;

namespace VTPL_ERP.Controllers
{
    public class PartyPaymentDebitEntryController : BaseController
    {
        private readonly IAccountPartyMasterService _accountPartyMasterData;
        private readonly IEmployeeMasterService _employeeMasterData;
        private readonly ISalesEntryService _salesEntryData;
        private readonly IERP_CommonService _erpCommonServiceData;
        private readonly IPartyPaymentDebitEntryService _partyPaymentDebitEntryData;
        public PartyPaymentDebitEntryController(IPartyPaymentDebitEntryService partyPaymentDebitEntryData, IERP_CommonService erpCommonServiceData, IAccountPartyMasterService accountPartyMasterData, IEmployeeMasterService employeeMasterData, ISalesEntryService salesEntryData)
        {
            _accountPartyMasterData = accountPartyMasterData;
            _employeeMasterData = employeeMasterData;
            _salesEntryData = salesEntryData;
            _erpCommonServiceData = erpCommonServiceData;
            _partyPaymentDebitEntryData = partyPaymentDebitEntryData;
        }
        public async Task<ActionResult> PartyPaymentDebitEntry(string ReceiptNo = "")
        {
            PartyPaymentDebitEntryViewModel model = new PartyPaymentDebitEntryViewModel();
            //if (string.IsNullOrEmpty(ReceiptNo))
            //{
               // model.ReceiptNo = await _erpCommonServiceData.GetIdByTable("PartyPaymentDebitEntry");
                model.PaymentDateDisplay = DateTime.Now.ToString("dd/MM/yyyy");
                model.ChequeDateDisplay = DateTime.Now.ToString("dd/MM/yyyy");
            //model.PageMode = "New";
            // }
            //else
            //{
            //    model.PageMode = "Edit";
            //    var partyPaymentObj = await _partyPaymentDebitEntryData.GetPartyPaymentDebitEntryByKey(ReceiptNo);
            //    model.ReceiptNo = partyPaymentObj.ReceiptNo;
            //    model.CompanyBankId = partyPaymentObj.CompanyBankId;
            //    model.SelectedAccountPartyId = partyPaymentObj.AccountPartyId;
            //    model.AccountPartyId = partyPaymentObj.AccountPartyId;
            //    model.Created_Date = partyPaymentObj.Created_Date;
            //    model.SalesEntryId = partyPaymentObj.SalesEntryId;
            //    model.Amount = partyPaymentObj.Amount;
            //    model.Narration = partyPaymentObj.Narration;
            //    model.Modified_Date = partyPaymentObj.Modified_Date;
            //    model.APCurrentBalance = partyPaymentObj.APCurrentBalance;
            //    model.AgstRef = partyPaymentObj.AgstRef;
            //    model.CompanyCurrentBalance = partyPaymentObj.CompanyCurrentBalance;
            //    model.PaymentDateDisplay = partyPaymentObj.PaymentDate.ToString("dd/MM/yyyy");
            //    model.TotalPaidAmount = partyPaymentObj.TotalPaidAmount;
            //    model.PaymentDate = partyPaymentObj.PaymentDate;
            //    model.PaymentType = partyPaymentObj.PaymentType;
            //    model.ChequeNo = partyPaymentObj.ChequeNo;
            //    model.ChequeDate = partyPaymentObj.ChequeDate;
            //    model.Bank = partyPaymentObj.Bank;
            //    model.Branch = partyPaymentObj.Branch;
            //    model.Remarks = partyPaymentObj.Remarks;
            //}
            model.AccountPartySelectList = new SelectList(await _accountPartyMasterData.GetAllAccountParty(), "Id", "Name");
            model.CompanyBankSelectList = new SelectList(await _accountPartyMasterData.GetAccountPartyBySundery(14), "Id", "Name");
            model.EmployeeSelectList = new SelectList(await _employeeMasterData.GetAllEmployee(), "EmpId", "FirstName");
            return View(model);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}