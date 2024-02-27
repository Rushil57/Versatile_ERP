using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTPL_ERP.Models
{
    public class CompanyBankAccountViewModel
    {
        public int Id { get; set; }
        public string BankName { get; set; }
        public string CurrencyOfLedger { get; set; }
        public bool IsActivateInterestCalculation { get; set; }
        public decimal ODLimit { get; set; }
        public bool IsChequeBooks { get; set; }
        public bool IsChequePrintingConfg { get; set; }
        public string ACHolderName { get; set; }
        public string ACName { get; set; }
        public string IFSCCode { get; set; }
        public string Branch { get; set; }
        public decimal Balance { get; set; }
        public string AccountGroup { get; set; }
    }
}
