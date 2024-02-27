using ERP.DAL.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTPL_ERP.Models.Master
{
    public class RolesAndRightsMasterViewModel
    {
        public int RightsId { get; set; }
        public int RoleId { get; set; }
        public int ActionId { get; set; }
        public int PageId { get; set; }
        public bool IsRight { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime? Modified_Date { get; set; }
        public bool IsActive { get; set; }

        public SelectList RolesSelectList { get; set; }
        public SelectList PageSelectList { get; set; }
        public List<RoleActionMasterDto> ActionSelectList { get; set; }

        public int SelectedRoleId { get; set; }
        public int SelectedPageId { get; set; }
        public int SelectedActionId { get; set; }
        public List<int> SelectedActionIds { get; set; }
    }
}
