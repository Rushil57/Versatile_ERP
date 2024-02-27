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
    public class OutwardController : BaseController
    {
        private readonly ICourierMasterService _courierMasterData;
        private readonly IInwardService _inwardData;
        public OutwardController(ICourierMasterService courierMasterData, IInwardService inwardData)
        {
            _courierMasterData = courierMasterData;
            _inwardData = inwardData;
        }
        public async Task<ActionResult> Outward()
        {
            OutwardViewModel model = new OutwardViewModel();
            model.OutwardDateDisplay = DateTime.Now.ToString("dd/MM/yyyy");
            model.CourierDateDisplay = DateTime.Now.ToString("dd/MM/yyyy");
            model.CourierSelectList = new SelectList(await _courierMasterData.GetAllCourier(), "Id", "CourierName");
            model.InwardSelectList = new SelectList(await _inwardData.GetAllInward(), "InwardId", "InwardId");
            return View(model);
        }
        public IActionResult Index()
        {
            return View();
        }
         
        public async Task<JsonResult> GetInwardDataByInwardId(int InwardId)
        {
            var inwardData = await _inwardData.GetInwardByKey(InwardId);
            OutwardViewModel model = new OutwardViewModel();
            model.AccountPartyName = inwardData.AccountPartyName;
            model.BrandName = inwardData.BrandName;
            model.InventoryName = inwardData.InventoryName;
            model.SerialNo = inwardData.SerialNo;
            return Json(model);
        }
    }
}