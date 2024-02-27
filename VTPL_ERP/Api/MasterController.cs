using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VTPL_ERP.Models.Master;
using ERP.DAL.Abstract;
using ERP.DAL.Dto;
using VTPL_ERP.Util;
using VTPL_ERP.Models;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Security.Cryptography;
using VTPL_ERP.Controllers;
using static VTPL_ERP.Util.AppConstants;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VTPL_ERP.Api
{
    //[Route("api/[Master]")]
    public class MasterController : BaseController
    {
        private readonly IUnitMasterService _unitMasterData;
        private readonly ICourierMasterService _courierMasterData;
        private readonly IBranchMasterService _branchMasterData;
        private readonly IProblemMasterService _problemMasterData;
        private readonly IRolesMasterService _rolesMasterData;
        private readonly IOtherChargesMasterService _otherChargesMasterData;
        private readonly IRoleActionMasterService _roleActionMasterData;
        private readonly IInventoryBrandMasterService _inventoryBrandMasterData;
        private readonly IInventoryCategoryMasterService _inventoryCategoryMasterData;
        private readonly IAccountPartyMasterService _accountPartyMasterData;
        private readonly IEmployeeMasterService _employeeMasterData;
        private readonly IERP_CommonService _erpCommonServiceData;
        private readonly IUserService _userData;
        private readonly IInventoryMasterService _inventoryMasterData;
        private readonly IInventoryGSTDetailsService _inventoryGSTDetailsData;
        private readonly ITransactionService _transactionData;

        public MasterController(IUnitMasterService unitMasterData, ICourierMasterService courierMasterData, IBranchMasterService branchMasterData, IProblemMasterService problemMasterData, IRolesMasterService rolesMasterData, IOtherChargesMasterService otherChargesMasterData, IRoleActionMasterService roleActionMasterData, IInventoryBrandMasterService inventoryBrandMasterData, IInventoryCategoryMasterService inventoryCategoryMasterData,IAccountPartyMasterService accountPartyMasterData, IEmployeeMasterService employeeMasterData, IERP_CommonService erpCommonServiceData, IUserService userData, IInventoryMasterService inventoryMasterData, IInventoryGSTDetailsService inventoryGSTDetailsData, ITransactionService transactionData)
        {
            _unitMasterData = unitMasterData;
            _courierMasterData = courierMasterData;
            _branchMasterData = branchMasterData;
            _problemMasterData = problemMasterData;
            _rolesMasterData = rolesMasterData;
            _otherChargesMasterData = otherChargesMasterData;
            _roleActionMasterData = roleActionMasterData;
            _inventoryBrandMasterData = inventoryBrandMasterData;
            _inventoryCategoryMasterData = inventoryCategoryMasterData;
            _accountPartyMasterData = accountPartyMasterData;
            _employeeMasterData = employeeMasterData;
            _erpCommonServiceData = erpCommonServiceData;
            _userData = userData;
            _inventoryMasterData = inventoryMasterData;
            _inventoryGSTDetailsData = inventoryGSTDetailsData;
            _transactionData = transactionData;
        }

        #region Unit MASTER
        [Route("api/Master/GetAllUnit")]
        [HttpGet]
        public async Task<JsonResult> GetAllUnit()
        {
            var unitlist = await _unitMasterData.GetAllUnit();
            return Json(unitlist);
        }

        [Route("api/Master/GetUnitByKey")]
        //[HttpGet("{id}")]
        public async Task<JsonResult> GetUnitByKey(int id)
        {
            var unitObj = await _unitMasterData.GetUnitByKey(id);
            return Json(unitObj);
        }

        [Route("api/Master/DeleteUnitByKey")]
        //[HttpDelete("{id}")]
        public async Task<JsonResult> DeleteUnitByKey(int id)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.Unit_Master, RoleActions.Delete);
            if (hasRights == true)
            {
                try
                {
                    await _unitMasterData.DeleteUnitByKey(id);
                    retObj.IsError = false;
                    retObj.SuccessMessage = "Data deleted Successfully!";
                    return Json(retObj);
                }
                catch (Exception ex)
                {
                    retObj.IsError = true;
                    retObj.ErrorMessage = ex.Message;
                    return Json(retObj);
                }
            }
            else
            {
                retObj.IsError = true;
                retObj.ErrorMessage = AppConstants.Messages.Not_Authorized;
                return Json(retObj);
            }
        }
        [Route("api/Master/SaveUnit")]
        [HttpPost]
        public async Task<JsonResult> SaveUnit(UnitMasterViewModel model)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.Unit_Master, RoleActions.InsertUpdate);
            if (hasRights == true)
            {
                try
                {
                    UnitMasterDto dtoObj = new UnitMasterDto();
                    if (model.Id == 0)
                    {
                        dtoObj.Created_Date = DateTime.Now;
                    }
                    else
                    {
                        dtoObj.Created_Date = model.Created_Date;
                        dtoObj.Modified_Date = DateTime.Now;
                    }
                    dtoObj.Id = model.Id;
                    dtoObj.UnitName = model.UnitName;
                    dtoObj.Alias = model.Alias;
                    dtoObj.IsActive = true;
                    var lastInsertedId = await _unitMasterData.InsertUpdateUnit(dtoObj);
                    if (lastInsertedId > 0)
                    {
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data Saved Successfully!";
                    }
                    else if (lastInsertedId == -1)
                    {
                        retObj.IsError = true;
                        retObj.ErrorMessage = "Unit name already exsist !";
                    }
                    else if (model.Id > 0)
                    {
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data Updated Successfully!";
                    }

                    return Json(retObj);
                }
                catch (Exception ex)
                {
                    retObj.IsError = true;
                    retObj.ErrorMessage = ex.Message;
                    return Json(retObj);
                }
            }
            else
            {
                retObj.IsError = true;
                retObj.ErrorMessage = AppConstants.Messages.Not_Authorized;
                return Json(retObj);
            }
        }
        #endregion

        #region Courier MASTER
        [Route("api/Master/GetAllCourier")]
        [HttpGet]
        public async Task<JsonResult> GetAllCourier()
        {
            var courierlist = await _courierMasterData.GetAllCourier();
            return Json(courierlist);
        }

        [Route("api/Master/GetCourierByKey")]
        //[HttpGet("{id}")]
        public async Task<JsonResult> GetCourierByKey(int id)
        {
            var courierObj = await _courierMasterData.GetCourierByKey(id);
            return Json(courierObj);
        }

        [Route("api/Master/DeleteCourierByKey")]
        //[HttpDelete("{id}")]
        public async Task<JsonResult> DeleteCourierByKey(int id)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.Courier_Master, RoleActions.Delete);
            if (hasRights == true)
            {
                try
                {
                    await _courierMasterData.DeleteCourierByKey(id);
                    retObj.IsError = false;
                    retObj.SuccessMessage = "Data deleted Successfully!";
                    return Json(retObj);
                }
                catch (Exception ex)
                {
                    retObj.IsError = true;
                    retObj.ErrorMessage = ex.Message;
                    return Json(retObj);
                }
            }
            else
            {
                retObj.IsError = true;
                retObj.ErrorMessage = AppConstants.Messages.Not_Authorized;
                return Json(retObj);
            }
        }
        [Route("api/Master/SaveCourier")]
        [HttpPost]
        public async Task<JsonResult> SaveCourier(CourierMasterViewModel model)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.Courier_Master, RoleActions.InsertUpdate);
            if (hasRights == true)
            {
                try
                {
                    CourierMasterDto dtoObj = new CourierMasterDto();
                    if (model.Id == 0)
                    {
                        dtoObj.Created_Date = DateTime.Now;
                    }
                    else
                    {
                        dtoObj.Created_Date = model.Created_Date;
                        dtoObj.Modified_Date = DateTime.Now;
                    }
                    dtoObj.Id = model.Id;
                    dtoObj.CourierName = model.CourierName;
                    dtoObj.Address = model.Address;
                    dtoObj.Website = model.Website;
                    dtoObj.Mobile = model.Mobile;
                    dtoObj.Contact = model.Contact;
                    dtoObj.Remarks = model.Remarks;
                    dtoObj.IsActive = true;
                    var lastInsertedId = await _courierMasterData.InsertUpdateCourier(dtoObj);
                    if (lastInsertedId > 0)
                    {
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data Saved Successfully!";
                    }
                    else if (lastInsertedId == -1)
                    {
                        retObj.IsError = true;
                        retObj.ErrorMessage = "Courier name already exsist !";
                    }
                    else if (model.Id > 0)
                    {
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data Updated Successfully!";
                    }

                    return Json(retObj);
                }
                catch (Exception ex)
                {
                    retObj.IsError = true;
                    retObj.ErrorMessage = ex.Message;
                    return Json(retObj);
                }
            }
            else
            {
                retObj.IsError = true;
                retObj.ErrorMessage = AppConstants.Messages.Not_Authorized;
                return Json(retObj);
            }
        }
