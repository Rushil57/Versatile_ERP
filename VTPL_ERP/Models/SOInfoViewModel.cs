using ERP.DAL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTPL_ERP.Models
{
    public class SOInfoViewModel
    {
        public SalesOrderDto SalesOrderData { get; set; }
        public CompanyDto CompanyData { get; set; }
        public List<SalesOrderDetailsDto> SalesOrderDetails { get; set; }
        public string TotalAmountinWord { get; set; }
    }
}
