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
    public class SalesEntryService : BaseRepository, ISalesEntryService
    {
        IConfiguration _configuration;

        public SalesEntryService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DeleteSalesEntryByKey(string salesEntryNo)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("SalesEntryNo", salesEntryNo, DbType.String, ParameterDirection.Input);
                await connection.QueryAsync("DeleteSalesEntryByKey_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task DeleteSalesEntryDetailsBySalesEntryNo(string salesEntryNo)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("SalesEntryNo", salesEntryNo, DbType.String, ParameterDirection.Input);
                await connection.QueryAsync("DeleteSalesEntryDetailsBySalesEntryNo_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task DeleteSEItemSerialNosDetailsBySalesEntryNo(string salesEntryNo)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("SalesEntryNo", salesEntryNo, DbType.String, ParameterDirection.Input);
                await connection.QueryAsync("DeleteSalesEntryItemSerialBySalesEntryNo_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task DeleteSEOtherChargesDetailsBySalesEntryNo(string salesEntryNo)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("SalesEntryNo", salesEntryNo, DbType.String, ParameterDirection.Input);
                await connection.QueryAsync("DeleteSalesEntryOtherChargesBySalesEntryNo_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<SalesEntryDto>> GetAllSalesEntry()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<SalesEntryDto>("GetAllSalesEntry_sp", commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<List<SalesEntryDto>> GetAllSalesEntryBySupplierId(int supplierId)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("SupplierId", supplierId, DbType.Int64, ParameterDirection.Input);
                var dataList = await connection.QueryAsync<SalesEntryDto>("GetAllSalesEntryBySupplierId_sp", param, commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<List<SalesEntryDto>> GetAllSalesEntryforSRE()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<SalesEntryDto>("GetAllSalesEntryforSRE_sp", commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<List<string>> GetAvailableSerialIdByInventoryId(int inventoryId, int branchId)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("InventoryId", inventoryId, DbType.Int64, ParameterDirection.Input);
                param.Add("BranchId", branchId, DbType.Int64, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<string>("GetAvailableSerialIdByInventoryId_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.ToList();
            }
        }

        public async Task<int> GetCurrentSEBalanceBySupplierId(int supplierId)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("SupplierId", supplierId, DbType.Int64, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<int>("GetCurrentSEBalanceBySupplierId_sp", param, commandType: CommandType.StoredProcedure);
                if (dataObj == null)
                {

                }
                return dataObj.FirstOrDefault();
            }
        }

        public async Task<SalesEntryDto> GetSalesEntryByKey(string salesEntryNo)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("SalesEntryNo", salesEntryNo, DbType.String, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<SalesEntryDto>("GetSalesEntryByKey_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<SalesEntryDetailsDto>> GetSalesEntryDetailsBySalesEntryNo(string salesEntryNo)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("SalesEntryNo", salesEntryNo, DbType.String, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<SalesEntryDetailsDto>("GetSalesEntryDetailsBySalesEntryNo_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.ToList();
            }
        }

        public async Task<SalesReturnEntryDto> GetSalesReturnEntryBySalesEntryNo(string salesEntryNo)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("SalesEntryNo", salesEntryNo, DbType.String, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<SalesReturnEntryDto>("GetSalesReturnEntryBySalesEntryNo_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<SEOtherChargesDetailsDto>> GetSEOtherChargesDetailsBySalesEntryNo(string salesEntryNo)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("SalesEntryNo", salesEntryNo, DbType.String, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<SEOtherChargesDetailsDto>("GetSEOtherChargesDetailsBySalesEntryNo_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.ToList();
            }
        }

        public async Task<List<string>> GetSESerialNosByInventoryId(int InventoryId)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("InventoryId", InventoryId, DbType.String, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<string>("GetSESerialNosByInventoryId_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.ToList();
            }
        }

        public async Task<string> InsertSalesEntry(SalesEntryDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("SalesEntryNo", model.SalesEntryNo, DbType.String, ParameterDirection.Input);
                    param.Add("SalesEntryDate", model.SalesEntryDate, DbType.DateTime, ParameterDirection.Input);
                    param.Add("RefNo", model.RefNo, DbType.String, ParameterDirection.Input);
                    param.Add("SupplierId", model.SupplierId, DbType.Int64, ParameterDirection.Input);
                    param.Add("CurrentBalance", model.CurrentBalance, DbType.Decimal, ParameterDirection.Input);
                    param.Add("SalesAccount", model.SalesAccount, DbType.String, ParameterDirection.Input);
                    param.Add("DispatchedBy", model.DispatchedBy, DbType.String, ParameterDirection.Input);
                    param.Add("DispatchedDocateNo", model.DispatchedDocateNo, DbType.String, ParameterDirection.Input);
                    param.Add("Destination", model.Destination, DbType.String, ParameterDirection.Input);
                    param.Add("Narration", model.Narration, DbType.String, ParameterDirection.Input);
                    param.Add("TotalQty", model.TotalQty, DbType.Int32, ParameterDirection.Input);
                    param.Add("Amount", model.Amount, DbType.Decimal, ParameterDirection.Input);
                    param.Add("OtherCharges", model.OtherCharges, DbType.Decimal, ParameterDirection.Input);
                    param.Add("DiscAmount", model.DiscAmount, DbType.Decimal, ParameterDirection.Input);
                    param.Add("SGST", model.SGST, DbType.Decimal, ParameterDirection.Input);
                    param.Add("CGST", model.CGST, DbType.Decimal, ParameterDirection.Input);
                    param.Add("TotalAmount", model.TotalAmount, DbType.Decimal, ParameterDirection.Input);
                    param.Add("RoundOff", model.RoundOff, DbType.Decimal, ParameterDirection.Input);
                    param.Add("Created_Date", model.Created_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("Modified_Date", model.Modified_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("BranchId", model.BranchId, DbType.Int64, ParameterDirection.Input);
                    param.Add("PaymentStatus", model.PaymentStatus, DbType.Int32, ParameterDirection.Input);
                    param.Add("CreatedBy", model.CreatedBy, DbType.Int64, ParameterDirection.Input);
                    param.Add("ModifiedBy", model.ModifiedBy, DbType.Int64, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<string>("InsertSalesEntry_sp", param, commandType: CommandType.StoredProcedure);
                    return lastInsertedId.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<int> InsertSalesEntryDetails(SalesEntryDetailsDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    //param.Add("Id", model.Id, DbType.Int64, ParameterDirection.Input);
                    param.Add("SalesEntryNo", model.SalesEntryNo, DbType.String, ParameterDirection.Input);
                    param.Add("InventoryId", model.InventoryId, DbType.Int64, ParameterDirection.Input);
                    param.Add("Qty", model.Qty, DbType.Int32, ParameterDirection.Input);
                    param.Add("Rate", model.Rate, DbType.Decimal, ParameterDirection.Input);
                    param.Add("GrossAmt", model.GrossAmt, DbType.Decimal, ParameterDirection.Input);
                    param.Add("Discount", model.Discount, DbType.Decimal, ParameterDirection.Input);
                    param.Add("DiscAmt", model.DiscAmt, DbType.Decimal, ParameterDirection.Input);
                    param.Add("UnitId", model.UnitId, DbType.Int64, ParameterDirection.Input);
                    param.Add("Tax", model.Tax, DbType.Decimal, ParameterDirection.Input);
                    param.Add("TaxAmt", model.TaxAmt, DbType.Decimal, ParameterDirection.Input);
                    param.Add("TotalAmount", model.TotalAmount, DbType.Decimal, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<int>("InsertSalesEntryDetails_sp", param, commandType: CommandType.StoredProcedure);
                    return lastInsertedId.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task InsertSalesEntryItemSerialNosDetails(string salesEntryNo, int inventoryId, string itemSerialNo)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("SalesEntryNo", salesEntryNo, DbType.String, ParameterDirection.Input);
                    param.Add("InventoryId", inventoryId, DbType.Int32, ParameterDirection.Input);
                    param.Add("ItemSerialNo", itemSerialNo, DbType.String, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync("InsertSalesEntryItemSerialNosDetails_sp", param, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<int> InsertSEOtherChargesDetails(SEOtherChargesDetailsDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    //param.Add("Id", model.Id, DbType.Int64, ParameterDirection.Input);
                    param.Add("Id", model.Id, DbType.Int64, ParameterDirection.Input);
                    param.Add("OtherChargeId", model.OtherChargeId, DbType.Int64, ParameterDirection.Input);
                    param.Add("Amount", model.Amount, DbType.Decimal, ParameterDirection.Input);
                    param.Add("SalesEntryNo", model.SalesEntryNo, DbType.String, ParameterDirection.Input);
                    param.Add("FinalAmount", model.FinalAmount, DbType.Decimal, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<int>("InsertSEOtherChargesDetails_sp", param, commandType: CommandType.StoredProcedure);
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
