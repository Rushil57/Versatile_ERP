using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class PhoneCallsEntryDto
    {
        public int Id { get; set; }
        public string ACPartyName { get; set; }
        public string SalesPersonRef { get; set; }
        public string ModelName { get; set; }
        public string SerialNumber { get; set; }
        public DateTime Date { get; set; }
        public string AccountPartyName { get; set; }
        public string EmployeeName { get; set; }
        public string InventoryName { get; set; }
    }
}
