using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class InwardDto
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
    }
}
