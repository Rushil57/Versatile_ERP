using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class StateMasterDto
    {
        public int Id { get; set; }
        public string StateCode { get; set; }
        public string TINNo { get; set; }
        public string StateName { get; set; }
    }
}
