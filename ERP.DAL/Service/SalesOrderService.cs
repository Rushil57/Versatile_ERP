using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ERP.DAL.Abstract;
using ERP.DAL.Dto;
using ERP.DAL.Helper;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using System.Linq;

namespace ERP.DAL.Service
{
    public class SalesOrderService : BaseRepository, ISalesOrderService
    {
        IConfiguration _configuration;

        public SalesOrderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task DeleteSalesOrderByKey(string sono)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("SONo", sono, DbType.String, ParameterDirection.Input);
                await connection.QueryAsync("DeleteSalesOrderByKey_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task DeleteSalesOrderDetailsBySONo(string sono)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("SONo", sono, DbType.String, ParameterDirection.Input);
                await connection.QueryAsync("DeleteSalesOrderDetailsBySONo_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<SalesOrderDto>> GetAllSalesOrder()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<SalesOrderDto>("GetAllSalesOrder_sp", commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<SalesOrderDto> GetSalesOrderByKey(string sono)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("SONo", sono, DbType.String, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<SalesOrderDto>("GetSalesOrderByKey_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<SalesOrderDetailsDto>> GetSalesOrderDetailsBySONo(string sono)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("SONo", sono, DbType.String, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<SalesOrderDetailsDto>("GetSalesOrderDetailsBySONo_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.ToList();
            }
        }

        public async Task<string> InsertSalesOrder(SalesOrderDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("SONo", model.SONo, DbType.String, ParameterDirection.Input);
                    param.Add("SalesDate", model.SalesDate, DbType.DateTime, ParameterDirection.Input);
                    param.Add("SalesPersonId", model.SalesPersonId, DbType.String, ParameterDirection.Input);
                    param.Add("SupplierId", model.SupplierId, DbType.Int64, ParameterDirection.Input);
                    param.Add("TotalQty", model.TotalQty, DbType.Int32, ParameterDirection.Input);
                    param.Add("TotalAmount", model.TotalAmount, DbType.Decimal, ParameterDirection.Input);
                    param.Add("Created_Date", model.Created_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("Modified_Date", model.Modified_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("BranchId", model.BranchId, DbType.Int64, ParameterDirection.Input);
                    param.Add("CreatedBy", model.CreatedBy, DbType.Int64, ParameterDirection.Input);
                    param.Add("ModifiedBy", model.ModifiedBy, DbType.Int64, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<string>("InsertSalesOrder_sp", param, commandType: CommandType.StoredProcedure);
                    return lastInsertedId.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<int> InsertSalesOrderDetails(SalesOrderDetailsDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    //param.Add("Id", model.Id, DbType.Int64, ParameterDirection.Input);
                    param.Add("SONo", model.SONo, DbType.String, ParameterDirection.Input);
                    param.Add("InventoryId", model.InventoryId, DbType.Int64, ParameterDirection.Input);
                    param.Add("Qty", model.Qty, DbType.Int32, ParameterDirection.Input);
                    param.Add("Rate", model.Rate, DbType.Decimal, ParameterDirection.Input);
                    param.Add("GrossAmt", model.GrossAmt, DbType.Decimal, ParameterDirection.Input);
                    param.Add("Discount", model.Discount, DbType.Decimal, ParameterDirection.Input);
                    param.Add("DiscAmt", model.DiscAmt, DbType.Decimal, ParameterDirection.Input);
                    //param.Add("UnitId", model.UnitId, DbType.Int64, ParameterDirection.Input);
                    param.Add("Tax", model.Tax, DbType.Decimal, ParameterDirection.Input);
                    param.Add("TaxAmt", model.TaxAmt, DbType.Decimal, ParameterDirection.Input);
                    param.Add("TotalAmount", model.TotalAmount, DbType.Decimal, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<int>("InsertSalesOrderDetails_sp", param, commandType: CommandType.StoredProcedure);
                    return lastInsertedId.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
