using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class OutwardDto
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
        public string CourierName { get; set; }
    }
}
