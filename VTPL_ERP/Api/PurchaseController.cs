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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VTPL_ERP.Api
{
    //[Route("api/[controller]")]
    public class PurchaseController : BaseController
    {
        private readonly IPurchaseQuotationService _purchaseQuotationData;
        private readonly IERP_CommonService _erpCommonServiceData;
        public PurchaseController(IPurchaseQuotationService purchaseQuotationData, IERP_CommonService erpCommonServiceData)
        {
            _purchaseQuotationData = purchaseQuotationData;
            _erpCommonServiceData = erpCommonServiceData;
        }
        #region PurchaseQuotation
        [Route("api/Purchase/GetAllPurchaseQuotation")]
        [HttpGet]
        public async Task<JsonResult> GetAllPurchaseQuotation()
        {
            var purchaseQuotationlist = await _purchaseQuotationData.GetAllPurchaseQuotation();
            return Json(purchaseQuotationlist);
        }
        [Route("api/Purchase/ViewPurchaseQuotation")]
        [HttpGet]
        public async Task<IActionResult> ViewPurchaseQuotation(string PQNo)
        {
            PQInfoViewModel model = new PQInfoViewModel();
            model.PurchaseQuotationData = await _purchaseQuotationData.GetPurchaseQuotationByKey(PQNo);
            model.PurchaseQuotationDetails = await _purchaseQuotationData.GetPurchaseQuotationDetailsByPQNo(PQNo);
            model.PQOtherChargesDetails = await _purchaseQuotationData.GetPQOtherChargesDetailsByPQNo(PQNo);
            return View(model);
        }
        
        [Route("api/Purchase/SavePurchaseQuotation")]
        [HttpPost]
        public async Task<JsonResult> SavePurchaseQuotation(PurchaseQuotationViewModel model)
        {
            JsonResponseData retObj = new JsonResponseData();
            var sessionEmployee = HttpContext.Session.GetObject<EmployeeMasterDto>(AppConstants.SessionKey.CURRENT_USER);
            var hasRights = IsHasRight(AppPages.PurchaseQuotation, RoleActions.InsertUpdate);
            if(hasRights == true)
            {
                try
                {
                    PurchaseQuotationDto dtoObj = new PurchaseQuotationDto();
                    dtoObj.PQNo = model.PQNo;
                    if (!string.IsNullOrEmpty(model.PQDateString))
                    {
                        dtoObj.PQDate = new DateTime(Convert.ToInt32(model.PQDateString.Split("/")[2]), Convert.ToInt32(model.PQDateString.Split("/")[1]), Convert.ToInt32(model.PQDateString.Split("/")[0]));
                    }
                    
                    dtoObj.BranchId = model.BranchId;
                    dtoObj.SupplierId = model.SupplierId;
                    //dtoObj.TotalQty = model.TotalQty;
                    dtoObj.Amount = model.Amount;
                    dtoObj.OtherCharges = model.OtherCharges;
                    dtoObj.DiscAmount = model.DiscAmount;
                    dtoObj.SGST = model.SGST;
                    dtoObj.CGST = model.CGST;
                    dtoObj.TotalAmount = model.TotalAmount;
                    dtoObj.RoundOff = model.RoundOff;
                    if (model.PageMode == "New")
                    {
                        dtoObj.Created_Date = DateTime.Now;
                        dtoObj.CreatedBy = sessionEmployee.UserId;
                    }
                    else
                    {
                        dtoObj.Created_Date = model.Created_Date;
                        dtoObj.Modified_Date = DateTime.Now;
                        dtoObj.CreatedBy = model.CreatedBy;
                        dtoObj.ModifiedBy = sessionEmployee.UserId;
                    }
                    dtoObj.TotalQty = model.PurchaseQuotationDetails.Sum(x => x.Qty);
                    var lastInsertedId = await _purchaseQuotationData.InsertPurchaseQutotaion(dtoObj);
                    foreach (PurchaseQuotationDetailsViewModel item in model.PurchaseQuotationDetails)
                    {
                        PurchaseQuotationDetailsDto purchaseDetailsObj = new PurchaseQuotationDetailsDto();
                        purchaseDetailsObj.Id = item.Id;
                        purchaseDetailsObj.PQNo = lastInsertedId;
                        purchaseDetailsObj.InventoryId = item.InventoryId;
                        purchaseDetailsObj.Qty = item.Qty;
                        purchaseDetailsObj.Rate = item.Rate;
                        purchaseDetailsObj.GrossAmt = item.GrossAmt;
                        purchaseDetailsObj.Discount = item.Discount;
                        purchaseDetailsObj.DiscAmt = item.DiscAmt;
                        // purchaseDetailsObj.UnitId = item.UnitId;
                        purchaseDetailsObj.Tax = item.Tax;
                        purchaseDetailsObj.TaxAmt = item.TaxAmt;
                        purchaseDetailsObj.TotalAmount = item.TotalAmount;
                        purchaseDetailsObj.SerialNos = item.SerialNos;
                        await _purchaseQuotationData.InsertPurchaseQutotaionDetails(purchaseDetailsObj);
                        //foreach (var serialNo in purchaseDetailsObj.SerialNos.Split(';'))
                        //{
                        //    await _purchaseQuotationData.InsertPurchaseQutotaionItemSerialNosDetails(purchaseDetailsObj.PQNo, purchaseDetailsObj.InventoryId, serialNo);
                        //}
                    }
                    if (model.PQOtherChargesDetails != null)
                    {
                        foreach (PQOtherChargesDetailsViewModel item in model.PQOtherChargesDetails)
                        {
                            PQOtherChargesDetailsDto pqOtherChargeObj = new PQOtherChargesDetailsDto();
                            pqOtherChargeObj.Id = item.Id;
                            pqOtherChargeObj.PQNo = item.PQNo;
                            pqOtherChargeObj.Amount = item.Amount;
                            pqOtherChargeObj.OtherChargeId = item.OtherChargeId;
                            pqOtherChargeObj.FinalAmount = item.FinalAmount;
                            await _purchaseQuotationData.InsertPQOtherChargesDetails(pqOtherChargeObj);
                        }
                    }
                    retObj.IsError = false;
                    retObj.ResponseDataList.Add(_erpCommonServiceData.GetIdByTable("PurchaseQuotation"));
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
        [Route("api/Purchase/DeletePurchaseQuotationByKey")]
        //[HttpDelete("{id}")]
        public async Task<JsonResult> DeletePurchaseQuotationByKey(string PQNo)
        {
            JsonResponseData retObj = new JsonResponseData();
            var hasRights = IsHasRight(AppPages.PurchaseQuotation, RoleActions.Delete);
            if (hasRights == true)
            {
                try
                {
                    var pqDetailsObj = await _purchaseQuotationData.GetPurchaseQuotationDetailsByPQNo(PQNo);
                    if (pqDetailsObj != null)
                    {
                        await _purchaseQuotationData.DeletePurchaseQuotationDetailsByPQNo(PQNo);
                    }
                    await _purchaseQuotationData.DeletePurchaseQuotationByKey(PQNo);
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
