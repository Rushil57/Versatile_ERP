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
    public class PurchaseReturnEntryController : BaseController
    {
        private readonly IERP_CommonService _erpCommonServiceData;
        private readonly IInventoryMasterService _inventoryMasterData;
        private readonly IAccountPartyMasterService _accountPartyMasterData;
        private readonly IOtherChargesMasterService _otherChargesMasterData;
        private readonly IInventoryGSTDetailsService _inventoryGSTDetailsData;
        private readonly IPurchaseEntryService _purchaseEntryData;
        private readonly IPurchaseReturnEntryService _purchaseReturnEntryData;
        private readonly IBranchMasterService _branchMasterData;
        public PurchaseReturnEntryController(IBranchMasterService branchMasterData, IERP_CommonService erpCommonServiceData, IOtherChargesMasterService otherChargesMasterData, IAccountPartyMasterService accountPartyMasterData, IInventoryMasterService inventoryMasterData, IInventoryGSTDetailsService inventoryGSTDetailsData, IPurchaseReturnEntryService purchaseReturnEntryData, IPurchaseEntryService purchaseEntryData)
        {
            _branchMasterData = branchMasterData;
            _erpCommonServiceData = erpCommonServiceData;
            _inventoryMasterData = inventoryMasterData;
            _accountPartyMasterData = accountPartyMasterData;
            _otherChargesMasterData = otherChargesMasterData;
            _inventoryGSTDetailsData = inventoryGSTDetailsData;
            _purchaseReturnEntryData = purchaseReturnEntryData;
            _purchaseEntryData = purchaseEntryData;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> PurchaseReturnEntry(string DebitNoteNo = "")
        {
            PurchaseReturnEntryViewModel model = new PurchaseReturnEntryViewModel();

            if (string.IsNullOrEmpty(DebitNoteNo))
            {
                model.DebitNoteNo = await _erpCommonServiceData.GetIdByTable("PurchaseReturnEntry");
                model.DebitNoteDateDisplay = DateTime.Now.ToString("dd/MM/yyyy");
                model.PageMode = "New";
            }
            else
            {
                model.PageMode = "Edit";
                var purchaseObj = await _purchaseReturnEntryData.GetPurchaseReturnEntryByKey(DebitNoteNo);
                model.DebitNoteNo = purchaseObj.DebitNoteNo;
                model.BranchId = purchaseObj.BranchId;
                model.BranchName = purchaseObj.BranchName;
                model.Amount = purchaseObj.Amount;
                model.CGST = purchaseObj.CGST;
                model.Created_Date = purchaseObj.Created_Date;
                model.CreatedBy = purchaseObj.CreatedBy;
                model.CurrentBalance = purchaseObj.CurrentBalance;
                model.DiscAmount = purchaseObj.DiscAmount;
                model.Modified_Date = purchaseObj.Modified_Date;
                model.ModifiedBy = purchaseObj.ModifiedBy;
                model.Narration = purchaseObj.Narration;
                model.OtherCharges = purchaseObj.OtherCharges;
                model.DebitNoteDateDisplay = purchaseObj.DebitNoteDate.ToString("dd/MM/yyyy");
                model.PurchaseAccount = purchaseObj.PurchaseAccount;
                model.DebitNoteDate = purchaseObj.DebitNoteDate;
                model.RoundOff = purchaseObj.RoundOff;
                model.SGST = purchaseObj.SGST;
                model.SelectedPEId = purchaseObj.OriginalPurchaseEntryId;
                model.SupplierName = purchaseObj.SupplierName;
                model.TotalAmount = purchaseObj.TotalAmount;
                model.TotalQty = purchaseObj.TotalQty;
                model.TotalTaxAmount = purchaseObj.TotalTaxAmount;
                model.SelectedSupplierId = purchaseObj.SupplierId;

            }

            //model.BranchList = (List<Branch>)retObj[0];
            //model.SupplierList = (List<Supplier>)retObj[1];
            var otherChargesList = await _accountPartyMasterData.GetAccountPartyBySunderyOtherCharge();
            model.JsonOtherChargesData = JsonConvert.SerializeObject(otherChargesList);
            var gstList = await _inventoryGSTDetailsData.GetAllInventoryGSTDetails();
            model.StockItemList = await _inventoryMasterData.GetAllInventory();
            model.BranchSelectList = new SelectList(await _branchMasterData.GetAllBranch(), "Id", "BranchName");
            model.SupplierSelectList = new SelectList(await _accountPartyMasterData.GetAccountPartyBySundery(1), "Id", "Name");
            model.PurchaseEntrySelectList = new SelectList(await _purchaseEntryData.GetAllPurchaseEntry(), "PENo", "PENo");
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
        public ActionResult PurchaseReturnEntryDetails()
        {
            return View();
        }
        public async Task<JsonResult> GetCurrentBalance(string supplierId)
        {
            var currentBalance = await _purchaseReturnEntryData.GetCurrentBalanceBySupplierId(supplierId);
            return Json(currentBalance);
        }
        public async Task<JsonResult> GetPREDataByPENo(string PENo,string PRENo)
        {
            PurchaseReturnEntryViewModel model = new PurchaseReturnEntryViewModel();
            var purchaseObj = await _purchaseEntryData.GetPurchaseEntryByKey(PENo);
            model.PurchaseEntryDetailsData = await _purchaseEntryData.GetPurchaseEntryDetailsByPENo(PENo);
            model.PEOtherChargesDetailsData = await _purchaseEntryData.GetPEOtherChargesDetailsByPENo(PENo);
            model.PageMode = "New";
            model.Amount = purchaseObj.Amount;
            model.BranchName = purchaseObj.BranchName;
            model.BranchId = purchaseObj.BranchId;
            model.CGST = purchaseObj.CGST;
            model.Created_Date = purchaseObj.Created_Date;
            model.CreatedBy = purchaseObj.CreatedBy;
            model.CurrentBalance = purchaseObj.CurrentBalance;
            model.DiscAmount = purchaseObj.DiscAmount;
            model.Modified_Date = purchaseObj.Modified_Date;
            model.ModifiedBy = purchaseObj.ModifiedBy;
            model.Narration = purchaseObj.Narration;
            model.OtherCharges = purchaseObj.OtherCharges;
            model.PurchaseAccount = purchaseObj.PurchaseAccount;
            model.RoundOff = purchaseObj.RoundOff;
            model.SGST = purchaseObj.SGST;
            model.SupplierName = purchaseObj.SupplierName;
            model.TotalAmount = purchaseObj.TotalAmount;
            model.TotalQty = purchaseObj.TotalQty;
            model.TotalTaxAmount = purchaseObj.TotalTaxAmount;
            model.SelectedSupplierId = purchaseObj.SupplierId.ToString();
            //
            if (model.PurchaseEntryDetailsData != null && model.PurchaseEntryDetailsData.Count > 0) {
                foreach (var item in model.PurchaseEntryDetailsData)
                {
                    int qty = 0;
                    var serialIds = await _purchaseReturnEntryData.GetInValidSerialIdsForPurchReturnByInventoryId(item.InventoryId, PRENo);
                    var list = item.SerialIds.Split(';');
                    for (int i = 0; i < list.Count(); i++)
                    {
                        if (!serialIds.Contains(list[i])) {
                            qty++;
                        }
                    }
                    item.Qty = qty;
                }
            }
            //
            return Json(model);
        }
    }
}