using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTPL_ERP.Models
{
    public class CompanyViewModel
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string City { get; set; }
        public string StatePrefix { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string ContactNo { get; set; }
        public string GSTNo { get; set; }
        public string CIN { get; set; }
        public string Email { get; set; }
        public string CompanyStateName { get; set; }
        public string MailingName { get; set; }
        public string PhoneNo { get; set; }
        public string FAXNo { get; set; }
        public string Website { get; set; }
        public DateTime? FinancialYearBegins { get; set; }
        public DateTime? BooksBeginningsFrom { get; set; }
        public string FinancialYearBeginsDisplay { get; set; }
        public string BooksBeginningsFromDisplay { get; set; }
        public string PANNumber { get; set; }
        public string FinancialYearBeginsString { get; set; }
        public string BooksBeginningsFromString { get; set; }
        public SelectList StateSelectList { get; set; }
        public int StateId { get; set; }
    }
}
