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
    public class PurchaseReturnEntryService : BaseRepository, IPurchaseReturnEntryService
    {
        IConfiguration _configuration;

        public PurchaseReturnEntryService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DeletePurchaseReturnEntryByKey(string debitNoteNo)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("DebitNoteNo", debitNoteNo, DbType.String, ParameterDirection.Input);
                await connection.QueryAsync("DeletePurchaseReturnEntryByKey_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task DeletePurchaseReturnEntryDetailsByDebitNoteNo(string debitNoteNo)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("DebitNoteNo", debitNoteNo, DbType.String, ParameterDirection.Input);
                await connection.QueryAsync("DeletePurchaseReturnEntryDetailsByDebitNoteNo_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task DeletePurchaseReturnEntryItemSerialNosDetailsByDebitNoteNo(string debitNoteNo)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("DebitNoteNo", debitNoteNo, DbType.String, ParameterDirection.Input);
                await connection.QueryAsync("DeletePurchaseReturnEntryItemSerialByDebitNoteNo_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task DeletePurchaseReturnEntryOtherChargesDetailsByDebitNoteNo(string debitNoteNo)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("DebitNoteNo", debitNoteNo, DbType.String, ParameterDirection.Input);
                await connection.QueryAsync("DeletePurchaseReturnEntryOtherChargesByDebitNoteNo_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<PurchaseReturnEntryDto>> GetAllPurchaseReturnEntry()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<PurchaseReturnEntryDto>("GetAllPurchaseReturnEntry_sp", commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<int> GetCurrentBalanceBySupplierId(string supplierId)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("SupplierId", supplierId, DbType.String, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<int>("GetCurrentPREBalanceBySupplier_sp", param, commandType: CommandType.StoredProcedure);
                if (dataObj == null)
                {

                }
                return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<string>> GetInValidSerialIdsForPurchReturnByInventoryId(int inventoryId, string PRENo)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("InventoryId", inventoryId, DbType.Int64, ParameterDirection.Input);
                param.Add("PRENo", PRENo, DbType.String, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<string>("GetInValidSerialIdsForPurchReturnByInventoryId_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.ToList();
            }
        }

        public async Task<PurchaseReturnEntryDto> GetPurchaseReturnEntryByKey(string debitNoteNo)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("DebitNoteNo", debitNoteNo, DbType.String, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<PurchaseReturnEntryDto>("GetPurchaseReturnEntryByKey_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<PurchaseReturnEntryDetailsDto>> GetPurchaseReturnEntryDetailsByDebitNoteNo(string debitNoteNo)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("DebitNoteNo", debitNoteNo, DbType.String, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<PurchaseReturnEntryDetailsDto>("GetPurchaseReturnEntryDetailsByDebitNoteNo_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.ToList();
            }
        }

        public async Task<List<PurchaseReturnEntryOtherChargesDetailsDto>> GetPurchaseReturnEntryOtherChargesDetailsByDebitNoteNo(string debitNoteNo)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("DebitNoteNo", debitNoteNo, DbType.String, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<PurchaseReturnEntryOtherChargesDetailsDto>("GetPurchaseReturnEntryOtherChargesDetailsByDebitNoteNo_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.ToList();
            }
        }

        public async Task<List<string>> GetSerialNosByDebitNoteNo(string debitNoteNo)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("DebitNoteNo", debitNoteNo, DbType.String, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<string>("GetSerialNosByDebitNoteNo_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.ToList();
            }
        }

        public async Task<string> InsertPurchaseReturnEntry(PurchaseReturnEntryDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("DebitNoteNo", model.DebitNoteNo, DbType.String, ParameterDirection.Input);
                    param.Add("DebitNoteDate", model.DebitNoteDate, DbType.DateTime, ParameterDirection.Input);
                    param.Add("OriginalPurchaseEntryId", model.OriginalPurchaseEntryId, DbType.String, ParameterDirection.Input);
                    param.Add("SupplierId", model.SupplierId, DbType.String, ParameterDirection.Input);
                    param.Add("CurrentBalance", model.CurrentBalance, DbType.Decimal, ParameterDirection.Input);
                    param.Add("PurchaseAccount", model.PurchaseAccount, DbType.String, ParameterDirection.Input);
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
                    param.Add("CreatedBy", model.CreatedBy, DbType.Int64, ParameterDirection.Input);
                    param.Add("ModifiedBy", model.ModifiedBy, DbType.Int64, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<string>("InsertPurchaseReturnEntry_sp", param, commandType: CommandType.StoredProcedure);
                    return lastInsertedId.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<int> InsertPurchaseReturnEntryDetails(PurchaseReturnEntryDetailsDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    //param.Add("Id", model.Id, DbType.Int64, ParameterDirection.Input);
                    param.Add("DebitNoteNo", model.DebitNoteNo, DbType.String, ParameterDirection.Input);
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
                    var lastInsertedId = await connection.QueryAsync<int>("InsertPurchaseReturnEntryDetails_sp", param, commandType: CommandType.StoredProcedure);
                    return lastInsertedId.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task InsertPurchaseReturnEntryItemSerialNosDetails(string debitNoteNo, int inventoryId, string itemSerialNo)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("DebitNoteNo", debitNoteNo, DbType.String, ParameterDirection.Input);
                    param.Add("InventoryId", inventoryId, DbType.Int32, ParameterDirection.Input);
                    param.Add("ItemSerialNo", itemSerialNo, DbType.String, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync("InsertPurchaseReturnEntryItemSerialNosDetails_sp", param, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<int> InsertPurchaseReturnEntryOtherChargesDetails(PurchaseReturnEntryOtherChargesDetailsDto model)
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
                    param.Add("DebitNoteNo", model.DebitNoteNo, DbType.String, ParameterDirection.Input);
                    param.Add("FinalAmount", model.FinalAmount, DbType.Decimal, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<int>("InsertPurchaseReturnEntryOtherChargesDetails_sp", param, commandType: CommandType.StoredProcedure);
                    return lastInsertedId.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<int> IsPurchaseReturnCanDelete(string serialIds, string debitNoteNo)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("SerailNos", serialIds, DbType.String, ParameterDirection.Input);
                param.Add("DebitNoteNo", debitNoteNo, DbType.String, ParameterDirection.Input);
                var lastInsertedId = await connection.QueryAsync<int>("IsPurchaseReturnCanDelete_sp", param, commandType: CommandType.StoredProcedure);
                return lastInsertedId.FirstOrDefault();
            }
        }
    }
}
