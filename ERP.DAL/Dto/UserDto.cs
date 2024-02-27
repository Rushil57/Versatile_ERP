using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string EmpId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime? Modified_Date { get; set; }
    }
}
