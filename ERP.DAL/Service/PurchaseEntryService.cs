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
    public class PurchaseEntryService : BaseRepository, IPurchaseEntryService
    {
        IConfiguration _configuration;

        public PurchaseEntryService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> CheckValidItemSerialNos(string serialIds, string peno)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("SerialIds", serialIds, DbType.String, ParameterDirection.Input);
                param.Add("PENo", peno, DbType.String, ParameterDirection.Input);
                var lastInsertedId = await connection.QueryAsync<string>("CheckValidItemSerialNos_sp", param, commandType: CommandType.StoredProcedure);
                return lastInsertedId.FirstOrDefault();
            }
        }

        public async Task DeletePEItemSerialNosDetailsByPENo(string peno)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("PENo", peno, DbType.String, ParameterDirection.Input);
                await connection.QueryAsync("DeletePurchaseEntryItemSerialByPENo_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task DeletePEOtherChargesDetailsByPENo(string peno)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("PENo", peno, DbType.String, ParameterDirection.Input);
                await connection.QueryAsync("DeletePurchaseEntryOtherChargesByPENo_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task DeletePurchaseEntryByKey(string peno)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("PENo", peno, DbType.String, ParameterDirection.Input);
                await connection.QueryAsync("DeletePurchaseEntryByKey_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task DeletePurchaseEntryDetailsByPENo(string peno)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("PENo", peno, DbType.String, ParameterDirection.Input);
                await connection.QueryAsync("DeletePurchaseEntryDetailsByPENo_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<PurchaseEntryDto>> GetAllPurchaseEntry()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<PurchaseEntryDto>("GetAllPurchaseEntry_sp", commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<List<PurchaseEntryDto>> GetAllPurchaseEntryforPRE()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<PurchaseEntryDto>("GetAllPurchaseEntryforPRE_sp", commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<int> GetCurrentBalanceBySupplierId(int supplierId)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("SupplierId", supplierId, DbType.Int64, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<int>("GetCurrentBalanceBySupplierId_sp", param, commandType: CommandType.StoredProcedure);
                if(dataObj == null)
                {

                }
                return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<PEOtherChargesDetailsDto>> GetPEOtherChargesDetailsByPENo(string peno)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("PENo", peno, DbType.String, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<PEOtherChargesDetailsDto>("GetPEOtherChargesDetailsByPENo_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.ToList();
            }
        }

        public async Task<List<string>> GetPESerialNosByInventoryId(int InventoryId)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("InventoryId", InventoryId, DbType.String, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<string>("GetPESerialNosByInventoryId_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.ToList();
            }
        }

        public async Task<PurchaseEntryDto> GetPurchaseEntryByKey(string peno)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("PENo", peno, DbType.String, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<PurchaseEntryDto>("GetPurchaseEntryByKey_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<PurchaseEntryDetailsDto>> GetPurchaseEntryDetailsByPENo(string peno)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("PENo", peno, DbType.String, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<PurchaseEntryDetailsDto>("GetPurchaseEntryDetailsByPENo_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.ToList();
            }
        }

        public async Task<PurchaseReturnEntryDto> GetPurchaseReturnEntryByPENo(string peno)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("PENo", peno, DbType.String, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<PurchaseReturnEntryDto>("GetPurchaseReturnEntryByPENo_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.FirstOrDefault();
            }
        }

        public async Task<int> InsertPEOtherChargesDetails(PEOtherChargesDetailsDto model)
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
                    param.Add("PENo", model.PENo, DbType.String, ParameterDirection.Input);
                    param.Add("FinalAmount", model.FinalAmount, DbType.Decimal, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<int>("InsertPEOtherChargesDetails_sp", param, commandType: CommandType.StoredProcedure);
                    return lastInsertedId.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<string> InsertPurchaseEntry(PurchaseEntryDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("PENo", model.PENo, DbType.String, ParameterDirection.Input);
                    param.Add("PurchaseDate", model.PurchaseDate, DbType.DateTime, ParameterDirection.Input);
                    param.Add("SupplierInvoiceNo", model.SupplierInvoiceNo, DbType.String, ParameterDirection.Input);
                    param.Add("SupplierId", model.SupplierId, DbType.Int64, ParameterDirection.Input);
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
                    var lastInsertedId = await connection.QueryAsync<string>("InsertPurchaseEntry_sp", param, commandType: CommandType.StoredProcedure);
                    return lastInsertedId.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<int> InsertPurchaseEntryDetails(PurchaseEntryDetailsDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    //param.Add("Id", model.Id, DbType.Int64, ParameterDirection.Input);
                    param.Add("PENo", model.PENo, DbType.String, ParameterDirection.Input);
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
                    var lastInsertedId = await connection.QueryAsync<int>("InsertPurchaseEntryDetails_sp", param, commandType: CommandType.StoredProcedure);
                    return lastInsertedId.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task InsertPurchaseEntryItemSerialNosDetails(string pENo, int inventoryId, string itemSerialNo)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("PENo", pENo, DbType.String, ParameterDirection.Input);
                    param.Add("InventoryId", inventoryId, DbType.Int32, ParameterDirection.Input);
                    param.Add("ItemSerialNo", itemSerialNo, DbType.String, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync("InsertPurchaseEntryItemSerialNosDetails_sp", param, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
