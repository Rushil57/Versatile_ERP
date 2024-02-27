using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class UnitMasterDto
    {
        public int Id { get; set; }
        public string UnitName { get; set; }
        public string Alias { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime? Modified_Date { get; set; }
        public bool IsActive { get; set; }
    }
}
