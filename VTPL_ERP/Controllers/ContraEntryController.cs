using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VTPL_ERP.Models;
using VTPL_ERP.Api;
using ERP.DAL.Abstract;
using ERP.DAL.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace VTPL_ERP.Controllers
{
    public class ContraEntryController : BaseController
    {
        private readonly IAccountPartyMasterService _accountPartyMasterData;
        public ContraEntryController(IAccountPartyMasterService accountPartyMasterData)
        {
            _accountPartyMasterData = accountPartyMasterData;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ContraEntry()
        {
            ContraEntryViewModel model = new ContraEntryViewModel();
            model.ContraDateDisplay = DateTime.Now.ToString("dd/MM/yyyy");
            model.SourceBankSelectList = new SelectList(await _accountPartyMasterData.GetAccountPartyBySundery(14), "Id", "Name");
            model.DestinationBankSelectList = new SelectList(await _accountPartyMasterData.GetAccountPartyBySundery(14), "Id", "Name");
            return View(model);
        }
    }
}