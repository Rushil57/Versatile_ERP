using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class SalesOrderDto
    {
        public string SONo { get; set; }
        public string SalesPersonId { get; set; }
        public DateTime SalesDate { get; set; }
        public int SupplierId { get; set; }
        public int TotalQty { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime? Created_Date { get; set; }
        public DateTime? Modified_Date { get; set; }
        public int BranchId { get; set; }
        public List<SalesOrderDetailsDto> SalesOrderDetails { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierGSTNumber { get; set; }
        public string SupplierPANNumber { get; set; }
        public string SupplierStateTINNo { get; set; }
        public string SupplierStateName { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
}
