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
    public class ContraEntryController : BaseController
    {
        private readonly IContraEntryService _contraEntryData;
        private readonly IERP_CommonService _erpCommonServiceData;
        public ContraEntryController(IContraEntryService contraEntryData, IERP_CommonService erpCommonServiceData)
        {
            _contraEntryData = contraEntryData;
            _erpCommonServiceData = erpCommonServiceData;
        }
        #region ContraEntry
        [Route("api/ContraEntry/GetAllContraEntry")]
        [HttpGet]
        public async Task<JsonResult> GetAllContraEntry()
        {
            var contraEntrylist = await _contraEntryData.GetAllContraEntry();
            return Json(contraEntrylist);
        }

        [Route("api/ContraEntry/GetContraEntryByKey")]
        //[HttpGet("{id}")]
        public async Task<JsonResult> GetContraEntryByKey(string id)
        {
            var contraEntryObj = await _contraEntryData.GetContraEntryByKey(id);
            return Json(contraEntryObj);
        }

        [Route("api/ContraEntry/DeleteContraEntryByKey")]
        //[HttpDelete("{id}")]
        public async Task<JsonResult> DeleteContraEntryByKey(string id)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.ContraEntry, RoleActions.Delete);
            if (hasRights == true)
            {
                try
                {
                    await _contraEntryData.DeleteContraEntryByKey(id);
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
        [Route("api/ContraEntry/SaveContraEntry")]
        [HttpPost]
        public async Task<JsonResult> SaveContraEntry(ContraEntryViewModel model)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.ContraEntry, RoleActions.InsertUpdate);
            if (hasRights == true)
            {
                try
                {
                    ContraEntryDto dtoObj = new ContraEntryDto();
                    if (!string.IsNullOrEmpty(model.ContraDateDisplay))
                    {
                        dtoObj.ContraDate = new DateTime(Convert.ToInt32(model.ContraDateDisplay.Split("/")[2]), Convert.ToInt32(model.ContraDateDisplay.Split("/")[1]), Convert.ToInt32(model.ContraDateDisplay.Split("/")[0]));
                    }
                    dtoObj.CONo = model.CONo;
                    dtoObj.SourceBankId = model.SourceBankId;
                    dtoObj.CurrentBalance = model.CurrentBalance;
                    dtoObj.DestinationBankId = model.DestinationBankId;
                    dtoObj.Amount = model.Amount;
                    var lastInsertedId = await _contraEntryData.InsertUpdateContraEntry(dtoObj);
                    if (lastInsertedId != null)
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
        [Route("api/ContraEntry/GetIdByTable")]
        [HttpGet]
        public async Task<JsonResult> GetIdByTable()
        {
            var coNo = await _erpCommonServiceData.GetIdByTable("ContraEntry");
            return Json(coNo);
        }
        #endregion

    }
}
