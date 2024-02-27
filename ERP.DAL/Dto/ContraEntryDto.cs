using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class ContraEntryDto
    {
        public string CONo { get; set; }
        public int SourceBankId { get; set; }
        public decimal CurrentBalance { get; set; }
        public int DestinationBankId { get; set; }
        public decimal Amount { get; set; }
        public DateTime? ContraDate { get; set; }

        public string SourceBankName { get; set; }
        public string DestinationBankName { get; set; }
    }
}
