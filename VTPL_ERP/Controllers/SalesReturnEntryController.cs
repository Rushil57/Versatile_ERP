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
    public class SalesReturnEntryController : BaseController
    {
        private readonly IERP_CommonService _erpCommonServiceData;
        private readonly IInventoryMasterService _inventoryMasterData;
        private readonly IAccountPartyMasterService _accountPartyMasterData;
        private readonly IOtherChargesMasterService _otherChargesMasterData;
        private readonly IInventoryGSTDetailsService _inventoryGSTDetailsData;
        private readonly ISalesEntryService _salesEntryData;
        private readonly ISalesReturnEntryService _salesReturnEntryData;
        private readonly IBranchMasterService _branchMasterData;
        public SalesReturnEntryController(IBranchMasterService branchMasterData, IERP_CommonService erpCommonServiceData, IOtherChargesMasterService otherChargesMasterData, IAccountPartyMasterService accountPartyMasterData, IInventoryMasterService inventoryMasterData, IInventoryGSTDetailsService inventoryGSTDetailsData, ISalesReturnEntryService salesReturnEntryData, ISalesEntryService salesEntryData)
        {
            _branchMasterData = branchMasterData;
            _erpCommonServiceData = erpCommonServiceData;
            _inventoryMasterData = inventoryMasterData;
            _accountPartyMasterData = accountPartyMasterData;
            _otherChargesMasterData = otherChargesMasterData;
            _inventoryGSTDetailsData = inventoryGSTDetailsData;
            _salesReturnEntryData = salesReturnEntryData;
            _salesEntryData = salesEntryData;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> SalesReturnEntry(string CreditNoteNo = "")
        {
            SalesReturnEntryViewModel model = new SalesReturnEntryViewModel();

            if (string.IsNullOrEmpty(CreditNoteNo))
            {
                model.CreditNoteNo = await _erpCommonServiceData.GetIdByTable("SalesReturnEntry");
                model.CreditNoteDateDisplay = DateTime.Now.ToString("dd/MM/yyyy");
                model.PageMode = "New";
            }
            else
            {
                model.PageMode = "Edit";
                var salesObj = await _salesReturnEntryData.GetSalesReturnEntryByKey(CreditNoteNo);
                model.CreditNoteNo = salesObj.CreditNoteNo;
                model.BranchId = salesObj.BranchId;
                model.BranchName = salesObj.BranchName;
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
                model.CreditNoteDateDisplay = salesObj.CreditNoteDate.ToString("dd/MM/yyyy");
                model.SalesAccount = salesObj.SalesAccount;
                model.CreditNoteDate = salesObj.CreditNoteDate;
                model.RoundOff = salesObj.RoundOff;
                model.SGST = salesObj.SGST;
                model.SelectedSEId = salesObj.OriginalSalesEntryId;
                model.SupplierName = salesObj.SupplierName;
                model.TotalAmount = salesObj.TotalAmount;
                model.TotalQty = salesObj.TotalQty;
                model.TotalTaxAmount = salesObj.TotalTaxAmount;
                model.SelectedSupplierId = salesObj.SupplierId;
                model.RefNo = salesObj.RefNo;
                model.DispatchedBy = salesObj.DispatchedBy;
                model.DispatchedDocateNo = salesObj.DispatchedDocateNo;
                model.Destination = salesObj.Destination;
            }

            //model.BranchList = (List<Branch>)retObj[0];
            //model.SupplierList = (List<Supplier>)retObj[1];
            var otherChargesList = await _accountPartyMasterData.GetAccountPartyBySunderyOtherCharge();
            model.JsonOtherChargesData = JsonConvert.SerializeObject(otherChargesList);
            var gstList = await _inventoryGSTDetailsData.GetAllInventoryGSTDetails();
            model.StockItemList = await _inventoryMasterData.GetAllInventory();
            model.BranchSelectList = new SelectList(await _branchMasterData.GetAllBranch(), "Id", "BranchName");
            model.SupplierSelectList = new SelectList(await _accountPartyMasterData.GetAccountPartyBySundery(1), "Id", "Name");
            model.SalesEntrySelectList = new SelectList(await _salesEntryData.GetAllSalesEntry(), "SalesEntryNo", "SalesEntryNo");
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
                  x.UnitName,
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
        public ActionResult SalesReturnEntryDetails()
        {
            return View();
        }
        public async Task<JsonResult> GetCurrentBalance(string supplierId)
        {
            var currentBalance = await _salesReturnEntryData.GetCurrentBalanceBySupplierId(supplierId);
            return Json(currentBalance);
        }
        public async Task<JsonResult> GetSREDataBySalesEntryNo(string SalesEntryNo, string CreditNoteNo)
        {
            SalesReturnEntryViewModel model = new SalesReturnEntryViewModel();
            var salesObj = await _salesEntryData.GetSalesEntryByKey(SalesEntryNo);
            model.SalesEntryDetailsData = await _salesEntryData.GetSalesEntryDetailsBySalesEntryNo(SalesEntryNo);
            model.SEOtherChargesDetailsData = await _salesEntryData.GetSEOtherChargesDetailsBySalesEntryNo(SalesEntryNo);
            model.PageMode = "New";
            model.BranchName = salesObj.BranchName;
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
            model.SalesAccount = salesObj.SalesAccount;
            model.RefNo = salesObj.RefNo;
            model.DispatchedBy = salesObj.DispatchedBy;
            model.DispatchedDocateNo = salesObj.DispatchedDocateNo;
            model.Destination = salesObj.Destination;
            model.RoundOff = salesObj.RoundOff;
            model.SGST = salesObj.SGST;
            model.SupplierName = salesObj.SupplierName;
            model.TotalAmount = salesObj.TotalAmount;
            model.TotalQty = salesObj.TotalQty;
            model.TotalTaxAmount = salesObj.TotalTaxAmount;
            model.SelectedSupplierId = salesObj.SupplierId.ToString();
            //
            if (model.SalesEntryDetailsData != null && model.SalesEntryDetailsData.Count > 0)
            {
                foreach (var item in model.SalesEntryDetailsData)
                {
                    int qty = 0;
                    var serialIds = await _salesReturnEntryData.GetInValidSerialIdsForSalesReturnByInventoryId(item.InventoryId, CreditNoteNo);
                    var list = item.SerialIds.Split(';');
                    for (int i = 0; i < list.Count(); i++)
                    {
                        if (serialIds.Contains(list[i]))
                        {
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