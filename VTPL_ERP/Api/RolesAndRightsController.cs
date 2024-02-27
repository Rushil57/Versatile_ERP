using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VTPL_ERP.Controllers;
using ERP.DAL.Abstract;
using VTPL_ERP.Models.Master;
using VTPL_ERP.Util;
using ERP.DAL.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VTPL_ERP.Api
{
   // [Route("api/[controller]")]
    public class RolesAndRightsController : BaseController
    {
        private readonly IRolesAndRightsMasterService _rolesandrightsMasterData;
        private readonly IRoleActionMasterService _roleActionMasterData;
        public RolesAndRightsController(IRolesAndRightsMasterService rolesandrightsMasterData, IRoleActionMasterService roleActionMasterData)
        {
            _rolesandrightsMasterData = rolesandrightsMasterData;
            _roleActionMasterData = roleActionMasterData;
        }
        [Route("api/RolesAndRights/SaveRolesAndRights")]
        [HttpPost]
        public async Task<JsonResult> SaveRolesAndRights(RolesAndRightsMasterViewModel model)
        {
            JsonResponseData retObj = new JsonResponseData();
            try
            {
                await _rolesandrightsMasterData.DeleteRolesAndRightsByKeys(model.RoleId, model.PageId);
                RolesAndRightsMasterDto dtoObj = new RolesAndRightsMasterDto();
                var lastInsertedId = 0;
                for (int i = 0; i < model.ActionSelectList.Count; i++)
                {
                    dtoObj.Created_Date = DateTime.Now;
                    dtoObj.Modified_Date = DateTime.Now;
                    dtoObj.RightsId = model.RightsId;
                    dtoObj.RoleId = model.RoleId;
                    dtoObj.PageId = model.PageId;
                    dtoObj.IsActive = true;
                    dtoObj.ActionId = model.ActionSelectList[i].ActionId;
                    dtoObj.IsRight = model.ActionSelectList[i].IsRight;
                    lastInsertedId = await _rolesandrightsMasterData.InsertUpdateRolesAndRights(dtoObj);
                }
                    retObj.IsError = false;
                    retObj.SuccessMessage = "Data Saved Successfully!";
                    return Json(retObj);
            }
            catch (Exception ex)
            {
                retObj.IsError = true;
                retObj.ErrorMessage = ex.Message;
                return Json(retObj);
            }
        }
        [Route("api/RolesAndRights/GetRolesAndRights")]
        //[HttpGet("{id}")]
        public async Task<JsonResult> GetRolesAndRights(int RoleId, int PageId)
        {
            try
            {
                var allActionList = await _roleActionMasterData.GetAllRoleAction();
                var rolesRightsObj = await _rolesandrightsMasterData.GetRolesAndRights(RoleId, PageId);
                var dbActionList = new List<RoleActionMasterDto>();
                foreach (var item in allActionList)
                {
                    var dbValue = rolesRightsObj.Where(x => x.ActionId.Equals(item.ActionId)).FirstOrDefault();
                    dbActionList.Add(new RoleActionMasterDto {
                        ActionId = item.ActionId,
                        ActionName = item.ActionName,
                        Created_Date = item.Created_Date,
                        IsRight = dbValue == null ? false : dbValue.IsRight,
                    });
                }
                return Json(dbActionList);
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
    }
}
