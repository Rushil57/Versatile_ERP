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
    public class CompanyController : BaseController
    {
        private readonly ICompanyService _companyData;

        public CompanyController(ICompanyService companyData)
        {
            _companyData = companyData;
        }

        #region Company
        [Route("api/Company/GetAllCompany")]
        //[HttpGet]
        public async Task<JsonResult> GetAllCompany()
        {
            var companylist = await _companyData.GetAllCompany();
            return Json(companylist);
        }

        [Route("api/Company/SaveCompany")]
        [HttpPost]
        public async Task<JsonResult> SaveCompany(CompanyViewModel model)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.Company, RoleActions.InsertUpdate);
            if (hasRights == true)
            {
                try
                {
                    CompanyDto dtoObj = new CompanyDto();
                    dtoObj.CompanyId = model.CompanyId;
                    dtoObj.CompanyName = model.CompanyName;
                    dtoObj.CompanyAddress = model.CompanyAddress;
                    dtoObj.City = model.City;
                    dtoObj.State =  model.State;
                    dtoObj.Country = model.Country;
                    dtoObj.ZipCode = model.ZipCode;
                    dtoObj.ContactNo = model.ContactNo;
                    dtoObj.GSTNo = model.GSTNo;
                    dtoObj.CIN = model.CIN;
                    dtoObj.Email = model.Email;
                    dtoObj.MailingName = model.MailingName;
                    dtoObj.PhoneNo = model.PhoneNo;
                    dtoObj.FAXNo = model.FAXNo;
                    dtoObj.Website = model.Website;
                    if (!string.IsNullOrEmpty(model.FinancialYearBeginsString))
                    {
                        dtoObj.FinancialYearBegins = new DateTime(Convert.ToInt32(model.FinancialYearBeginsString.Split("/")[2]), Convert.ToInt32(model.FinancialYearBeginsString.Split("/")[1]), Convert.ToInt32(model.FinancialYearBeginsString.Split("/")[0]));
                    }
                    if (!string.IsNullOrEmpty(model.BooksBeginningsFromString))
                    {
                        dtoObj.BooksBeginningsFrom = new DateTime(Convert.ToInt32(model.BooksBeginningsFromString.Split("/")[2]), Convert.ToInt32(model.BooksBeginningsFromString.Split("/")[1]), Convert.ToInt32(model.BooksBeginningsFromString.Split("/")[0]));
                    }
                    
                    dtoObj.PANNumber = model.PANNumber;
                    var lastInsertedId = await _companyData.InsertUpdateCompany(dtoObj);
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
