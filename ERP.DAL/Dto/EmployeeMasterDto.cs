using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class EmployeeMasterDto
    {
        public string EmpId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? HireDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsUser { get; set; }
        public string Address { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime? Modified_Date { get; set; }
        public bool IsActive { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public int RoleId { get; set; }
        public string PhotoUrl { get; set; }
        public int UserId { get; set; }
    }
}
