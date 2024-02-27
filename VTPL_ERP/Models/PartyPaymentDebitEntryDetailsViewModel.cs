using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTPL_ERP.Models
{
    public class PartyPaymentDebitEntryDetailsViewModel
    {
        public int Id { get; set; }
        public string ReceiptNo { get; set; }
        public string InvoiceNo { get; set; }
        public decimal Amount { get; set; }
    }
}
