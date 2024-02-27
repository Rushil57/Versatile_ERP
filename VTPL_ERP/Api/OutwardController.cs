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
    //[Route("api/[controller]")]
    public class OutwardController : BaseController
    {
        private readonly ICourierMasterService _courierMasterData;
        private readonly IInwardService _inwardData;
        private readonly IOutwardService _outwardData;
        public OutwardController(IOutwardService outwardData, ICourierMasterService courierMasterData, IInwardService inwardData)
        {
            _outwardData = outwardData;
            _courierMasterData = courierMasterData;
            _inwardData = inwardData;
        }
        #region Outward
        [Route("api/Outward/GetAllOutward")]
        [HttpGet]
        public async Task<JsonResult> GetAllOutward()
        {
            var outwardlist = await _outwardData.GetAllOutward();
            return Json(outwardlist);
        }

        [Route("api/Outward/GetOutwardByKey")]
        //[HttpGet("{id}")]
        public async Task<JsonResult> GetOutwardByKey(int id)
        {
            var outwardObj = await _outwardData.GetOutwardByKey(id);
            return Json(outwardObj);
        }

        [Route("api/Outward/DeleteOutwardByKey")]
        //[HttpDelete("{id}")]
        public async Task<JsonResult> DeleteOutwardByKey(int id)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.Outward, RoleActions.Delete);
            if (hasRights == true)
            {
                try
                {
                    await _outwardData.DeleteOutwardByKey(id);
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
        [Route("api/Outward/SaveOutward")]
        [HttpPost]
        public async Task<JsonResult> SaveOutward(OutwardViewModel model)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.Outward, RoleActions.InsertUpdate);
            if (hasRights == true)
            {
                try
                {
                    OutwardDto dtoObj = new OutwardDto();
                    if (model.File != null)
                    {
                        var dir = Directory.GetCurrentDirectory();
                        var combinepath = Path.Combine(dir, "wwwroot\\Resources\\Outward_Img");
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
                        dtoObj.ImagePath = "\\Resources\\Outward_Img\\" + formatedfilename;
                    }
                    if (model.OutwardId == 0)
                    {

                    }
                    else
                    {
                        if (model.File == null)
                        {
                            dtoObj.ImagePath = model.ImagePath;
                        }
                    }
                    dtoObj.OutwardId = model.OutwardId;
                    if (!string.IsNullOrEmpty(model.OutwardDateDisplay))
                    {
                        dtoObj.OutwardDate = new DateTime(Convert.ToInt32(model.OutwardDateDisplay.Split("/")[2]), Convert.ToInt32(model.OutwardDateDisplay.Split("/")[1]), Convert.ToInt32(model.OutwardDateDisplay.Split("/")[0]));
                    }
                    if (!string.IsNullOrEmpty(model.CourierDateDisplay))
                    {
                        dtoObj.CourierDate = new DateTime(Convert.ToInt32(model.CourierDateDisplay.Split("/")[2]), Convert.ToInt32(model.CourierDateDisplay.Split("/")[1]), Convert.ToInt32(model.CourierDateDisplay.Split("/")[0]));
                    }
                    dtoObj.InwardId = model.InwardId;
                    dtoObj.CourierId = model.CourierId;
                    dtoObj.DocumentNo = model.DocumentNo;
                    dtoObj.Charges = model.Charges;
                    var lastInsertedId = await _outwardData.InsertUpdateOutward(dtoObj);
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
