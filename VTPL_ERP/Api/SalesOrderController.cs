using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VTPL_ERP.Controllers;
using ERP.DAL.Abstract;
using VTPL_ERP.Models;
using VTPL_ERP.Util;
using ERP.DAL.Dto;
using Newtonsoft.Json;
using static VTPL_ERP.Util.AppConstants;
using VTPL_ERP.Models.Master;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VTPL_ERP.Api
{
    //[Route("api/[controller]")]
    public class SalesOrderController : BaseController
    {
        private readonly ISalesOrderService _salesOrderData;
        private readonly IERP_CommonService _erpCommonServiceData;
        private readonly IAccountPartyMasterService _accountPartyMasterData;
        private readonly IInventoryMasterService _inventoryMasterData;
        private readonly IOtherChargesMasterService _otherChargesMasterData;
        private readonly IInventoryGSTDetailsService _inventoryGSTDetailsData;
        private readonly ICompanyService _companyData;
        public SalesOrderController(ISalesOrderService salesOrderData, IERP_CommonService erpCommonServiceData, IAccountPartyMasterService accountPartyMasterData, IOtherChargesMasterService otherChargesMasterData, IInventoryMasterService inventoryMasterData, IInventoryGSTDetailsService inventoryGSTDetailsData, ICompanyService companyData)
        {
            _salesOrderData = salesOrderData;
            _erpCommonServiceData = erpCommonServiceData;
            _accountPartyMasterData = accountPartyMasterData;
            _inventoryMasterData = inventoryMasterData;
            _otherChargesMasterData = otherChargesMasterData;
            _inventoryGSTDetailsData = inventoryGSTDetailsData;
            _companyData = companyData;
        }
        #region SalesOrder
        [Route("api/SalesOrder/GetAllSalesOrder")]
        [HttpGet]
        public async Task<JsonResult> GetAllSalesOrder()
        {
            var salesOrderlist = await _salesOrderData.GetAllSalesOrder();
            return Json(salesOrderlist);
        }
        [Route("api/SalesOrder/GetSalesOrderByKey")]
        //[HttpGet]
        public async Task<JsonResult> GetSalesOrderByKey(string SONo)
        {
            SalesOrderViewModel model = new SalesOrderViewModel();
            model.SalesOrderDetailsData = await _salesOrderData.GetSalesOrderDetailsBySONo(SONo);
            return Json(model);
        }
        [Route("api/SalesOrder/PrintSalesOrder")]
        //[HttpGet]
        public async Task<ActionResult> PrintSalesOrder(string SONo)
        {
            SOInfoViewModel model = new SOInfoViewModel();
            model.SalesOrderData = await _salesOrderData.GetSalesOrderByKey(SONo);
            model.CompanyData = await _companyData.GetAllCompany();
            model.SalesOrderDetails = await _salesOrderData.GetSalesOrderDetailsBySONo(SONo);
            var totalAmount = (int)model.SalesOrderData.TotalAmount;
            model.TotalAmountinWord = ERP_Utitlity.NumberToWords(totalAmount);
            return View(model);
        }
        [Route("api/SalesOrder/SaveSalesOrder")]
        [HttpPost]
        public async Task<JsonResult> SaveSalesOrder(SalesOrderViewModel model)
        {
            JsonResponseData retObj = new JsonResponseData();
            var sessionEmployee = HttpContext.Session.GetObject<EmployeeMasterDto>(AppConstants.SessionKey.CURRENT_USER);
            var hasRights = IsHasRight(AppPages.SalesOrder, RoleActions.InsertUpdate);
            if (hasRights == true)
            {
                try
                {
                    SalesOrderDto dtoObj = new SalesOrderDto();
                    if (model.PageMode == "New")
                    {
                        dtoObj.Created_Date = DateTime.Now;
                        dtoObj.CreatedBy = sessionEmployee.UserId;
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Record inserted successfully !";
                    }
                    else
                    {
                        dtoObj.Created_Date = model.Created_Date;
                        dtoObj.Modified_Date = DateTime.Now;
                        dtoObj.CreatedBy = model.CreatedBy;
                        dtoObj.ModifiedBy = sessionEmployee.UserId;
                        await _salesOrderData.DeleteSalesOrderDetailsBySONo(model.SONo);
                        retObj.IsError = false;
                        retObj.SuccessMessage = "Record updated successfully !";
                    }
                    dtoObj.SONo = model.SONo;
                    if (!string.IsNullOrEmpty(model.SalesDateString))
                    {
                        dtoObj.SalesDate = new DateTime(Convert.ToInt32(model.SalesDateString.Split("/")[2]), Convert.ToInt32(model.SalesDateString.Split("/")[1]), Convert.ToInt32(model.SalesDateString.Split("/")[0]));
                    }
                    dtoObj.BranchId = model.BranchId;
                    dtoObj.SalesPersonId = model.SalesPersonId;
                    dtoObj.SupplierId = model.SupplierId;
                    //dtoObj.TotalQty = model.TotalQty;
                    dtoObj.TotalAmount = model.TotalAmount;
                    dtoObj.TotalQty = model.SalesOrderDetails.Sum(x => x.Qty);
                    var lastInsertedId = await _salesOrderData.InsertSalesOrder(dtoObj);
                    foreach (SalesOrderDetailsViewModel item in model.SalesOrderDetails)
                    {
                        SalesOrderDetailsDto salesOrderObj = new SalesOrderDetailsDto();
                        // purchaseDetailsObj.Id = item.Id;
                        salesOrderObj.SONo = lastInsertedId;
                        salesOrderObj.InventoryId = item.InventoryId;
                        salesOrderObj.Qty = item.Qty;
                        salesOrderObj.Rate = item.Rate;
                        salesOrderObj.GrossAmt = item.GrossAmt;
                        salesOrderObj.Discount = item.Discount;
                        salesOrderObj.DiscAmt = item.DiscAmt;
                        // purchaseDetailsObj.UnitId = item.UnitId;
                        salesOrderObj.Tax = item.Tax;
                        salesOrderObj.TaxAmt = item.TaxAmt;
                        salesOrderObj.TotalAmount = item.TotalAmount;
                        await _salesOrderData.InsertSalesOrderDetails(salesOrderObj);
                    }
                    retObj.ResponseDataList.Add(_erpCommonServiceData.GetIdByTable("SalesOrder"));
                    return Json(retObj);
                }
                catch (Exception ex)
                {
                    retObj.IsError = true;
                    retObj.ErrorMessage = ex.Message;
                    return Json(retObj);
                }
            }
            else
            {
                retObj.IsError = true;
                retObj.ErrorMessage = AppConstants.Messages.Not_Authorized;
                return Json(retObj);
            }
        }
        [Route("api/SalesOrder/DeleteSalesOrderByKey")]
        //[HttpDelete("{id}")]
        public async Task<JsonResult> DeleteSalesOrderByKey(string SONo)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.SalesOrder, RoleActions.Delete);
            if (hasRights == true)
            {
                try
                {
                    var soDetailsObj = await _salesOrderData.GetSalesOrderDetailsBySONo(SONo);
                    if (soDetailsObj != null)
                    {
                        await _salesOrderData.DeleteSalesOrderDetailsBySONo(SONo);
                    }
                    await _salesOrderData.DeleteSalesOrderByKey(SONo);
                    retObj.IsError = false;
                    retObj.SuccessMessage = "Data deleted Successfully!";
                    return Json(retObj);
                }
                catch (Exception ex)
                {
                    retObj.IsError = true;
                    retObj.ErrorMessage = ex.Message;
                    return Json(retObj);
                }
            }
            else
            {
                retObj.IsError = true;
                retObj.ErrorMessage = AppConstants.Messages.Not_Authorized;
                return Json(retObj);
            }
        }
        #endregion

    }
}
