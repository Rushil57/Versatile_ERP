using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTPL_ERP.Models
{
    public class OutwardViewModel
    {
        public int OutwardId { get; set; }
        public DateTime OutwardDate { get; set; }
        public string OutwardDateDisplay { get; set; }
        public int InwardId { get; set; }
        public int CourierId { get; set; }
        public DateTime CourierDate { get; set; }
        public string CourierDateDisplay { get; set; }
        public string DocumentNo { get; set; }
        public decimal Charges { get; set; }
        public string ImagePath { get; set; }
        public IFormFile File { get; set; }
        public string CourierName { get; set; }

        public string AccountPartyName { get; set; }
        public string InventoryName { get; set; }
        public string BrandName { get; set; }
        public string SerialNo { get; set; }

        public SelectList CourierSelectList { get; set; }
        public SelectList InwardSelectList { get; set; }
    }
}
