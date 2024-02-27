using ERP.DAL.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DAL.Abstract
{
    public interface ISalesOrderService
    {
        Task<List<SalesOrderDto>> GetAllSalesOrder();
        Task<string> InsertSalesOrder(SalesOrderDto model);
        Task<int> InsertSalesOrderDetails(SalesOrderDetailsDto model);
        Task DeleteSalesOrderByKey(string sono);
        Task DeleteSalesOrderDetailsBySONo(string sono);
        Task<SalesOrderDto> GetSalesOrderByKey(string sono);
        Task<List<SalesOrderDetailsDto>> GetSalesOrderDetailsBySONo(string sono);
    }
}
