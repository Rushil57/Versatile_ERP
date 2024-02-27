using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class ProblemMasterDto
    {
        public int Id { get; set; }
        public string ProblemName { get; set; }
        public string Description { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime? Modified_Date { get; set; }
        public bool IsActive { get; set; }
    }
}
