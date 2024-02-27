using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTPL_ERP.Models
{
    public class ContraEntryViewModel
    {
        public string CONo { get; set; }
        public int SourceBankId { get; set; }
        public decimal CurrentBalance { get; set; }
        public int DestinationBankId { get; set; }
        public decimal Amount { get; set; }
        public DateTime ContraDate { get; set; }
        public string ContraDateString { get; set; }
        public string ContraDateDisplay { get; set; }

        public string SourceBankName { get; set; }
        public string DestinationBankName { get; set; }

        public SelectList SourceBankSelectList { get; set; }
        public SelectList DestinationBankSelectList { get; set; }
    }
}
