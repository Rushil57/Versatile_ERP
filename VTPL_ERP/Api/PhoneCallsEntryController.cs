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
    public class PhoneCallsEntryController : BaseController
    {
        private readonly IPhoneCallsEntryService _phoneCallsEntryData;
        public PhoneCallsEntryController(IPhoneCallsEntryService phoneCallsEntryData)
        {
            _phoneCallsEntryData = phoneCallsEntryData;
        }
        #region PhoneCallsEntry
        [Route("api/PhoneCallsEntry/GetAllPhoneCallsEntry")]
        [HttpGet]
        public async Task<JsonResult> GetAllPhoneCallsEntry()
        {
            var phoneCallsEntrylist = await _phoneCallsEntryData.GetAllPhoneCallsEntry();
            return Json(phoneCallsEntrylist);
        }

        [Route("api/PhoneCallsEntry/GetPhoneCallsEntryByKey")]
        //[HttpGet("{id}")]
        public async Task<JsonResult> GetPhoneCallsEntryByKey(int id)
        {
            var phoneCallsEntryObj = await _phoneCallsEntryData.GetPhoneCallsEntryByKey(id);
            return Json(phoneCallsEntryObj);
        }

        [Route("api/PhoneCallsEntry/DeletePhoneCallsEntryByKey")]
        //[HttpDelete("{id}")]
        public async Task<JsonResult> DeletePhoneCallsEntryByKey(int id)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.PhoneCallsEntry, RoleActions.Delete);
            if (hasRights == true)
            {
                try
                {
                    await _phoneCallsEntryData.DeletePhoneCallsEntryByKey(id);
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
        [Route("api/PhoneCallsEntry/SavePhoneCallsEntry")]
        [HttpPost]
        public async Task<JsonResult> SavePhoneCallsEntry(PhoneCallsEntryViewModel model)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.PhoneCallsEntry, RoleActions.InsertUpdate);
            if (hasRights == true)
            {
                try
                {
                    PhoneCallsEntryDto dtoObj = new PhoneCallsEntryDto();
                    if (model.Id == 0)
                    {
                        dtoObj.Date = DateTime.Now;
                    }
                    else
                    {
                        dtoObj.Date = model.Date;
                    }
                    dtoObj.Id = model.Id;
                    dtoObj.ACPartyName = model.ACPartyName;
                    dtoObj.SalesPersonRef = model.SalesPersonRef;
                    dtoObj.ModelName = model.ModelName;
                    dtoObj.SerialNumber = model.SerialNumber;
                    var lastInsertedId = await _phoneCallsEntryData.InsertUpdatePhoneCallsEntry(dtoObj);
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
