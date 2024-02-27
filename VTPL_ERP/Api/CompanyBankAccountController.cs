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
    public class CompanyBankAccountController : BaseController
    {
        private readonly ICompanyBankAccountService _companyBankAccountData;

        public CompanyBankAccountController(ICompanyBankAccountService companyBankAccountData)
        {
            _companyBankAccountData = companyBankAccountData;
        }

        #region CompanyBankAccount
        [Route("api/CompanyBankAccount/GetAllCompanyBankAccount")]
        [HttpGet]
        public async Task<JsonResult> GetAllCompanyBankAccount()
        {
            var companyBankAccountlist = await _companyBankAccountData.GetAllCompanyBankAccount();
            return Json(companyBankAccountlist);
        }

        [Route("api/CompanyBankAccount/GetCompanyBankAccountByKey")]
        //[HttpGet("{id}")]
        public async Task<JsonResult> GetCompanyBankAccountByKey(int id)
        {
            var companyBankAccountObj = await _companyBankAccountData.GetCompanyBankAccountByKey(id);
            return Json(companyBankAccountObj);
        }

        [Route("api/CompanyBankAccount/DeleteCompanyBankAccountByKey")]
        //[HttpDelete("{id}")]
        public async Task<JsonResult> DeleteCompanyBankAccountByKey(int id)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.CompanyBankAccount, RoleActions.Delete);
            if (hasRights == true)
            {
                try
                {
                    await _companyBankAccountData.DeleteCompanyBankAccountByKey(id);
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
        [Route("api/CompanyBankAccount/SaveCompanyBankAccount")]
        [HttpPost]
        public async Task<JsonResult> SaveCompanyBankAccount(CompanyBankAccountViewModel model)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.CompanyBankAccount, RoleActions.InsertUpdate);
            if (hasRights == true)
            {
                try
                {
                    CompanyBankAccountDto dtoObj = new CompanyBankAccountDto();
                    
                    dtoObj.Id = model.Id;
                    dtoObj.BankName = model.BankName;
                    dtoObj.CurrencyOfLedger = model.CurrencyOfLedger;
                    dtoObj.IsActivateInterestCalculation = model.IsActivateInterestCalculation;
                    dtoObj.ODLimit = model.ODLimit;
                    dtoObj.IsChequeBooks = model.IsChequeBooks;
                    dtoObj.IsChequePrintingConfg = model.IsChequePrintingConfg;
                    dtoObj.ACHolderName = model.ACHolderName;
                    dtoObj.ACName = model.ACName;
                    dtoObj.IFSCCode = model.IFSCCode;
                    dtoObj.Branch = model.Branch;
                    dtoObj.Balance = model.Balance;
                    dtoObj.AccountGroup = model.AccountGroup;
                    var lastInsertedId = await _companyBankAccountData.InsertUpdateCompanyBankAccount(dtoObj);
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
