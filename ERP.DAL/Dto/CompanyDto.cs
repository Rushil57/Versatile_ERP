using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class CompanyDto
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string City { get; set; }
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
        public string PANNumber { get; set; }
    }
}
