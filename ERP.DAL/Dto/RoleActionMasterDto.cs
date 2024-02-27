using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class RoleActionMasterDto
    {
        public int ActionId { get; set; }
        public string ActionName { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime? Modified_Date { get; set; }
        public bool IsActive { get; set; }
        public bool IsRight { get; set; }
    }
}
