using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTPL_ERP.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string EmpId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime? Modified_Date { get; set; }
    }
}
