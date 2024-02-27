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
    public class SalesOrderController : BaseController
    {
        private readonly IERP_CommonService _erpCommonServiceData;
        private readonly IInventoryMasterService _inventoryMasterData;
        private readonly IAccountPartyMasterService _accountPartyMasterData;
        private readonly IEmployeeMasterService _employeeMasterData;
        private readonly IOtherChargesMasterService _otherChargesMasterData;
        private readonly IInventoryGSTDetailsService _inventoryGSTDetailsData;
        private readonly ISalesOrderService _salesOrderData;
        private readonly IBranchMasterService _branchMasterData;
        public SalesOrderController(IBranchMasterService branchMasterData, IERP_CommonService erpCommonServiceData, IOtherChargesMasterService otherChargesMasterData, IAccountPartyMasterService accountPartyMasterData, IEmployeeMasterService employeeMasterData, IInventoryMasterService inventoryMasterData, IInventoryGSTDetailsService inventoryGSTDetailsData, ISalesOrderService salesOrderData)
        {
            _branchMasterData = branchMasterData;
            _erpCommonServiceData = erpCommonServiceData;
            _inventoryMasterData = inventoryMasterData;
            _accountPartyMasterData = accountPartyMasterData;
            _employeeMasterData = employeeMasterData;
            _otherChargesMasterData = otherChargesMasterData;
            _inventoryGSTDetailsData = inventoryGSTDetailsData;
            _salesOrderData = salesOrderData;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> SalesOrder(string SONo = "")
        {
            SalesOrderViewModel model = new SalesOrderViewModel();


            if (string.IsNullOrEmpty(SONo))
            {
                model.SONo = await _erpCommonServiceData.GetIdByTable("SalesOrder");
                model.SODateDisplay = DateTime.Now.ToString("dd/MM/yyyy");
                model.PageMode = "New";
            }
            else
            {
                model.PageMode = "Edit";
                var salesOrderObj = await _salesOrderData.GetSalesOrderByKey(SONo);
                model.SONo = salesOrderObj.SONo;
                model.BranchId = salesOrderObj.BranchId;
                model.Created_Date = salesOrderObj.Created_Date;
                model.CreatedBy = salesOrderObj.CreatedBy;
                model.Modified_Date = salesOrderObj.Modified_Date;
                model.ModifiedBy = salesOrderObj.ModifiedBy;
                model.SODateDisplay = salesOrderObj.SalesDate.ToString("dd/MM/yyyy");
                model.SalesDate = salesOrderObj.SalesDate;
                model.SupplierName = salesOrderObj.SupplierName;
                model.TotalAmount = salesOrderObj.TotalAmount;
                model.TotalQty = salesOrderObj.TotalQty;
                model.SelectedSupplierId = salesOrderObj.SupplierId;
                model.SelectedBranchId = salesOrderObj.BranchId;
                model.SelectedSalesPersonId = salesOrderObj.SalesPersonId;
            }
            var gstList = await _inventoryGSTDetailsData.GetAllInventoryGSTDetails();
            model.BranchSelectList = new SelectList(await _branchMasterData.GetAllBranch(), "Id", "BranchName");
           // model.StockItemList = await _inventoryMasterData.GetAllAccountPartyWithAvailableStock();
            model.SupplierSelectList = new SelectList(await _accountPartyMasterData.GetAccountPartyBySundery(2), "Id", "Name");
            model.SalesPersonSelectList = new SelectList(await _employeeMasterData.GetAllEmployee(), "EmpId", "FirstName");
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
            //      x.Rate,
            //      x.AvailableStock,
            //      Tax = gstList.Where(y => y.InventoryId == x.Id).Select(z => z.CentralTax + z.StateTax).FirstOrDefault(),
            //      StateTax = gstList.Where(y => y.InventoryId == x.Id).Select(z => z.StateTax).FirstOrDefault(),
            //      CentralTax = gstList.Where(y => y.InventoryId == x.Id).Select(z => z.CentralTax).FirstOrDefault(),
            //      HSNCode = gstList.Where(y => y.InventoryId == x.Id).Select(z => z.HSNCode).FirstOrDefault(),
            //  });
            return View(model);

        }
        public ActionResult SalesOrderDetails()
        {
            return View();
        }
        public async Task<JsonResult> GetStockItem(int branchId)
        {
            SalesOrderViewModel model = new SalesOrderViewModel();
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