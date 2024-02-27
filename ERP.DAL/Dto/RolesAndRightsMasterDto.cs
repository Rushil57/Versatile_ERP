using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class RolesAndRightsMasterDto
    {
        public int RightsId { get; set; }
        public int RoleId { get; set; }
        public int ActionId { get; set; }
        public int PageId { get; set; }
        public bool IsRight { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime? Modified_Date { get; set; }
        public bool IsActive { get; set; }
    }
}
