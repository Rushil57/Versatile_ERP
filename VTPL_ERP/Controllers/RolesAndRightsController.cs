using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.DAL.Abstract;
using VTPL_ERP.Models.Master;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VTPL_ERP.Controllers
{
    public class RolesAndRightsController : BaseController
    {
        private readonly IRolesMasterService _rolesMasterData;
        private readonly IRoleActionMasterService _roleActionMasterData;
        private readonly IPageMasterService _pageMasterData;
        public RolesAndRightsController(IRolesMasterService rolesMasterData, IRoleActionMasterService roleActionMasterData, IPageMasterService pageMasterData)
        {
            _rolesMasterData = rolesMasterData;
            _roleActionMasterData = roleActionMasterData;
            _pageMasterData = pageMasterData;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> RolesAndRights()
        {
            RolesAndRightsMasterViewModel model = new RolesAndRightsMasterViewModel();
            model.RolesSelectList = new SelectList(await _rolesMasterData.GetAllRole(), "RoleId", "RoleName");
            model.PageSelectList = new SelectList(await _pageMasterData.GetAllPage(), "Id", "PageName");
            model.ActionSelectList = await _roleActionMasterData.GetAllRoleAction();
            return View(model);
        }
    }
}