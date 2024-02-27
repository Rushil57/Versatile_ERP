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
    public class TransactionService : BaseRepository, ITransactionService
    {
        IConfiguration _configuration;

        public TransactionService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DeleteStockItemDetailsByInventoryId(int inventoryId)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("InventoryId", inventoryId, DbType.Int64, ParameterDirection.Input);
                await connection.QueryAsync("DeleteStockItemDetailsByInventoryId_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task DeleteStockMovementByInventoryId(int inventoryId)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("InventoryId", inventoryId, DbType.Int64, ParameterDirection.Input);
                await connection.QueryAsync("DeleteStockMovementByInventoryId_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task DeleteStockMovementByTranCode(string tranCode)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("TranCode", tranCode, DbType.String, ParameterDirection.Input);
                await connection.QueryAsync("DeleteStockMovementByTranCode_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task<int> GetAvailableStockItemDetailsByInventoryId(int inventoryId)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("InventoryId", inventoryId, DbType.Int64, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<int>("GetAvailableStockItemDetailsByInventoryId_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.FirstOrDefault();
            }
        }
        public async Task<int> InsertStockMovementSerialDetails(StockMovementSerialDetailsDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("InventoryId", model.InventoryId, DbType.Int64, ParameterDirection.Input);
                    param.Add("BranchId", model.BranchId, DbType.Int64, ParameterDirection.Input);
                    param.Add("FYId", model.FYId, DbType.Int64, ParameterDirection.Input);
                    param.Add("TranBook", model.TranBook, DbType.String, ParameterDirection.Input);
                    param.Add("TranType", model.TranType, DbType.String, ParameterDirection.Input);
                    param.Add("TranId", model.TranId, DbType.String, ParameterDirection.Input);
                    param.Add("TranDetailId", model.TranDetailId, DbType.Int64, ParameterDirection.Input);
                    param.Add("Qty", model.Qty, DbType.Int32, ParameterDirection.Input);
                    param.Add("SerailNo", model.SerailNo, DbType.String, ParameterDirection.Input);
                    param.Add("Created_Date", model.Created_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("Modified_Date", model.Modified_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("StockMovementId", model.StockMovementId, DbType.Int64, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<int>("InsertStockMovementSerialDetails_sp", param, commandType: CommandType.StoredProcedure);
                    return lastInsertedId.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<int> InsertUpdateStockItemDetails(StockItemDetailsDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("Id", model.Id, DbType.Int64, ParameterDirection.Input);
                    param.Add("InventoryId", model.InventoryId, DbType.Int64, ParameterDirection.Input);
                    param.Add("BranchId", model.BranchId, DbType.Int64, ParameterDirection.Input);
                    param.Add("UnitId", model.UnitId, DbType.Int64, ParameterDirection.Input);
                    param.Add("FYId", model.FYId, DbType.Int64, ParameterDirection.Input);
                    param.Add("AvailableQty", model.AvailableQty, DbType.Int32, ParameterDirection.Input);
                    param.Add("Created_by", model.Created_by, DbType.Int64, ParameterDirection.Input);
                    param.Add("Modified_by", model.Modified_by, DbType.Int64, ParameterDirection.Input);
                    param.Add("Created_Date", model.Created_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("Modified_Date", model.Modified_Date, DbType.DateTime, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<int>("InsertUpdateStockItemDetails_sp", param, commandType: CommandType.StoredProcedure);
                    return lastInsertedId.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task InsertUpdateStockMovement(StockMovementDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("Id", model.Id, DbType.Int64, ParameterDirection.Input);
                    param.Add("InventoryId", model.InventoryId, DbType.Int64, ParameterDirection.Input);
                    param.Add("BranchId", model.BranchId, DbType.Int64, ParameterDirection.Input);
                    param.Add("TranCode", model.TranCode, DbType.String, ParameterDirection.Input);
                    param.Add("TranId", model.TranId, DbType.String, ParameterDirection.Input);
                    param.Add("TranDetailId", model.TranDetailId, DbType.Int64, ParameterDirection.Input);
                    param.Add("FYId", model.FYId, DbType.Int64, ParameterDirection.Input);
                    param.Add("UnitId", model.UnitId, DbType.Int64, ParameterDirection.Input);
                    param.Add("TranRate", model.TranRate, DbType.Decimal, ParameterDirection.Input);
                    param.Add("TranQty", model.TranQty, DbType.Int32, ParameterDirection.Input);
                    param.Add("TranAmount", model.TranAmount, DbType.Decimal, ParameterDirection.Input);
                    param.Add("TranBook", model.TranBook, DbType.String, ParameterDirection.Input);
                    param.Add("TranType", model.TranType, DbType.String, ParameterDirection.Input);
                    param.Add("Created_Date", model.Created_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("Modified_Date", model.Modified_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("InsertFrom", model.InsertFrom, DbType.String, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<int>("InsertUpdateStockMovement_sp", param, commandType: CommandType.StoredProcedure);

                    // Insert data in serail table if Qty  > 0
                    if (model.TranQty > 0 && model.SerialNos != null) {
                        foreach (var serialid in model.SerialNos)
                        {
                            StockMovementSerialDetailsDto obj = new StockMovementSerialDetailsDto();
                            obj.InventoryId = model.InventoryId;
                            obj.BranchId = model.BranchId;
                            obj.FYId = model.FYId;
                            obj.TranBook = model.TranBook;
                            obj.TranType = model.TranType;
                            obj.TranId = model.TranId;
                            obj.TranDetailId = model.TranDetailId;
                            obj.Qty = 1;
                            obj.Created_Date = model.Created_Date;
                            obj.Modified_Date = model.Modified_Date;
                            obj.SerailNo = serialid;
                            obj.StockMovementId = lastInsertedId.FirstOrDefault();
                            await InsertStockMovementSerialDetails(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
