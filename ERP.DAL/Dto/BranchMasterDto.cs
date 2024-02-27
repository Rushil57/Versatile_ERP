﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class BranchMasterDto
    {
        public int Id { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime? Modified_Date { get; set; }
        public bool IsActive { get; set; }
    }
}
