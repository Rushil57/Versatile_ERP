using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class OtherChargesMasterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public bool IsActive { get; set; }
    }
}
