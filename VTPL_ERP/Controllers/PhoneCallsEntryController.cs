using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.DAL.Abstract;
using VTPL_ERP.Models.Master;
using Microsoft.AspNetCore.Mvc.Rendering;
using VTPL_ERP.Models;

namespace VTPL_ERP.Controllers
{
    public class PhoneCallsEntryController : Controller
    {
        private readonly IAccountPartyMasterService _accountPartyMasterData;
        private readonly IEmployeeMasterService _employeeMasterData;
        private readonly IInventoryMasterService _inventoryMasterData;
        private readonly IPurchaseEntryService _purchaseEntryData;
        public PhoneCallsEntryController(IAccountPartyMasterService accountPartyMasterData, IEmployeeMasterService employeeMasterData, IInventoryMasterService inventoryMasterData, IPurchaseEntryService purchaseEntryData)
        {
            _accountPartyMasterData = accountPartyMasterData;
            _employeeMasterData = employeeMasterData;
            _inventoryMasterData = inventoryMasterData;
            _purchaseEntryData = purchaseEntryData;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> PhoneCallsEntry()
        {
            PhoneCallsEntryViewModel model = new PhoneCallsEntryViewModel();
            model.DateDisplay = DateTime.Now.ToString("MM/dd/yyyy"); ;
            model.AccountPartySelectList = new SelectList(await _accountPartyMasterData.GetAllAccountParty(), "Id", "Name");
            model.EmployeeSelectList = new SelectList(await _employeeMasterData.GetAllEmployee(), "EmpId", "FirstName");
            model.InventorySelectList = new SelectList(await _inventoryMasterData.GetAllInventory(), "Id", "ItemName");
            return View(model);
        }
        public async Task<JsonResult> GetPESerialNos(int InventoryId)
        {
            List<string> listSerialNos = new List<string>();
            listSerialNos = await _purchaseEntryData.GetPESerialNosByInventoryId(InventoryId);
            return Json(listSerialNos);
        }
    }
}