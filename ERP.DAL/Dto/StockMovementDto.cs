﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class StockMovementDto
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public int BranchId { get; set; }
        public string TranCode { get; set; }
        public string TranId { get; set; }
        public int TranDetailId { get; set; }
        public int FYId { get; set; }
        public int UnitId { get; set; }
        public decimal TranRate { get; set; }
        public int TranQty { get; set; }
        public decimal TranAmount { get; set; }
        public string TranBook { get; set; }
        public string TranType { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Modified_Date { get; set; }
        public string InsertFrom { get; set; }

        // Extra Field
        public List<string> SerialNos { get; set; }
    }
}
