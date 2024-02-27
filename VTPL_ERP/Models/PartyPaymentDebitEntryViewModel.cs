using ERP.DAL.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTPL_ERP.Models
{
    public class PartyPaymentDebitEntryViewModel
    {
        public string ReceiptNo { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentDateString { get; set; }
        public string PaymentDateDisplay { get; set; }
        public int CompanyBankId { get; set; }
        public int AccountPartyId { get; set; }
        public string SalesEntryId { get; set; }
        public decimal Amount { get; set; }
        public string Narration { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime? Modified_Date { get; set; }
        public decimal APCurrentBalance { get; set; }
        public string AgstRef { get; set; }
        public decimal CompanyCurrentBalance { get; set; }
        public decimal TotalPaidAmount { get; set; }
        public string PaymentType { get; set; }
        public string ChequeNo { get; set; }
        public DateTime ChequeDate { get; set; }
        public string ChequeDateString { get; set; }
        public string ChequeDateDisplay { get; set; }
        public string Bank { get; set; }
        public string Branch { get; set; }
        public string Remarks { get; set; }
        public string AccountPartyName { get; set; }
        public string EmployeeName { get; set; }
        public List<SalesEntryDto> SalesEntryData { get; set; }
        public string PageMode { get; set; }

        public SelectList AccountPartySelectList { get; set; }
        public SelectList EmployeeSelectList { get; set; }
        public SelectList CompanyBankSelectList { get; set; }

        public int SelectedAccountPartyId { get; set; }
        public int SelectedEmployeeId { get; set; }
        public int SelectedCompanyBankId { get; set; }

        public List<PartyPaymentDebitEntryDetailsDto> PartyPaymentDebitEntryDetailsData { get; set; }

        public List<PartyPaymentDebitEntryDetailsViewModel> PartyPaymentDebitEntryDetails { get; set; }
    }
}
