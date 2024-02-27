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
    public class SalesEntryController : BaseController
    {
        private readonly IERP_CommonService _erpCommonServiceData;
        private readonly IInventoryMasterService _inventoryMasterData;
        private readonly IAccountPartyMasterService _accountPartyMasterData;
        private readonly IOtherChargesMasterService _otherChargesMasterData;
        private readonly IInventoryGSTDetailsService _inventoryGSTDetailsData;
        private readonly ISalesEntryService _salesEntryData;
        private readonly IBranchMasterService _branchMasterData;
        public SalesEntryController(IBranchMasterService branchMasterData, IERP_CommonService erpCommonServiceData, IOtherChargesMasterService otherChargesMasterData, IAccountPartyMasterService accountPartyMasterData, IInventoryMasterService inventoryMasterData, IInventoryGSTDetailsService inventoryGSTDetailsData, ISalesEntryService salesEntryData)
        {
            _branchMasterData = branchMasterData;
            _erpCommonServiceData = erpCommonServiceData;
            _inventoryMasterData = inventoryMasterData;
            _accountPartyMasterData = accountPartyMasterData;
            _otherChargesMasterData = otherChargesMasterData;
            _inventoryGSTDetailsData = inventoryGSTDetailsData;
            _salesEntryData = salesEntryData;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> SalesEntry(string SalesEntryNo = "")
        {
            SalesEntryViewModel model = new SalesEntryViewModel();


            if (string.IsNullOrEmpty(SalesEntryNo))
            {
                model.SalesEntryNo = await _erpCommonServiceData.GetIdByTable("SalesEntry");
                model.SEDateDisplay = DateTime.Now.ToString("dd/MM/yyyy");
                model.PageMode = "New";
                model.PaymentStatus = 0;
            }
            else
            {
                model.PageMode = "Edit";
                var salesObj = await _salesEntryData.GetSalesEntryByKey(SalesEntryNo);
                model.SalesEntryNo = salesObj.SalesEntryNo;
                model.BranchId = salesObj.BranchId;
                model.Amount = salesObj.Amount;
                model.CGST = salesObj.CGST;
                model.Created_Date = salesObj.Created_Date;
                model.CreatedBy = salesObj.CreatedBy;
                model.CurrentBalance = salesObj.CurrentBalance;
                model.DiscAmount = salesObj.DiscAmount;
                model.Modified_Date = salesObj.Modified_Date;
                model.ModifiedBy = salesObj.ModifiedBy;
                model.Narration = salesObj.Narration;
                model.OtherCharges = salesObj.OtherCharges;
                model.SEDateDisplay = salesObj.SalesEntryDate.ToString("dd/MM/yyyy");
                model.SalesAccount = salesObj.SalesAccount;
                model.SalesEntryDate = salesObj.SalesEntryDate;
                model.RoundOff = salesObj.RoundOff;
                model.SGST = salesObj.SGST;
                model.RefNo = salesObj.RefNo;
                model.SupplierName = salesObj.SupplierName;
                model.TotalAmount = salesObj.TotalAmount;
                model.TotalQty = salesObj.TotalQty;
                model.TotalTaxAmount = salesObj.TotalTaxAmount;
                model.SelectedSupplierId = salesObj.SupplierId;
                model.SelectedBranchId = salesObj.BranchId;
                model.DispatchedBy = salesObj.DispatchedBy;
                model.DispatchedDocateNo = salesObj.DispatchedDocateNo;
                model.Destination = salesObj.Destination;
                model.PaymentStatus = salesObj.PaymentStatus;
            }
            var otherChargesList = await _accountPartyMasterData.GetAccountPartyBySunderyOtherCharge();
            model.JsonOtherChargesData = JsonConvert.SerializeObject(otherChargesList);
            var gstList = await _inventoryGSTDetailsData.GetAllInventoryGSTDetails();
            //model.StockItemList = await _inventoryMasterData.GetAllAccountPartyWithAvailableStock();
            model.BranchSelectList = new SelectList(await _branchMasterData.GetAllBranch(), "Id", "BranchName");
            model.SupplierSelectList = new SelectList(await _accountPartyMasterData.GetAccountPartyBySundery(2), "Id", "Name");
            model.OtherChargesSelectList = new SelectList(await _accountPartyMasterData.GetAccountPartyBySunderyOtherCharge(), "Id", "Name");
            //ViewBag.StockData = model.StockItemList.Select(x =>
            //  new
            //  {
            //      x.BrandId,
            //      x.CategoryId,
            //      x.Created_Date,
            //      id = x.Id,
            //      text = x.ItemName,
            //      x.TypesOfGood,
            //      x.UnitId,
            //      x.UnitName,
            //      x.Rate,
            //      x.AvailableStock,
            //      Tax = gstList.Where(y => y.InventoryId == x.Id).Select(z => z.CentralTax + z.StateTax).FirstOrDefault(),
            //      StateTax = gstList.Where(y => y.InventoryId == x.Id).Select(z => z.StateTax).FirstOrDefault(),
            //      CentralTax = gstList.Where(y => y.InventoryId == x.Id).Select(z => z.CentralTax).FirstOrDefault(),
            //      HSNCode = gstList.Where(y => y.InventoryId == x.Id).Select(z => z.HSNCode).FirstOrDefault(),
            //  });
            return View(model);
        }
        public ActionResult SalesEntryDetails()
        {
            return View();
        }
        public async Task<JsonResult> GetCurrentBalance(int supplierId)
        {
            var currentBalance = await _salesEntryData.GetCurrentSEBalanceBySupplierId(supplierId);
            return Json(currentBalance);
        }
        public async Task<JsonResult> GetStockItem(int branchId)
        {
            SalesEntryViewModel model = new SalesEntryViewModel();
            var gstList = await _inventoryGSTDetailsData.GetAllInventoryGSTDetails();
            model.StockItemList = await _inventoryMasterData.GetAllAccountPartyWithAvailableStock(branchId);
            var StockData = model.StockItemList.Select(x =>
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
                 x.UnitName,
                 x.Rate,
                 x.AvailableStock,
                 Tax = gstList.Where(y => y.InventoryId == x.Id).Select(z => z.CentralTax + z.StateTax).FirstOrDefault(),
                 StateTax = gstList.Where(y => y.InventoryId == x.Id).Select(z => z.StateTax).FirstOrDefault(),
                 CentralTax = gstList.Where(y => y.InventoryId == x.Id).Select(z => z.CentralTax).FirstOrDefault(),
                 HSNCode = gstList.Where(y => y.InventoryId == x.Id).Select(z => z.HSNCode).FirstOrDefault(),
             });
            return Json(StockData);
        }
    }
}