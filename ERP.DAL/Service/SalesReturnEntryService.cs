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
    public class SalesReturnEntryService : BaseRepository, ISalesReturnEntryService
    {
        IConfiguration _configuration;

        public SalesReturnEntryService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DeleteSalesReturnEntryByKey(string creditNoteNo)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("CreditNoteNo", creditNoteNo, DbType.String, ParameterDirection.Input);
                await connection.QueryAsync("DeleteSalesReturnEntryByKey_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task DeleteSalesReturnEntryDetailsByCreditNoteNo(string creditNoteNo)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("CreditNoteNo", creditNoteNo, DbType.String, ParameterDirection.Input);
                await connection.QueryAsync("DeleteSalesReturnEntryDetailsByCreditNoteNo_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task DeleteSalesReturnEntryItemSerialNosDetailsByCreditNoteNo(string creditNoteNo)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("CreditNoteNo", creditNoteNo, DbType.String, ParameterDirection.Input);
                await connection.QueryAsync("DeleteSalesReturnEntryItemSerialByCreditNoteNo_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task DeleteSalesReturnEntryOtherChargesDetailsByCreditNoteNo(string creditNoteNo)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("CreditNoteNo", creditNoteNo, DbType.String, ParameterDirection.Input);
                await connection.QueryAsync("DeleteSalesReturnEntryOtherChargesByCreditNoteNo_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<SalesReturnEntryDto>> GetAllSalesReturnEntry()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<SalesReturnEntryDto>("GetAllSalesReturnEntry_sp", commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<int> GetCurrentBalanceBySupplierId(string supplierId)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("SupplierId", supplierId, DbType.String, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<int>("GetCurrentSREBalanceBySupplier_sp", param, commandType: CommandType.StoredProcedure);
                if (dataObj == null)
                {

                }
                return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<string>> GetInValidSerialIdsForSalesReturnByInventoryId(int inventoryId, string CreditNoteNo)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("InventoryId", inventoryId, DbType.Int64, ParameterDirection.Input);
                param.Add("CreditNoteNo", CreditNoteNo, DbType.String, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<string>("GetInValidSerialIdsForSalesReturnByInventoryId_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.ToList();
            }
        }

        public async Task<SalesReturnEntryDto> GetSalesReturnEntryByKey(string creditNoteNo)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("CreditNoteNo", creditNoteNo, DbType.String, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<SalesReturnEntryDto>("GetSalesReturnEntryByKey_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<SalesReturnEntryDetailsDto>> GetSalesReturnEntryDetailsByCreditNoteNo(string creditNoteNo)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("CreditNoteNo", creditNoteNo, DbType.String, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<SalesReturnEntryDetailsDto>("GetSalesReturnEntryDetailsByCreditNoteNo_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.ToList();
            }
        }

        public async Task<List<SalesReturnEntryOtherChargesDetailsDto>> GetSalesReturnEntryOtherChargesDetailsByCreditNoteNo(string creditNoteNo)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("CreditNoteNo", creditNoteNo, DbType.String, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<SalesReturnEntryOtherChargesDetailsDto>("GetSalesReturnEntryOtherChargesDetailsByCreditNoteNo_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.ToList();
            }
        }

        public async Task<List<string>> GetSerialNosByCreditNoteNo(string creditNoteNo)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("CreditNoteNo", creditNoteNo, DbType.String, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<string>("GetSerialNosByCreditNoteNo_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.ToList();
            }
        }

        public async Task<string> InsertSalesReturnEntry(SalesReturnEntryDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("CreditNoteNo", model.CreditNoteNo, DbType.String, ParameterDirection.Input);
                    param.Add("CreditNoteDate", model.CreditNoteDate, DbType.DateTime, ParameterDirection.Input);
                    param.Add("OriginalSalesEntryId", model.OriginalSalesEntryId, DbType.String, ParameterDirection.Input);
                    param.Add("SupplierId", model.SupplierId, DbType.String, ParameterDirection.Input);
                    param.Add("RefNo", model.RefNo, DbType.String, ParameterDirection.Input);
                    param.Add("CurrentBalance", model.CurrentBalance, DbType.Decimal, ParameterDirection.Input);
                    param.Add("SalesAccount", model.SalesAccount, DbType.String, ParameterDirection.Input);
                    param.Add("Narration", model.Narration, DbType.String, ParameterDirection.Input);
                    param.Add("TotalQty", model.TotalQty, DbType.Int32, ParameterDirection.Input);
                    param.Add("Amount", model.Amount, DbType.Decimal, ParameterDirection.Input);
                    param.Add("OtherCharges", model.OtherCharges, DbType.Decimal, ParameterDirection.Input);
                    param.Add("DiscAmount", model.DiscAmount, DbType.Decimal, ParameterDirection.Input);
                    param.Add("SGST", model.SGST, DbType.Decimal, ParameterDirection.Input);
                    param.Add("CGST", model.CGST, DbType.Decimal, ParameterDirection.Input);
                    param.Add("TotalAmount", model.TotalAmount, DbType.Decimal, ParameterDirection.Input);
                    param.Add("RoundOff", model.RoundOff, DbType.Decimal, ParameterDirection.Input);
                    param.Add("DispatchedBy", model.DispatchedBy, DbType.String, ParameterDirection.Input);
                    param.Add("DispatchedDocateNo", model.DispatchedDocateNo, DbType.String, ParameterDirection.Input);
                    param.Add("Destination", model.Destination, DbType.String, ParameterDirection.Input);
                    param.Add("Created_Date", model.Created_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("Modified_Date", model.Modified_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("BranchId", model.BranchId, DbType.Int64, ParameterDirection.Input);
                    param.Add("CreatedBy", model.CreatedBy, DbType.Int64, ParameterDirection.Input);
                    param.Add("ModifiedBy", model.ModifiedBy, DbType.Int64, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<string>("InsertSalesReturnEntry_sp", param, commandType: CommandType.StoredProcedure);
                    return lastInsertedId.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<int> InsertSalesReturnEntryDetails(SalesReturnEntryDetailsDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    //param.Add("Id", model.Id, DbType.Int64, ParameterDirection.Input);
                    param.Add("CreditNoteNo", model.CreditNoteNo, DbType.String, ParameterDirection.Input);
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
                    var lastInsertedId = await connection.QueryAsync<int>("InsertSalesReturnEntryDetails_sp", param, commandType: CommandType.StoredProcedure);
                    return lastInsertedId.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task InsertSalesReturnEntryItemSerialNosDetails(string creditNoteNo, int inventoryId, string itemSerialNo)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("CreditNoteNo", creditNoteNo, DbType.String, ParameterDirection.Input);
                    param.Add("InventoryId", inventoryId, DbType.Int32, ParameterDirection.Input);
                    param.Add("ItemSerialNo", itemSerialNo, DbType.String, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync("InsertSalesReturnEntryItemSerialNosDetails_sp", param, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<int> InsertSalesReturnEntryOtherChargesDetails(SalesReturnEntryOtherChargesDetailsDto model)
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
                    param.Add("CreditNoteNo", model.CreditNoteNo, DbType.String, ParameterDirection.Input);
                    param.Add("FinalAmount", model.FinalAmount, DbType.Decimal, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<int>("InsertSalesReturnEntryOtherChargesDetails_sp", param, commandType: CommandType.StoredProcedure);
                    return lastInsertedId.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<int> IsSalesReturnCanDelete(string serialIds, string creditNoteNo)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("SerailNos", serialIds, DbType.String, ParameterDirection.Input);
                param.Add("CreditNoteNo", creditNoteNo, DbType.String, ParameterDirection.Input);
                var lastInsertedId = await connection.QueryAsync<int>("IsSalesReturnCanDelete_sp", param, commandType: CommandType.StoredProcedure);
                return lastInsertedId.FirstOrDefault();
            }
        }
    }
}
