﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTPL_ERP.Models.Master
{
    public class CourierMasterViewModel
    {
        public int Id { get; set; }
        public string CourierName { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string Mobile { get; set; }
        public string Contact { get; set; }
        public string Remarks { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime? Modified_Date { get; set; }
        public bool IsActive { get; set; }
    }
}