#endregion

        #region BRANCH MASTER
        // GET: api/values
        [Route("api/Master/GetAllBranchData")]
        [HttpGet]
        public async Task<JsonResult> GetAllBranchData()
        {
            var branchList = await _branchMasterData.GetAllBranch();
            return Json(branchList);
        }

        // GET api/values/5
        [Route("api/Master/GetBranchByKey")]
        [HttpGet("{id}")]
        public async Task<JsonResult> GetBranchByKey(int id)
        {
            var branchObj = await _branchMasterData.GetBranchByKey(id);
            return Json(branchObj);
        }

        [Route("api/Master/DeleteBranchByKey")]
        public async Task<JsonResult> DeleteBranchByKey(int id)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.Branch_Master, RoleActions.Delete);
            if (hasRights == true)
            {
                try
                {
                    await _branchMasterData.DeleteBranchByKey(id);
                    retObj.IsError = false;
                    retObj.SuccessMessage = "Data deleted Successfully!";
                    return Json(retObj);
                }
                catch (Exception ex)
                {
                    retObj.IsError = true;
                    retObj.ErrorMessage = ex.Message;
                    return Json(retObj);
                }
            }
            else
            {
                retObj.IsError = true;
                retObj.ErrorMessage = AppConstants.Messages.Not_Authorized;
                return Json(retObj);
            }
        }
        [Route("api/Master/SaveBranch")]
        [HttpPost]
        public async Task<JsonResult> SaveBranch(BranchMasterViewModel model)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.Branch_Master, RoleActions.InsertUpdate);
            if (hasRights == true)
            {
                try
                {
                    BranchMasterDto dtoObj = new BranchMasterDto();
                    if (model.Id == 0)
                    {
                        dtoObj.Created_Date = DateTime.Now;
                    }
                    else
                    {
                        dtoObj.Created_Date = model.Created_Date;
                        dtoObj.Modified_Date = DateTime.Now;
                    }
                    dtoObj.Id = model.Id;
                    dtoObj.BranchName = model.BranchName;
                    dtoObj.Address = model.Address;
                    dtoObj.Contact = model.Contact;
                    dtoObj.IsActive = true;
                    var lastInsertedId = await _branchMasterData.InsertUpdateBranch(dtoObj);
                    if (lastInsertedId > 0)
                    {
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data Saved Successfully!";
                    }
                    else if (lastInsertedId == -1)
                    {
                        retObj.IsError = true;
                        retObj.ErrorMessage = "Branch name already exsist !";
                    }
                    else if (model.Id > 0)
                    {
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data Updated Successfully!";
                    }

                    return Json(retObj);
                }
                catch (Exception ex)
                {
                    retObj.IsError = true;
                    retObj.ErrorMessage = ex.Message;
                    return Json(retObj);
                }
            }
            else
            {
                retObj.IsError = true;
                retObj.ErrorMessage = AppConstants.Messages.Not_Authorized;
                return Json(retObj);
            }
        }
        #endregion

        #region Problem MASTER
        [Route("api/Master/GetAllProblem")]
        [HttpGet]
        public async Task<JsonResult> GetAllProblem()
        {
            var problemlist = await _problemMasterData.GetAllProblem();
            return Json(problemlist);
        }

        [Route("api/Master/GetProblemByKey")]
        //[HttpGet("{id}")]
        public async Task<JsonResult> GetProblemByKey(int id)
        {
            var problemObj = await _problemMasterData.GetProblemByKey(id);
            return Json(problemObj);
        }

        [Route("api/Master/DeleteProblemByKey")]
        //[HttpDelete("{id}")]
        public async Task<JsonResult> DeleteProblemByKey(int id)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.Problem_Master, RoleActions.Delete);
            if (hasRights == true)
            {
                try
                {
                    await _problemMasterData.DeleteProblemByKey(id);
                    retObj.IsError = false;
                    retObj.SuccessMessage = "Data deleted Successfully!";
                    return Json(retObj);
                }
                catch (Exception ex)
                {
                    retObj.IsError = true;
                    retObj.ErrorMessage = ex.Message;
                    return Json(retObj);
                }
            }
            else
            {
                retObj.IsError = true;
                retObj.ErrorMessage = AppConstants.Messages.Not_Authorized;
                return Json(retObj);
            }
        }
        [Route("api/Master/SaveProblem")]
        [HttpPost]
        public async Task<JsonResult> SaveProblem(ProblemMasterViewModel model)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.Problem_Master, RoleActions.InsertUpdate);
            if (hasRights == true)
            {
                try
                {
                    ProblemMasterDto dtoObj = new ProblemMasterDto();
                    if (model.Id == 0)
                    {
                        dtoObj.Created_Date = DateTime.Now;
                    }
                    else
                    {
                        dtoObj.Created_Date = model.Created_Date;
                        dtoObj.Modified_Date = DateTime.Now;
                    }
                    dtoObj.Id = model.Id;
                    dtoObj.ProblemName = model.ProblemName;
                    dtoObj.Description = model.Description;
                    dtoObj.IsActive = true;
                    var lastInsertedId = await _problemMasterData.InsertUpdateProblem(dtoObj);
                    if (lastInsertedId > 0)
                    {
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data Saved Successfully!";
                    }
                    else if (lastInsertedId == -1)
                    {
                        retObj.IsError = true;
                        retObj.ErrorMessage = "Problem name already exsist !";
                    }
                    else if (model.Id > 0)
                    {
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data Updated Successfully!";
                    }

                    return Json(retObj);
                }
                catch (Exception ex)
                {
                    retObj.IsError = true;
                    retObj.ErrorMessage = ex.Message;
                    return Json(retObj);
                }
            }
            else
            {
                retObj.IsError = true;
                retObj.ErrorMessage = AppConstants.Messages.Not_Authorized;
                return Json(retObj);
            }
        }
        #endregion

        #region Roles MASTER
        [Route("api/Master/GetAllRole")]
        [HttpGet]
        public async Task<JsonResult> GetAllRole()
        {
            var rolelist = await _rolesMasterData.GetAllRole();
            return Json(rolelist);
        }

        [Route("api/Master/GetRoleByKey")]
        //[HttpGet("{id}")]
        public async Task<JsonResult> GetRoleByKey(int id)
        {
            var roleObj = await _rolesMasterData.GetRoleByKey(id);
            return Json(roleObj);
        }

        [Route("api/Master/DeleteRoleByKey")]
        //[HttpDelete("{id}")]
        public async Task<JsonResult> DeleteRoleByKey(int id)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.Roles_Master, RoleActions.Delete);
            if (hasRights == true)
            {
                try
                {
                    await _rolesMasterData.DeleteRoleByKey(id);
                    retObj.IsError = false;
                    retObj.SuccessMessage = "Data deleted Successfully!";
                    return Json(retObj);
                }
                catch (Exception ex)
                {
                    retObj.IsError = true;
                    retObj.ErrorMessage = ex.Message;
                    return Json(retObj);
                }
            }
            else
            {
                retObj.IsError = true;
                retObj.ErrorMessage = AppConstants.Messages.Not_Authorized;
                return Json(retObj);
            }
        }
        [Route("api/Master/SaveRole")]
        [HttpPost]
        public async Task<JsonResult> SaveRole(RolesMasterViewModel model)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.Roles_Master, RoleActions.InsertUpdate);
            if (hasRights == true)
            {
                try
                {
                    RolesMasterDto dtoObj = new RolesMasterDto();
                    if (model.RoleId == 0)
                    {
                        dtoObj.Created_Date = DateTime.Now;
                    }
                    else
                    {
                        dtoObj.Created_Date = model.Created_Date;
                        dtoObj.Modified_Date = DateTime.Now;
                    }
                    dtoObj.RoleId = model.RoleId;
                    dtoObj.RoleName = model.RoleName;
                    dtoObj.Description = model.Description;
                    dtoObj.IsActive = true;
                    var lastInsertedId = await _rolesMasterData.InsertUpdateRole(dtoObj);
                    if (lastInsertedId > 0)
                    {
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data Saved Successfully!";
                    }
                    else if (lastInsertedId == -1)
                    {
                        retObj.IsError = true;
                        retObj.ErrorMessage = "Role name already exsist !";
                    }
                    else if (model.RoleId > 0)
                    {
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data Updated Successfully!";
                    }

                    return Json(retObj);
                }
                catch (Exception ex)
                {
                    retObj.IsError = true;
                    retObj.ErrorMessage = ex.Message;
                    return Json(retObj);
                }
            }
            else
            {
                retObj.IsError = true;
                retObj.ErrorMessage = AppConstants.Messages.Not_Authorized;
                return Json(retObj);
            }
        }
        #endregion

        #region OtherCharges MASTER
        [Route("api/Master/GetAllOtherCharges")]
        [HttpGet]
        public async Task<JsonResult> GetAllOtherCharges()
        {
            var problemlist = await _otherChargesMasterData.GetAllOtherCharges();
            return Json(problemlist);
        }

        [Route("api/Master/GetOtherChargesByKey")]
        //[HttpGet("{id}")]
        public async Task<JsonResult> GetOtherChargesByKey(int id)
        {
            var otherchargesObj = await _otherChargesMasterData.GetOtherChargesByKey(id);
            return Json(otherchargesObj);
        }

        [Route("api/Master/DeleteOtherChargesByKey")]
        //[HttpDelete("{id}")]
        public async Task<JsonResult> DeleteOtherChargesByKey(int id)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.OtherCharges_Master, RoleActions.Delete);
            if (hasRights == true)
            {
                try
                {
                    await _otherChargesMasterData.DeleteOtherChargesByKey(id);
                    retObj.IsError = false;
                    retObj.SuccessMessage = "Data deleted Successfully!";
                    return Json(retObj);
                }
                catch (Exception ex)
                {
                    retObj.IsError = true;
                    retObj.ErrorMessage = ex.Message;
                    return Json(retObj);
                }
            }
            else
            {
                retObj.IsError = true;
                retObj.ErrorMessage = AppConstants.Messages.Not_Authorized;
                return Json(retObj);
            }
        }
        [Route("api/Master/SaveOtherCharges")]
        [HttpPost]
        public async Task<JsonResult> SaveOtherCharges(OtherChargesMasterViewModel model)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.OtherCharges_Master, RoleActions.InsertUpdate);
            if (hasRights == true)
            {
                try
                {
                    OtherChargesMasterDto dtoObj = new OtherChargesMasterDto();
                    //if (model.Id == 0)
                    //{
                    //    dtoObj.Created_Date = DateTime.Now;
                    //}
                    //else
                    //{
                    //    dtoObj.Created_Date = model.Created_Date;
                    //    dtoObj.Modified_Date = DateTime.Now;
                    //}
                    dtoObj.Id = model.Id;
                    dtoObj.Name = model.Name;
                    dtoObj.Rate = model.Rate;
                    dtoObj.IsActive = true;
                    var lastInsertedId = await _otherChargesMasterData.InsertUpdateOtherCharges(dtoObj);
                    if (lastInsertedId > 0)
                    {
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data Saved Successfully!";
                    }
                    else if (lastInsertedId == -1)
                    {
                        retObj.IsError = true;
                        retObj.ErrorMessage = "Name already exsist !";
                    }
                    else if (model.Id > 0)
                    {
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data Updated Successfully!";
                    }

                    return Json(retObj);
                }
                catch (Exception ex)
                {
                    retObj.IsError = true;
                    retObj.ErrorMessage = ex.Message;
                    return Json(retObj);
                }
            }
            else
            {
                retObj.IsError = true;
                retObj.ErrorMessage = AppConstants.Messages.Not_Authorized;
                return Json(retObj);
            }
        }
        #endregion

        #region RoleAction MASTER
        [Route("api/Master/GetAllRoleAction")]
        [HttpGet]
        public async Task<JsonResult> GetAllRoleAction()
        {
            var roleActionlist = await _roleActionMasterData.GetAllRoleAction();
            return Json(roleActionlist);
        }

        [Route("api/Master/GetRoleActionByKey")]
        //[HttpGet("{id}")]
        public async Task<JsonResult> GetRoleActionByKey(int id)
        {
            var roleActionObj = await _roleActionMasterData.GetRoleActionByKey(id);
            return Json(roleActionObj);
        }

        [Route("api/Master/DeleteRoleActionByKey")]
        //[HttpDelete("{id}")]
        public async Task<JsonResult> DeleteRoleActionByKey(int id)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.RoleAction_Master, RoleActions.Delete);
            if (hasRights == true)
            {
                try
                {
                    await _roleActionMasterData.DeleteRoleActionByKey(id);
                    retObj.IsError = false;
                    retObj.SuccessMessage = "Data deleted Successfully!";
                    return Json(retObj);
                }
                catch (Exception ex)
                {
                    retObj.IsError = true;
                    retObj.ErrorMessage = ex.Message;
                    return Json(retObj);
                }
            }
            else
            {
                retObj.IsError = true;
                retObj.ErrorMessage = AppConstants.Messages.Not_Authorized;
                return Json(retObj);
            }
        }
        [Route("api/Master/SaveRoleAction")]
        [HttpPost]
        public async Task<JsonResult> SaveRoleAction(RoleActionMasterViewModel model)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.RoleAction_Master, RoleActions.InsertUpdate);
            if (hasRights == true)
            {
                try
                {
                    RoleActionMasterDto dtoObj = new RoleActionMasterDto();
                    if (model.ActionId == 0)
                    {
                        dtoObj.Created_Date = DateTime.Now;
                    }
                    else
                    {
                        dtoObj.Created_Date = model.Created_Date;
                        dtoObj.Modified_Date = DateTime.Now;
                    }
                    dtoObj.ActionId = model.ActionId;
                    dtoObj.ActionName = model.ActionName;
                    dtoObj.IsActive = true;
                    var lastInsertedId = await _roleActionMasterData.InsertUpdateRoleAction(dtoObj);
                    if (lastInsertedId > 0)
                    {
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data Saved Successfully!";
                    }
                    else if (lastInsertedId == -1)
                    {
                        retObj.IsError = true;
                        retObj.ErrorMessage = "Action name already exsist !";
                    }
                    else if (model.ActionId > 0)
                    {
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data Updated Successfully!";
                    }

                    return Json(retObj);
                }
                catch (Exception ex)
                {
                    retObj.IsError = true;
                    retObj.ErrorMessage = ex.Message;
                    return Json(retObj);
                }
            }
            else
            {
                retObj.IsError = true;
                retObj.ErrorMessage = AppConstants.Messages.Not_Authorized;
                return Json(retObj);
            }
        }
        #endregion

        #region InventoryBrand MASTER
        [Route("api/Master/GetAllInventoryBrand")]
        [HttpGet]
        public async Task<JsonResult> GetAllInventoryBrand()
        {
            var inventoryBrandlist = await _inventoryBrandMasterData.GetAllInventoryBrand();
            return Json(inventoryBrandlist);
        }

        [Route("api/Master/GetInventoryBrandByKey")]
        //[HttpGet("{id}")]
        public async Task<JsonResult> GetInventoryBrandByKey(int id)
        {
            var brandObj = await _inventoryBrandMasterData.GetInventoryBrandByKey(id);
            return Json(brandObj);
        }

        [Route("api/Master/DeleteInventoryBrandByKey")]
        //[HttpDelete("{id}")]
        public async Task<JsonResult> DeleteInventoryBrandByKey(int id)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.InventoryBrand_Master, RoleActions.Delete);
            if (hasRights == true)
            {
                try
                {
                    await _inventoryBrandMasterData.DeleteInventoryBrandByKey(id);
                    retObj.IsError = false;
                    retObj.SuccessMessage = "Data deleted Successfully!";
                    return Json(retObj);
                }
                catch (Exception ex)
                {
                    retObj.IsError = true;
                    retObj.ErrorMessage = ex.Message;
                    return Json(retObj);
                }
            }
            else
            {
                retObj.IsError = true;
                retObj.ErrorMessage = AppConstants.Messages.Not_Authorized;
                return Json(retObj);
            }
        }
        [Route("api/Master/SaveInventoryBrand")]
        [HttpPost]
        public async Task<JsonResult> SaveInventoryBrand(InventoryBrandMasterViewModel model)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.InventoryBrand_Master, RoleActions.InsertUpdate);
            if (hasRights == true)
            {
                try
                {
                    InventoryBrandMasterDto dtoObj = new InventoryBrandMasterDto();
                    if (model.Id == 0)
                    {
                        dtoObj.Created_Date = DateTime.Now;
                    }
                    else
                    {
                        dtoObj.Created_Date = model.Created_Date;
                        dtoObj.Modified_Date = DateTime.Now;
                    }
                    dtoObj.Id = model.Id;
                    dtoObj.BrandName = model.BrandName;
                    dtoObj.IsActive = true;
                    var lastInsertedId = await _inventoryBrandMasterData.InsertUpdateInventoryBrand(dtoObj);
                    if (lastInsertedId > 0)
                    {
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data Saved Successfully!";
                    }
                    else if (lastInsertedId == -1)
                    {
                        retObj.IsError = true;
                        retObj.ErrorMessage = "Brand name already exsist !";
                    }
                    else if (model.Id > 0)
                    {
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data Updated Successfully!";
                    }

                    return Json(retObj);
                }
                catch (Exception ex)
                {
                    retObj.IsError = true;
                    retObj.ErrorMessage = ex.Message;
                    return Json(retObj);
                }
            }
            else
            {
                retObj.IsError = true;
                retObj.ErrorMessage = AppConstants.Messages.Not_Authorized;
                return Json(retObj);
            }
        }
        #endregion

        #region InventoryCategory MASTER
        [Route("api/Master/GetAllInventoryCategory")]
        [HttpGet]
        public async Task<JsonResult> GetAllInventoryCategory()
        {
            var inventoryCategorylist = await _inventoryCategoryMasterData.GetAllInventoryCategory();
            return Json(inventoryCategorylist);
        }

        [Route("api/Master/GetInventoryCategoryByKey")]
        //[HttpGet("{id}")]
        public async Task<JsonResult> GetInventoryCategoryByKey(int id)
        {
            var categoryObj = await _inventoryCategoryMasterData.GetInventoryCategoryByKey(id);
            return Json(categoryObj);
        }

        [Route("api/Master/DeleteInventoryCategoryByKey")]
        //[HttpDelete("{id}")]
        public async Task<JsonResult> DeleteInventoryCategoryByKey(int id)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.InventoryCategory_Master, RoleActions.Delete);
            if (hasRights == true)
            {
                try
                {
                    await _inventoryCategoryMasterData.DeleteInventoryCategoryByKey(id);
                    retObj.IsError = false;
                    retObj.SuccessMessage = "Data deleted Successfully!";
                    return Json(retObj);
                }
                catch (Exception ex)
                {
                    retObj.IsError = true;
                    retObj.ErrorMessage = ex.Message;
                    return Json(retObj);
                }
            }
            else
            {
                retObj.IsError = true;
                retObj.ErrorMessage = AppConstants.Messages.Not_Authorized;
                return Json(retObj);
            }
        }
        [Route("api/Master/SaveInventoryCategory")]
        [HttpPost]
        public async Task<JsonResult> SaveInventoryCategory(InventoryCategoryMasterViewModel model)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.InventoryCategory_Master, RoleActions.InsertUpdate);
            if (hasRights == true)
            {
                try
                {
                    InventoryCategoryMasterDto dtoObj = new InventoryCategoryMasterDto();
                    if (model.Id == 0)
                    {
                        dtoObj.Created_Date = DateTime.Now;
                    }
                    else
                    {
                        dtoObj.Created_Date = model.Created_Date;
                        dtoObj.Modified_Date = DateTime.Now;
                    }
                    dtoObj.Id = model.Id;
                    dtoObj.CategoryName = model.CategoryName;
                    dtoObj.IsActive = true;
                    var lastInsertedId = await _inventoryCategoryMasterData.InsertUpdateInventoryCategory(dtoObj);
                    if (lastInsertedId > 0)
                    {
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data Saved Successfully!";
                    }
                    else if (lastInsertedId == -1)
                    {
                        retObj.IsError = true;
                        retObj.ErrorMessage = "Category name already exsist !";
                    }
                    else if (model.Id > 0)
                    {
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data Updated Successfully!";
                    }

                    return Json(retObj);
                }
                catch (Exception ex)
                {
                    retObj.IsError = true;
                    retObj.ErrorMessage = ex.Message;
                    return Json(retObj);
                }
            }
            else
            {
                retObj.IsError = true;
                retObj.ErrorMessage = AppConstants.Messages.Not_Authorized;
                return Json(retObj);
            }
        }
        #endregion

        #region AccountParty MASTER
        [Route("api/Master/GetAllAccountParty")]
        [HttpGet]
        public async Task<JsonResult> GetAllAccountParty()
        {
            var accountPartylist = await _accountPartyMasterData.GetAllAccountParty();
            return Json(accountPartylist);
        }

        [Route("api/Master/GetAccountPartyByKey")]
        //[HttpGet("{id}")]
        public async Task<JsonResult> GetAccountPartyByKey(int id)
        {
            var accountpartyObj = await _accountPartyMasterData.GetAccountPartyByKey(id);
            return Json(accountpartyObj);
        }

        [Route("api/Master/DeleteAccountPartyByKey")]
        //[HttpDelete("{id}")]
        public async Task<JsonResult> DeleteAccountPartyByKey(int id)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.AccountParty_Master, RoleActions.Delete);
            if (hasRights == true)
            {
                try
                {
                    await _accountPartyMasterData.DeleteAccountPartyByKey(id);
                    retObj.IsError = false;
                    retObj.SuccessMessage = "Data deleted Successfully!";
                    return Json(retObj);
                }
                catch (Exception ex)
                {
                    retObj.IsError = true;
                    retObj.ErrorMessage = ex.Message;
                    return Json(retObj);
                }
            }
            else
            {
                retObj.IsError = true;
                retObj.ErrorMessage = AppConstants.Messages.Not_Authorized;
                return Json(retObj);
            }
        }
        [Route("api/Master/SaveAccountParty")]
        [HttpPost]
        public async Task<JsonResult> SaveAccountParty(AccountPartyMasterViewModel model)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.AccountParty_Master, RoleActions.InsertUpdate);
            if (hasRights == true)
            {
                try
                {
                    AccountPartyMasterDto dtoObj = new AccountPartyMasterDto();
                    if (model.Id == 0)
                    {
                        dtoObj.Created_Date = DateTime.Now;
                    }
                    else
                    {
                        dtoObj.Created_Date = model.Created_Date;
                        dtoObj.Modified_Date = DateTime.Now;
                    }
                    dtoObj.Id = model.Id;
                    dtoObj.Name = model.Name;
                    dtoObj.IsSundery = model.IsSundery;
                    //dtoObj.CurrencyOfLedger = model.CurrencyOfLedger;
                    dtoObj.IsMaintainBalanceByBill = model.IsMaintainBalanceByBill;
                    dtoObj.DefaultCreditPeriod = model.DefaultCreditPeriod;
                    dtoObj.CreditLimit = model.CreditLimit;
                    //dtoObj.IsInventoryValueEffected = model.IsInventoryValueEffected;
                    dtoObj.IsActivateInterestCalculation = model.IsActivateInterestCalculation;
                    if (!string.IsNullOrEmpty(model.OpeningDateString))
                    {
                        dtoObj.OpeningDate = new DateTime(Convert.ToInt32(model.OpeningDateString.Split("/")[2]), Convert.ToInt32(model.OpeningDateString.Split("/")[1]), Convert.ToInt32(model.OpeningDateString.Split("/")[0]));
                    }
                    
                    dtoObj.IsActive = true;
                    dtoObj.Email = model.Email;
                    dtoObj.MobileNo = model.MobileNo;
                    dtoObj.Interest = model.Interest;
                   // dtoObj.AccountType = model.AccountType;
                    dtoObj.Landline = model.Landline;
                    dtoObj.Email2 = model.Email2;
                    dtoObj.Contact = model.Contact;
                    dtoObj.ContactPerson1 = model.ContactPerson1;
                    dtoObj.ContactPerson2 = model.ContactPerson2;
                    dtoObj.MainAddress = model.MainAddress;
                    dtoObj.DeliveryAddress = model.DeliveryAddress;
                    dtoObj.DeliveryAddressContactPersonName = model.DeliveryAddressContactPersonName;
                    dtoObj.DeliveryAddressContactPersonNo = model.DeliveryAddressContactPersonNo;
                    dtoObj.InterestPeriod = model.InterestPeriod;
                    dtoObj.SalesPersonId = model.SalesPersonId;
                    dtoObj.PDC = model.PDC;
                    dtoObj.GSTDetails = model.GSTDetails;
                    dtoObj.GSTNumber = model.GSTNumber;
                    dtoObj.PANNumber = model.PANNumber;
                    dtoObj.State = model.State;
                    dtoObj.HSNCode = model.HSNCode;
                    dtoObj.TypesOfGood = model.TypesOfGood;
                    dtoObj.Taxability = model.Taxability;
                    dtoObj.IGST = model.IGST;
                    dtoObj.SGST = model.SGST;
                    dtoObj.CGST = model.CGST;
                    dtoObj.CurrencyOfLedger = model.CurrencyOfLedger;
                    dtoObj.OpeningBalance = model.OpeningBalance;
                    dtoObj.ACHolderName = model.ACHolderName;
                    dtoObj.ACName = model.ACName;
                    dtoObj.IFSCCode = model.IFSCCode;
                    dtoObj.BankName = model.BankName;
                    dtoObj.Branch = model.Branch;
                    dtoObj.BSRCode = model.BSRCode;
                    dtoObj.IsChequeBooks = model.IsChequeBooks;
                    dtoObj.IsChequePrintingConfg = model.IsChequePrintingConfg;
                    dtoObj.Country = model.Country;
                    dtoObj.PinCode = model.PinCode;
                    dtoObj.FAXNo = model.FAXNo;
                    dtoObj.CCEmail = model.CCEmail;
                    dtoObj.Website = model.Website;
                    dtoObj.SetServiceTaxDetails = model.SetServiceTaxDetails;
                    dtoObj.TypesOfDuty = model.TypesOfDuty;
                    dtoObj.TaxType = model.TaxType;
                    dtoObj.ODLimit = model.ODLimit;
                    var lastInsertedId = await _accountPartyMasterData.InsertUpdateAccountParty(dtoObj);
                    if (lastInsertedId > 0)
                    {
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data Saved Successfully!";
                    }
                    else if (lastInsertedId == -1)
                    {
                        retObj.IsError = true;
                        retObj.ErrorMessage = "Name already exsist !";
                    }
                    else if (model.Id > 0)
                    {
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data Updated Successfully!";
                    }

                    return Json(retObj);
                }
                catch (Exception ex)
                {
                    retObj.IsError = true;
                    retObj.ErrorMessage = ex.Message;
                    return Json(retObj);
                }
            }
            else
            {
                retObj.IsError = true;
                retObj.ErrorMessage = AppConstants.Messages.Not_Authorized;
                return Json(retObj);
            }
        }
        #endregion

        #region Employee MASTER
        [Route("api/Master/GetAllEmployee")]
        [HttpGet]
        public async Task<JsonResult> GetAllEmployee()
        {
            var emplist = await _employeeMasterData.GetAllEmployee();
            return Json(emplist);
        }

        [Route("api/Master/GetEmployeeByKey")]
        //[HttpGet("{id}")]
        public async Task<JsonResult> GetEmployeeByKey(string id)
        {
            var employeeObj = await _employeeMasterData.GetEmployeeByKey(id);
            if(employeeObj.UserPassword != null)
            {
                ERP_Utitlity erpUtil = new ERP_Utitlity();
                var encryptPswd = erpUtil.Decrypt(employeeObj.UserPassword);
                employeeObj.UserPassword = encryptPswd;
            }
            return Json(employeeObj);
        }

        [Route("api/Master/DeleteEmployeeByKey")]
        //[HttpDelete("{id}")]
        public async Task<JsonResult> DeleteEmployeeByKey(string id)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.Employee_Master, RoleActions.Delete);
            if (hasRights == true)
            {
                try
                {
                    var userObj = await _userData.GetUserByEmpId(id);
                    if (userObj != null)
                    {
                        await _userData.DeleteUserByKey(userObj.UserId);
                    }
                    await _employeeMasterData.DeleteEmployeeByKey(id);
                    retObj.IsError = false;
                    //retObj.SuccessMessage = "Data deleted Successfully!";
                    return Json(retObj);
                }
                catch (Exception ex)
                {
                    retObj.IsError = true;
                    retObj.ErrorMessage = ex.Message;
                    return Json(retObj);
                }
            }
            else
            {
                retObj.IsError = true;
                retObj.ErrorMessage = AppConstants.Messages.Not_Authorized;
                return Json(retObj);
            }
        }
        [Route("api/Master/SaveEmployee")]
        [HttpPost]
        public async Task<JsonResult> SaveEmployee(EmployeeMasterViewModel model)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.Employee_Master, RoleActions.InsertUpdate);
            if (hasRights == true)
            {
                try
                {
                    EmployeeMasterDto dtoObj = new EmployeeMasterDto();
                    if (model.File != null)
                    {
                        var dir = Directory.GetCurrentDirectory();
                        var combinepath = Path.Combine(dir, "wwwroot\\Resources\\Emp_Img");
                        if (!Directory.Exists(combinepath))
                        {
                            DirectoryInfo DI = Directory.CreateDirectory(combinepath);
                        }
                        var formatedfilename = string.Concat(Path.GetFileNameWithoutExtension(model.File.FileName),
                                      DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                                      Path.GetExtension(model.File.FileName));
                        var dirpath = Path.Combine(combinepath, formatedfilename);
                        using (var stream = new FileStream(dirpath, FileMode.Create))
                        {
                            await model.File.CopyToAsync(stream);
                        }
                        dtoObj.PhotoUrl = "\\Resources\\Emp_Img\\" + formatedfilename;
                    }

                    if (model.PageMode == "NEW")
                    {
                        dtoObj.Created_Date = DateTime.Now;
                    }
                    else
                    {
                        dtoObj.Created_Date = model.Created_Date;
                        dtoObj.Modified_Date = DateTime.Now;
                        if (model.File == null)
                        {
                            dtoObj.PhotoUrl = model.PhotoUrl;
                        }
                    }
                    dtoObj.EmpId = model.EmpId;
                    dtoObj.Title = model.Title;
                    dtoObj.FirstName = model.FirstName;
                    dtoObj.LastName = model.LastName;
                    dtoObj.Gender = model.Gender;
                    if (!string.IsNullOrEmpty(model.DateOfBirthString)) {
                        dtoObj.DateOfBirth = new DateTime(Convert.ToInt32(model.DateOfBirthString.Split("/")[2]), Convert.ToInt32(model.DateOfBirthString.Split("/")[1]), Convert.ToInt32(model.DateOfBirthString.Split("/")[0]));
                    }
                    if (!string.IsNullOrEmpty(model.HireDateString))
                    {
                        dtoObj.HireDate = new DateTime(Convert.ToInt32(model.HireDateString.Split("/")[2]), Convert.ToInt32(model.HireDateString.Split("/")[1]), Convert.ToInt32(model.HireDateString.Split("/")[0]));
                    }
                    dtoObj.Email = model.Email;
                    dtoObj.Phone = model.Phone;
                    dtoObj.IsUser = model.IsUser;
                    dtoObj.Address = model.Address;
                    dtoObj.IsActive = true;
                    dtoObj.RoleId = model.RoleId;

                    var lastInsertedId = await _employeeMasterData.InsertUpdateEmployee(dtoObj);
                    var userObj = await _userData.GetUserByEmpId(model.EmpId);

                    if (model.IsUser && lastInsertedId != "Duplicate")
                    {
                        UserDto user = new UserDto();
                        if (userObj != null)
                        {
                            user.UserId = userObj.UserId;
                            user.Created_Date = userObj.Created_Date;
                            user.Modified_Date = DateTime.Now;
                        }
                        else
                        {
                            user.Created_Date = DateTime.Now;
                        }
                        user.EmpId = model.EmpId;
                        user.Email = model.Email;
                        if (model.UserPassword != null)
                        {
                            ERP_Utitlity erpUtil = new ERP_Utitlity();
                            var encryptPswd = erpUtil.Encrypt(model.UserPassword);
                            //var encryptPswd = Encrypt(model.UserPassword);
                            user.Password = encryptPswd;
                        }
                        else
                        {
                            user.Password = "";
                        }
                        await _userData.InsertUpdateUser(user);
                    }
                    else if (!model.IsUser && userObj != null)
                    {
                        await _userData.DeleteUserByKey(userObj.UserId);
                    }
                    //if (lastInsertedId != null)
                    //{
                    //    retObj.IsError = false;
                    //    retObj.SuccessMessage = "Data Saved Successfully!";
                    //}
                    //else
                    //{
                    //    retObj.IsError = false;
                    //    retObj.SuccessMessage = "Data Updated Successfully!";
                    //}
                    if (lastInsertedId == "Duplicate")
                    {
                        retObj.IsError = true;
                        retObj.ErrorMessage = "Email already exsist !";

                    }
                    else if (lastInsertedId == null)
                    {
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data Updated Successfully!";
                    }
                    else
                    {
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data Saved Successfully!";
                    }
                    return Json(retObj);
                }
                catch (Exception ex)
                {
                    retObj.IsError = true;
                    retObj.ErrorMessage = ex.Message;
                    return Json(retObj);
                }
            }
            else
            {
                retObj.IsError = true;
                retObj.ErrorMessage = AppConstants.Messages.Not_Authorized;
                return Json(retObj);
            }
        }
        [Route("api/Master/GetIdByTable")]
        [HttpGet]
        public async Task<JsonResult> GetIdByTable()
        {
            var empId = await _erpCommonServiceData.GetIdByTable("EmployeeMaster");
            return Json(empId);
        }
        #endregion

        #region User 
        [Route("api/Master/GetAllUser")]
        [HttpGet]
        public async Task<JsonResult> GetAllUser()
        {
            var userlist = await _userData.GetAllUser();
            return Json(userlist);
        }

        [Route("api/Master/GetUserByKey")]
        //[HttpGet("{id}")]
        public async Task<JsonResult> GetUserByKey(int id)
        {
            var userObj = await _userData.GetUserByKey(id);
            return Json(userObj);
        }

        [Route("api/Master/DeleteUserByKey")]
        //[HttpDelete("{id}")]
        public async Task<JsonResult> DeleteUserByKey(int id)
        {
            JsonResponseData retObj = new JsonResponseData();
            try
            {
               await _userData.DeleteUserByKey(id);
               retObj.IsError = false;
               retObj.SuccessMessage = "Data deleted Successfully!";
               return Json(retObj);
             }
             catch (Exception ex)
             {
                retObj.IsError = true;
                retObj.ErrorMessage = ex.Message;
                return Json(retObj);
             }
        }
        [Route("api/Master/SaveUser")]
        [HttpPost]
        public async Task<JsonResult> SaveUser(UserViewModel model)
        {
            JsonResponseData retObj = new JsonResponseData();
                try
                {
                    UserDto dtoObj = new UserDto();
                    if (model.UserId == 0)
                    {
                        dtoObj.Created_Date = DateTime.Now;
                    }
                    else
                    {
                        dtoObj.Created_Date = model.Created_Date;
                        dtoObj.Modified_Date = DateTime.Now;
                    }
                    dtoObj.UserId = model.UserId;
                    dtoObj.EmpId = model.EmpId;
                    dtoObj.Email = model.Email;
                    dtoObj.Password = model.Password;
                    var lastInsertedId = await _userData.InsertUpdateUser(dtoObj);
                    if (lastInsertedId > 0)
                    {
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data Saved Successfully!";
                    }
                    else
                    {
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data Updated Successfully!";
                    }
                    return Json(retObj);
                }
                catch (Exception ex)
                {
                    retObj.IsError = true;
                    retObj.ErrorMessage = ex.Message;
                    return Json(retObj);
                }
        }
        #endregion

        #region Inventory MASTER
        [Route("api/Master/GetAllInventory")]
        [HttpGet]
        public async Task<JsonResult> GetAllInventory()
        {
            var inventorylist = await _inventoryMasterData.GetAllInventory();
            return Json(inventorylist);
        }

        [Route("api/Master/GetInventoryByKey")]
        //[HttpGet("{id}")]
        public async Task<JsonResult> GetInventoryByKey(int id)
        {
            var inventoryObj = await _inventoryMasterData.GetInventoryByKey(id);
            return Json(inventoryObj);
        }

        [Route("api/Master/DeleteInventoryByKey")]
        //[HttpDelete("{id}")]
        public async Task<JsonResult> DeleteInventoryByKey(int id)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.Inventory_Master, RoleActions.Delete);
            if (hasRights == true)
            {
                try
                {
                    var availableStockCount = await _transactionData.GetAvailableStockItemDetailsByInventoryId(id);
                    if(availableStockCount > 0)
                    {
                        retObj.IsError = true;
                        retObj.ErrorMessage = "You can not delete this inventory! It is already in Use.";
                        return Json(retObj);
                    }
                    else
                    {
                        var gstObj = await _inventoryGSTDetailsData.GetInventoryGSTDetailsByInventoryId(id);
                        if (gstObj != null)
                        {
                            await _inventoryGSTDetailsData.DeleteInventoryGSTDetailsByInventoryId(id);
                        }
                        await _inventoryMasterData.DeleteInventoryByKey(id);
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data deleted Successfully!";
                        await _transactionData.DeleteStockItemDetailsByInventoryId(id);
                        await _transactionData.DeleteStockMovementByInventoryId(id);
                        return Json(retObj);
                    }
                }
                catch (Exception ex)
                {
                    retObj.IsError = true;
                    retObj.ErrorMessage = ex.Message;
                    return Json(retObj);
                }
            }
            else
            {
                retObj.IsError = true;
                retObj.ErrorMessage = AppConstants.Messages.Not_Authorized;
                return Json(retObj);
            }
        }
        [Route("api/Master/SaveInventory")]
        [HttpPost]
        public async Task<JsonResult> SaveInventory(InventoryMasterViewModel model)
        {
            JsonResponseData retObj = new JsonResponseData();
            var sessionEmployee = HttpContext.Session.GetObject<EmployeeMasterDto>(AppConstants.SessionKey.CURRENT_USER);
            var hasRights = IsHasRight(AppPages.Inventory_Master, RoleActions.InsertUpdate);
            if (hasRights == true)
            {
                try
                {
                    InventoryMasterDto dtoObj = new InventoryMasterDto();
                    if (model.Id == 0)
                    {
                        dtoObj.Created_Date = DateTime.Now;
                        
                    }
                    else
                    {
                        dtoObj.Created_Date = model.Created_Date;
                        dtoObj.Modified_Date = DateTime.Now;
                    }
                    dtoObj.Id = model.Id;
                    dtoObj.BrandId = model.BrandId;
                    dtoObj.CategoryId = model.CategoryId;
                    dtoObj.ItemName = model.ItemName;
                    dtoObj.UnitId = model.UnitId;
                   // dtoObj.BranchId = model.BranchId;
                    dtoObj.TypesOfGood = model.TypesOfGood;
                    dtoObj.ServiceType = model.ServiceType;
                    //dtoObj.Rate = model.Rate;
                    dtoObj.IsGSTApplicable = model.IsGSTApplicable;
                    dtoObj.IsActive = true;
                    var lastInsertedId = await _inventoryMasterData.InsertUpdateInventory(dtoObj);
                    if (model.IsGSTApplicable)
                    {
                        InventoryGSTDetailsDto gstdataObj = new InventoryGSTDetailsDto();
                        gstdataObj.Id = model.GSTDetail.Id;
                        gstdataObj.InventoryId = lastInsertedId;
                        gstdataObj.HSNCode = model.GSTDetail.HSNCode;
                        gstdataObj.CalculationType = model.GSTDetail.CalculationType;
                        gstdataObj.Taxability = model.GSTDetail.Taxability;
                        gstdataObj.IntegratedTax = model.GSTDetail.IntegratedTax;
                        gstdataObj.CentralTax = model.GSTDetail.CentralTax;
                        gstdataObj.StateTax = model.GSTDetail.StateTax;
                        var inventoryGSTDetailsId = await _inventoryGSTDetailsData.InsertUpdateInventoryGSTDetails(gstdataObj);
                    }
                    else
                    {
                        await _inventoryGSTDetailsData.DeleteInventoryGSTDetailsByInventoryId(lastInsertedId);
                    }
                    if (model.Id == 0)
                    {
                        var branchList = await _branchMasterData.GetAllBranch();
                        foreach (var item in branchList)
                        {
                            StockItemDetailsDto stockItemObj = new StockItemDetailsDto();
                            stockItemObj.InventoryId = lastInsertedId;
                            stockItemObj.BranchId = item.Id;
                            stockItemObj.UnitId = model.UnitId;
                            stockItemObj.AvailableQty = 0;
                            stockItemObj.FYId = 1;
                            stockItemObj.Created_Date = DateTime.Now;
                            stockItemObj.Modified_Date = DateTime.Now;
                            stockItemObj.Created_by = sessionEmployee.UserId;
                            stockItemObj.Modified_by = sessionEmployee.UserId;
                            await _transactionData.InsertUpdateStockItemDetails(stockItemObj);

                            StockMovementDto stockMovementObj = new StockMovementDto();
                            stockMovementObj.InventoryId = lastInsertedId;
                            stockMovementObj.BranchId = item.Id;
                            stockMovementObj.TranCode = null;
                            stockMovementObj.TranId = null;
                            stockMovementObj.TranDetailId = 0;
                            stockMovementObj.FYId = 1;
                            stockMovementObj.UnitId = model.UnitId;
                            stockMovementObj.TranRate = 0;
                            stockMovementObj.TranQty = 0;
                            stockMovementObj.TranAmount = 0;
                            stockMovementObj.TranBook = AppConstants.Tran.Opening;
                            stockMovementObj.TranType = AppConstants.Tran.Debit;
                            stockMovementObj.Created_Date = DateTime.Now;
                            stockMovementObj.Modified_Date = DateTime.Now;
                            stockMovementObj.InsertFrom = "SystemGenerated";
                            await _transactionData.InsertUpdateStockMovement(stockMovementObj);

                        }
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data Saved Successfully!";
                    }
                    else
                    {
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Data Updated Successfully!";
                    }

                    return Json(retObj);
                }
                catch (Exception ex)
                {
                    retObj.IsError = true;
                    retObj.ErrorMessage = ex.Message;
                    return Json(retObj);
                }
            }
            else
            {
                retObj.IsError = true;
                retObj.ErrorMessage = AppConstants.Messages.Not_Authorized;
                return Json(retObj);
            }
        }
        #endregion

    }
}
