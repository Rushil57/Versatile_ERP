﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class RolesMasterDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime? Modified_Date { get; set; }
        public bool IsActive { get; set; }
    }
}
