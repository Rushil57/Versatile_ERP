using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTPL_ERP.Models.Master
{
    public class EmployeeMasterViewModel
    {
        public string EmpId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? HireDate { get; set; }
        public string HireDateDisplay { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsUser { get; set; }
        public string Address { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime? Modified_Date { get; set; }
        public bool IsActive { get; set; }
        public string PageMode { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public int RoleId { get; set; }
        public List<RolesMasterViewModel> RoleList { get; set; }
        public IFormFile File { get; set; }
        public string PhotoUrl { get; set; }
        public string DateOfBirthString { get; set; }
        public string HireDateString { get; set; }
        public int UserId { get; set; }
    }
}
