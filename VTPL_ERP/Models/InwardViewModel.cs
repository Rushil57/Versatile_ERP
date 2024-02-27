using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTPL_ERP.Models
{
    public class InwardViewModel
    {
        public int InwardId { get; set; }
        public DateTime InwardDate { get; set; }
        public string InwardDateDisplay { get; set; }
        public int ACPartyId { get; set; }
        public int InventoryId { get; set; }
        public int BrandId { get; set; }
        public string SerialNo { get; set; }
        public int ProblemId { get; set; }
        public decimal ApproxCharge { get; set; }
        public string ImagePath { get; set; }
        public string AccountPartyName { get; set; }
        public string InventoryName { get; set; }
        public string BrandName { get; set; }
        public string ProblemName { get; set; }
        public IFormFile File { get; set; }

        public SelectList AccountPartySelectList { get; set; }
        public SelectList InventorySelectList { get; set; }
        public SelectList InventoryBrandSelectList { get; set; }
        public SelectList ProblemSelectList { get; set; }

        public int SelectedACPartyId { get; set; }
        public int SelectedInventoryId { get; set; }
        public int SelectedBrandId { get; set; }
        public int SelectedProblemId { get; set; }
    }
}
