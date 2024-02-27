using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class PartyPaymentDebitEntryDetailsDto
    {
        public int Id { get; set; }
        public string ReceiptNo { get; set; }
        public string InvoiceNo { get; set; }
        public decimal Amount { get; set; }
    }
}
