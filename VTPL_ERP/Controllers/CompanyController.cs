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
    public class CompanyController : BaseController
    {
        private readonly IAccountPartyMasterService _accountPartyMasterData;
        public CompanyController(IAccountPartyMasterService accountPartyMasterData)
        {
            _accountPartyMasterData = accountPartyMasterData;
           
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Company()
        {
            CompanyViewModel model = new CompanyViewModel();
            model.BooksBeginningsFromDisplay = DateTime.Now.ToString("dd/MM/yyyy");
            model.FinancialYearBeginsDisplay = DateTime.Now.ToString("dd/MM/yyyy");
            model.StateSelectList = new SelectList(await _accountPartyMasterData.GetAllState(), "TINNo", "StateName");
            return View(model);
        }
    }
}