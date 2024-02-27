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
   // [Route("api/[controller]")]
    public class InwardController : BaseController
    {
        private readonly IAccountPartyMasterService _accountPartyMasterData;
        private readonly IInventoryMasterService _inventoryMasterData;
        private readonly IInventoryBrandMasterService _inventoryBrandMasterData;
        private readonly IProblemMasterService _problemMasterData;
        private readonly IInwardService _inwardData;
        public InwardController(IInwardService inwardData, IAccountPartyMasterService accountPartyMasterData, IInventoryMasterService inventoryMasterData, IInventoryBrandMasterService inventoryBrandMasterData, IProblemMasterService problemMasterData)
        {
            _inwardData = inwardData;
            _accountPartyMasterData = accountPartyMasterData;
            _inventoryMasterData = inventoryMasterData;
            _inventoryBrandMasterData = inventoryBrandMasterData;
            _problemMasterData = problemMasterData;
        }
        #region Inward
        [Route("api/Inward/GetAllInward")]
        [HttpGet]
        public async Task<JsonResult> GetAllInward()
        {
            var inwardlist = await _inwardData.GetAllInward();
            return Json(inwardlist);
        }

        [Route("api/Inward/GetInwardByKey")]
        //[HttpGet("{id}")]
        public async Task<JsonResult> GetInwardByKey(int id)
        {
            var inwardObj = await _inwardData.GetInwardByKey(id);
            return Json(inwardObj);
        }

        [Route("api/Inward/DeleteInwardByKey")]
        //[HttpDelete("{id}")]
        public async Task<JsonResult> DeleteInwardByKey(int id)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.Inward, RoleActions.Delete);
            if (hasRights == true)
            {
                try
                {
                    await _inwardData.DeleteInwardByKey(id);
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
        [Route("api/Inward/SaveInward")]
        [HttpPost]
        public async Task<JsonResult> SaveInward(InwardViewModel model)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.Inward, RoleActions.InsertUpdate);
            if (hasRights == true)
            {
                try
                {
                    InwardDto dtoObj = new InwardDto();
                    if (model.File != null)
                    {
                        var dir = Directory.GetCurrentDirectory();
                        var combinepath = Path.Combine(dir, "wwwroot\\Resources\\Inward_Img");
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
                        dtoObj.ImagePath = "\\Resources\\Inward_Img\\" + formatedfilename;
                    }
                    if(model.InwardId == 0)
                    {

                    }
                    else
                    {
                        if (model.File == null)
                        {
                            dtoObj.ImagePath = model.ImagePath;
                        }
                    }
                    dtoObj.InwardId = model.InwardId;
                    if (!string.IsNullOrEmpty(model.InwardDateDisplay))
                    {
                        dtoObj.InwardDate = new DateTime(Convert.ToInt32(model.InwardDateDisplay.Split("/")[2]), Convert.ToInt32(model.InwardDateDisplay.Split("/")[1]), Convert.ToInt32(model.InwardDateDisplay.Split("/")[0]));
                    }
                    dtoObj.ACPartyId = model.ACPartyId;
                    dtoObj.InventoryId = model.InventoryId;
                    dtoObj.BrandId = model.BrandId;
                    dtoObj.SerialNo = model.SerialNo;
                    dtoObj.ProblemId = model.ProblemId;
                    dtoObj.ApproxCharge = model.ApproxCharge;
                    var lastInsertedId = await _inwardData.InsertUpdateInward(dtoObj);
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
