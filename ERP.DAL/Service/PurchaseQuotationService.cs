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
    public class PurchaseQuotationService : BaseRepository, IPurchaseQuotationService
    {
        IConfiguration _configuration;

        public PurchaseQuotationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DeletePurchaseQuotationByKey(string pqno)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("PQNo", pqno, DbType.String, ParameterDirection.Input);
                await connection.QueryAsync("DeletePurchaseQuotationByKey_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task DeletePurchaseQuotationDetailsByPQNo(string pqno)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("PQNo", pqno, DbType.String, ParameterDirection.Input);
                await connection.QueryAsync("DeletePurchaseQuotationDetailsByPQNo_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<PurchaseQuotationDto>> GetAllPurchaseQuotation()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<PurchaseQuotationDto>("GetAllPurchaseQuotation_sp", commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<List<PQOtherChargesDetailsDto>> GetPQOtherChargesDetailsByPQNo(string pqno)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("PQNo", pqno, DbType.String, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<PQOtherChargesDetailsDto>("GetPQOtherChargesDetailsByPQNo_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.ToList();
            }
        }

        public async Task<PurchaseQuotationDto> GetPurchaseQuotationByKey(string pqno)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("PQNo", pqno, DbType.String, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<PurchaseQuotationDto>("GetPurchaseQuotationByKey_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<PurchaseQuotationDetailsDto>> GetPurchaseQuotationDetailsByPQNo(string pqno)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("PQNo", pqno, DbType.String, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<PurchaseQuotationDetailsDto>("GetPurchaseQuotationDetailsByPQNo_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.ToList();
            }
        }

        public async Task<int> InsertPQOtherChargesDetails(PQOtherChargesDetailsDto model)
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
                    param.Add("PQNo", model.PQNo, DbType.String, ParameterDirection.Input);
                    param.Add("FinalAmount", model.FinalAmount, DbType.Decimal, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<int>("InsertOtherChargesDetails_sp", param, commandType: CommandType.StoredProcedure);
                    return lastInsertedId.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<string> InsertPurchaseQutotaion(PurchaseQuotationDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("PQNo", model.PQNo, DbType.String, ParameterDirection.Input);
                    param.Add("PQDate", model.PQDate, DbType.DateTime, ParameterDirection.Input);
                    param.Add("BranchId", model.BranchId, DbType.Int32, ParameterDirection.Input);
                    param.Add("SupplierId", model.SupplierId, DbType.Int64, ParameterDirection.Input);
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
                    param.Add("CreatedBy", model.CreatedBy, DbType.Int64, ParameterDirection.Input);
                    param.Add("ModifiedBy", model.ModifiedBy, DbType.Int64, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<string>("InsertPurchaseQutotaion_sp", param, commandType: CommandType.StoredProcedure);
                    return lastInsertedId.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<int> InsertPurchaseQutotaionDetails(PurchaseQuotationDetailsDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    //param.Add("Id", model.Id, DbType.Int64, ParameterDirection.Input);
                    param.Add("PQNo", model.PQNo, DbType.String, ParameterDirection.Input);
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
                    var lastInsertedId = await connection.QueryAsync<int>("InsertPurchaseQutotaionDetails_sp", param, commandType: CommandType.StoredProcedure);
                    return lastInsertedId.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public  async Task InsertPurchaseQutotaionItemSerialNosDetails(string pQNo, int inventoryId, string itemSerialNo)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("PQNo", pQNo, DbType.String, ParameterDirection.Input);
                    param.Add("InventoryId", inventoryId, DbType.Int32, ParameterDirection.Input);
                    param.Add("ItemSerialNo", itemSerialNo, DbType.String, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync("InsertPurchaseQutotaionItemSerialNosDetails_sp", param, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
