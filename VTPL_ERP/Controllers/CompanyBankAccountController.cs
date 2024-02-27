using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VTPL_ERP.Models;
using ERP.DAL.Abstract;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VTPL_ERP.Controllers
{
    public class CompanyBankAccountController : BaseController
    {
        private readonly ICompanyBankAccountService _companyBankAccountController;

        public CompanyBankAccountController(ICompanyBankAccountService companyBankAccountController)
        {
            _companyBankAccountController = companyBankAccountController;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CompanyBankAccount()
        {
            return View();
        }
    }
}