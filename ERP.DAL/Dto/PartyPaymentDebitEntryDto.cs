using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class PartyPaymentDebitEntryDto
    {
        public string ReceiptNo { get; set; }
        public DateTime PaymentDate { get; set; }
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
        public string AccountPartyName { get; set; }
        public string EmployeeName { get; set; }
        public string PaymentType { get; set; }
        public string ChequeNo { get; set; }
        public DateTime ChequeDate { get; set; }
        public string Bank { get; set; }
        public string Branch { get; set; }
        public string Remarks { get; set; }
        public List<PartyPaymentDebitEntryDetailsDto> PartyPaymentDebitEntryDetailsData { get; set; }
    }
}
