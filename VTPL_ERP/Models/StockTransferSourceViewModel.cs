﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTPL_ERP.Models
{
    public class StockTransferSourceViewModel
    {
        public string Id { get; set; }
        public int BranchId { get; set; }
        public int TotalQty { get; set; }
        public decimal TotalAmount { get; set; }
        public string Narration { get; set; }

        public string BranchName { get; set; }
        public SelectList BranchSelectList { get; set; }
    }
}
