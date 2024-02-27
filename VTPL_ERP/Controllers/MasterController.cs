using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VTPL_ERP.Models.Master;
using VTPL_ERP.Api;
using ERP.DAL.Abstract;
using ERP.DAL.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VTPL_ERP.Controllers
{
    public class MasterController : BaseController
    {
        private readonly IRolesMasterService _rolesMasterData;
        private readonly IInventoryBrandMasterService _inventoryBrandMasterData;
        private readonly IInventoryCategoryMasterService _inventoryCategoryMasterData;
        private readonly IBranchMasterService _branchMasterData;
        private readonly IUnitMasterService _unitMasterData;
        private readonly IEmployeeMasterService _employeeMasterData;
        private readonly IAccountPartyMasterService _accountPartyMasterData;
        public MasterController(IRolesMasterService rolesMasterData,IUnitMasterService unitMasterData, IBranchMasterService branchMasterData, IInventoryBrandMasterService inventoryBrandMasterData, IInventoryCategoryMasterService inventoryCategoryMasterData, IEmployeeMasterService employeeMasterData, IAccountPartyMasterService accountPartyMasterData)
        {
            _rolesMasterData = rolesMasterData;
            _inventoryBrandMasterData = inventoryBrandMasterData;
            _inventoryCategoryMasterData = inventoryCategoryMasterData;
            _branchMasterData = branchMasterData;
            _unitMasterData = unitMasterData;
            _employeeMasterData = employeeMasterData;
            _accountPartyMasterData = accountPartyMasterData;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Branch_Master()
        {
            return View();
        }
        public IActionResult Courier_Master()
        {
            return View();
        }
        public async Task<IActionResult> Employee_Master()
        {
            EmployeeMasterViewModel model = new EmployeeMasterViewModel();
            var roleData =  await _rolesMasterData.GetAllRole();
            var roleDto = roleData.Select(x => new RolesMasterViewModel
            {
                RoleId = x.RoleId,
                RoleName = x.RoleName,
                Description = x.Description,
                Created_Date = x.Created_Date,
                Modified_Date = x.Modified_Date,
                IsActive = x.IsActive
            }).ToList();
            model.RoleList = roleDto;
            model.HireDateDisplay = DateTime.Now.ToShortDateString();
            var finalList = new List<SelectListItem>();
           // finalList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
            foreach (var item in roleDto)
            {
                finalList.Add(new SelectListItem { Text = item.RoleName, Value = item.RoleId.ToString() });
            }
            ViewBag.RoleDataList = finalList;
            return View(model);
        }
        public IActionResult Unit_Master()
        {

            return View();
        }
        public IActionResult Problem_Master()
        {
            return View();
        }
        public IActionResult Roles_Master()
        {
            return View();
        }
        public IActionResult OtherCharges_Master()
        {
            return View();
        }
        public IActionResult RoleAction_Master()
        {
            return View();
        }
        public IActionResult InventoryBrand_Master()
        {
            return View();
        }
        public IActionResult InventoryCategory_Master()
        {
            return View();
        }
        public async Task<IActionResult> AccountParty_Master()
        {
            AccountPartyMasterViewModel model = new AccountPartyMasterViewModel();
            model.OpeningDateDisplay = DateTime.Now.ToShortDateString();
            model.SalesPersonSelectList = new SelectList(await _employeeMasterData.GetEmployeeBySalesPersonRole(), "EmpId", "FirstName");
            model.StateSelectList = new SelectList(await _accountPartyMasterData.GetAllState(), "TINNo", "StateName");
            return View(model);
        }
        public IActionResult User()
        {
            return View();
        }
        public async Task<IActionResult> Inventory_Master()
        {
            InventoryMasterViewModel model = new InventoryMasterViewModel();
            // Unit Master List
            var unitData = await _unitMasterData.GetAllUnit();
            var unitDto = unitData.Select(x => new UnitMasterViewModel
            {
                Id = x.Id,
                UnitName = x.UnitName,
                Alias = x.Alias,
                Created_Date = x.Created_Date,
                Modified_Date = x.Modified_Date,
                IsActive = x.IsActive
            }).ToList();
            model.UnitList = unitDto;
            var finalUnitList = new List<SelectListItem>();
            //finalUnitList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
            foreach (var item in unitDto)
            {
                finalUnitList.Add(new SelectListItem { Text = item.UnitName, Value = item.Id.ToString() });
            }
            ViewBag.UnitDataList = finalUnitList;

            // Inventory Brand Master List
            var brandData = await _inventoryBrandMasterData.GetAllInventoryBrand();
            var brandDto = brandData.Select(x => new InventoryBrandMasterViewModel
            {
                Id = x.Id,
                BrandName = x.BrandName,
                Created_Date = x.Created_Date,
                Modified_Date = x.Modified_Date,
                IsActive = x.IsActive
            }).ToList();
            model.BrandList = brandDto;
            var finalBrandList = new List<SelectListItem>();
           // finalBrandList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
            foreach (var item in brandDto)
            {
                finalBrandList.Add(new SelectListItem { Text = item.BrandName, Value = item.Id.ToString() });
            }
            ViewBag.BrandDataList = finalBrandList;

            //Inventory Category Master List
            var categoryData = await _inventoryCategoryMasterData.GetAllInventoryCategory();
            var categoryDto = categoryData.Select(x => new InventoryCategoryMasterViewModel
            {
                Id = x.Id,
                CategoryName = x.CategoryName,
                Created_Date = x.Created_Date,
                Modified_Date = x.Modified_Date,
                IsActive = x.IsActive
            }).ToList();
            model.CategoryList = categoryDto;
            var finalCategoryList = new List<SelectListItem>();
           // finalCategoryList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
            foreach (var item in categoryDto)
            {
                finalCategoryList.Add(new SelectListItem { Text = item.CategoryName, Value = item.Id.ToString() });
            }
            ViewBag.CategoryDataList = finalCategoryList;

            //Branch Master List
            var branchData = await _branchMasterData.GetAllBranch();
            var branchDto = branchData.Select(x => new BranchMasterViewModel
            {
                Id = x.Id,
                BranchName = x.BranchName,
                Address = x.Address,
                Contact = x.Contact,
                Created_Date = x.Created_Date,
                Modified_Date = x.Modified_Date,
                IsActive = x.IsActive
            }).ToList();
            model.BranchList = branchDto;
            var finalBranchList = new List<SelectListItem>();
            //finalBranchList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
            foreach (var item in branchDto)
            {
                finalBranchList.Add(new SelectListItem { Text = item.BranchName, Value = item.Id.ToString() });
            }
            ViewBag.BranchDataList = finalBranchList;
            return View(model);
        }
        public IActionResult UnauthorizedUser()
        {
            return View();
        }
    }
}