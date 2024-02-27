using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VTPL_ERP.Models;
using ERP.DAL.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using ERP.DAL.Abstract;
using Newtonsoft.Json;

namespace VTPL_ERP.Controllers
{
    public class StockTransferController : BaseController
    {
        private readonly IERP_CommonService _erpCommonServiceData;
        private readonly IInventoryMasterService _inventoryMasterData;
        private readonly IAccountPartyMasterService _accountPartyMasterData;
        private readonly IOtherChargesMasterService _otherChargesMasterData;
        private readonly IInventoryGSTDetailsService _inventoryGSTDetailsData;
        private readonly ISalesEntryService _salesEntryData;
        private readonly IBranchMasterService _branchMasterData;
        private readonly IStockTransferService _stockTransferData;
        public StockTransferController(IStockTransferService stockTransferData, IBranchMasterService branchMasterData, IERP_CommonService erpCommonServiceData, IOtherChargesMasterService otherChargesMasterData, IAccountPartyMasterService accountPartyMasterData, IInventoryMasterService inventoryMasterData, IInventoryGSTDetailsService inventoryGSTDetailsData, ISalesEntryService salesEntryData)
        {
            _branchMasterData = branchMasterData;
            _erpCommonServiceData = erpCommonServiceData;
            _inventoryMasterData = inventoryMasterData;
            _accountPartyMasterData = accountPartyMasterData;
            _otherChargesMasterData = otherChargesMasterData;
            _inventoryGSTDetailsData = inventoryGSTDetailsData;
            _salesEntryData = salesEntryData;
            _stockTransferData = stockTransferData;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> StockTransfer()
        {
            StockTransferViewModel model = new StockTransferViewModel();

            model.StockJournalNo = await _erpCommonServiceData.GetIdByTable("StockTransfer");
            model.StockTransferDateDisplay = DateTime.Now.ToString("dd/MM/yyyy");
            model.SourceBranchSelectList = new SelectList(await _branchMasterData.GetAllBranch(), "Id", "BranchName");
            model.DestinationBranchSelectList = new SelectList(await _branchMasterData.GetAllBranch(), "Id", "BranchName");
            model.StockItemList = await _inventoryMasterData.GetAllInventory();
            ViewBag.StockData = model.StockItemList.Select(x =>
              new
              {
                  //x.BranchId,
                  //x.BranchName,
                  x.BrandId,
                  x.CategoryId,
                  // x.CategoryName,
                  x.Created_Date,
                  id = x.Id,
                  text = x.ItemName,
                  x.TypesOfGood,
                  x.UnitId,
                  x.Rate,
              });
            return View(model);
        }
    }
}