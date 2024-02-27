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
    public class PurchaseController : BaseController
    {
        private readonly IERP_CommonService _erpCommonServiceData;
        private readonly IInventoryMasterService _inventoryMasterData;
        private readonly IBranchMasterService _branchMasterData;
        private readonly IAccountPartyMasterService _accountPartyMasterData;
        private readonly IOtherChargesMasterService _otherChargesMasterData;
        private readonly IInventoryGSTDetailsService _inventoryGSTDetailsData;
        private readonly IPurchaseQuotationService _purchaseQuotationData;
        public PurchaseController(IERP_CommonService erpCommonServiceData, IBranchMasterService branchMasterData, IOtherChargesMasterService otherChargesMasterData, IAccountPartyMasterService accountPartyMasterData, IInventoryMasterService inventoryMasterData, IInventoryGSTDetailsService inventoryGSTDetailsData, IPurchaseQuotationService purchaseQuotationData)
        {
            _erpCommonServiceData = erpCommonServiceData;
            _inventoryMasterData = inventoryMasterData;
            _branchMasterData = branchMasterData;
            _accountPartyMasterData = accountPartyMasterData;
            _otherChargesMasterData = otherChargesMasterData;
            _inventoryGSTDetailsData = inventoryGSTDetailsData;
            _purchaseQuotationData = purchaseQuotationData;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> PurchaseQuotation()
        {
            PurchaseQuotationViewModel model = new PurchaseQuotationViewModel();
            // var retObj = obj.GetBranch_Supplier_StockItem_Data();
            model.PQNo = await _erpCommonServiceData.GetIdByTable("PurchaseQuotation");
            model.PQDateDisplay = DateTime.Now.ToString("MM/dd/yyyy");
            model.PageMode = "New";
            //model.BranchList = (List<Branch>)retObj[0];
            //model.SupplierList = (List<Supplier>)retObj[1];
            var otherChargesList = await _accountPartyMasterData.GetAccountPartyBySunderyOtherCharge();
            model.JsonOtherChargesData = JsonConvert.SerializeObject(otherChargesList);
            var gstList = await _inventoryGSTDetailsData.GetAllInventoryGSTDetails();
            model.StockItemList = await _inventoryMasterData.GetAllInventory();
            model.BranchSelectList = new SelectList(await _branchMasterData.GetAllBranch(), "Id", "BranchName");
            model.SupplierSelectList = new SelectList(await _accountPartyMasterData.GetAccountPartyBySundery(1), "Id", "Name");
            //model.OtherChargesSelectList = new SelectList(await _otherChargesMasterData.GetAllOtherCharges(), "Id", "Name");
            model.OtherChargesSelectList = new SelectList(await _accountPartyMasterData.GetAccountPartyBySunderyOtherCharge(), "Id", "Name");
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
                  Tax = gstList.Where(y => y.InventoryId == x.Id).Select(z => z.CentralTax + z.StateTax).FirstOrDefault(),
                  StateTax = gstList.Where(y => y.InventoryId == x.Id).Select(z => z.StateTax).FirstOrDefault(),
                  CentralTax = gstList.Where(y => y.InventoryId == x.Id).Select(z => z.CentralTax).FirstOrDefault(),
                  HSNCode = gstList.Where(y => y.InventoryId == x.Id).Select(z => z.HSNCode).FirstOrDefault(),
              });
           // ViewBag.StockData = model.StockItemList;
            //model.StockItemSelectList = new SelectList(model.StockItemList, "Id", "ItemName");
            return View(model);
        }
        public ActionResult PurchaseQuotationDetails()
        {
            //var purchaseQuotationList = await _purchaseQuotationData.GetAllPurchaseQuotation();
            return View();
        }
    }
}