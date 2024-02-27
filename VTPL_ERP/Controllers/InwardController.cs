using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.DAL.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VTPL_ERP.Models;

namespace VTPL_ERP.Controllers
{
    public class InwardController : BaseController
    {
        private readonly IAccountPartyMasterService _accountPartyMasterData;
        private readonly IInventoryMasterService _inventoryMasterData;
        private readonly IInventoryBrandMasterService _inventoryBrandMasterData;
        private readonly IProblemMasterService _problemMasterData;
        public InwardController(IAccountPartyMasterService accountPartyMasterData, IInventoryMasterService inventoryMasterData, IInventoryBrandMasterService inventoryBrandMasterData, IProblemMasterService problemMasterData)
        {
            _accountPartyMasterData = accountPartyMasterData;
            _inventoryMasterData = inventoryMasterData;
            _inventoryBrandMasterData = inventoryBrandMasterData;
            _problemMasterData = problemMasterData;
        }
        public async Task<ActionResult> Inward()
        {
            InwardViewModel model = new InwardViewModel();
            model.InwardDateDisplay = DateTime.Now.ToString("dd/MM/yyyy"); ;
            model.AccountPartySelectList = new SelectList(await _accountPartyMasterData.GetAllAccountParty(), "Id", "Name");
            model.InventorySelectList = new SelectList(await _inventoryMasterData.GetAllInventory(), "Id", "ItemName");
            model.InventoryBrandSelectList = new SelectList(await _inventoryBrandMasterData.GetAllInventoryBrand(), "Id", "BrandName");
            model.ProblemSelectList = new SelectList(await _problemMasterData.GetAllProblem(), "Id", "ProblemName");
            return View(model);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}