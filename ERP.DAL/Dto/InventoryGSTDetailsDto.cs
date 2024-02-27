using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class InventoryGSTDetailsDto
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public string HSNCode { get; set; }
        public string CalculationType { get; set; }
        public string Taxability { get; set; }
        public decimal IntegratedTax { get; set; }
        public decimal CentralTax { get; set; }
        public decimal StateTax { get; set; }
    }
}
