using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTPL_ERP.Models
{
    public class PhoneCallsEntryViewModel
    {
        public int Id { get; set; }
        public string ACPartyName { get; set; }
        public string SalesPersonRef { get; set; }
        public string ModelName { get; set; }
        public string SerialNumber { get; set; }
        public DateTime Date { get; set; }
        public string DateDisplay { get; set; }
        public string AccountPartyName { get; set; }
        public string EmployeeName { get; set; }
        public string InventoryName { get; set; }

        public SelectList AccountPartySelectList { get; set; }
        public SelectList EmployeeSelectList { get; set; }
        public SelectList InventorySelectList { get; set; }
        public SelectList PurchaseEntrySelectList { get; set; }

        public int SelectedAccountPartyId { get; set; }
        public int SelectedEmployeeId { get; set; }
        public int SelectedInventoryId { get; set; }
        public IEnumerable<SelectListItem> SelectedPurchaseEntryId { get; set; }
    }
}
