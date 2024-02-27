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
    public class StockTransferService : BaseRepository, IStockTransferService
    {
        IConfiguration _configuration;

        public StockTransferService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DeleteStockTransferDestinationByKey(int id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                await connection.QueryAsync("DeleteStockTransferDestinationByKey_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task DeleteStockTransferDestinationDetailsByDestiantionId(int destId)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("StockTransferDestinationId", destId, DbType.Int64, ParameterDirection.Input);
                await connection.QueryAsync("DeleteStockTransferDestinationDetailsByDestiantionId_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task DeleteStockTransferSourceByKey(int id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                await connection.QueryAsync("DeleteStockTransferSourceByKey_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task DeleteStockTransferSourceDetailsBySourceId(int sourceId)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("StockTransferSourceId", sourceId, DbType.Int64, ParameterDirection.Input);
                await connection.QueryAsync("DeleteStockTransferSourceDetailsBySourceId_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<StockTransferDestinationDto>> GetAllStockTransferDestination()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<StockTransferDestinationDto>("GetAllStockTransferDestination_sp", commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<List<StockTransferSourceDto>> GetAllStockTransferSource()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<StockTransferSourceDto>("GetAllStockTransferSource_sp", commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<StockTransferDestinationDto> GetStockTransferDestinationByKey(int id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<StockTransferDestinationDto>("GetStockTransferDestinationByKey_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<StockTransferDestinationDetailsDto>> GetStockTransferDestinationDetailsByDestinationId(int destId)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("StockTransferDestinationId", destId, DbType.Int64, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<StockTransferDestinationDetailsDto>("GetStockTransferDestinationDetailsByDestinationId_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.ToList();
            }
        }

        public async Task<StockTransferSourceDto> GetStockTransferSourceByKey(int id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<StockTransferSourceDto>("GetStockTransferSourceByKey_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<StockTransferSourceDetailsDto>> GetStockTransferSourceDetailsBySourceId(int sourceId)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("StockTransferSourceId", sourceId, DbType.Int64, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<StockTransferSourceDetailsDto>("GetStockTransferSourceDetailsBySourceId_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.ToList();
            }
        }

        public async Task InsertStockTransferDestinationItemSerialNosDetails(string stockTransferId, int inventoryId, string itemSerialNo)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("StockTransferId", stockTransferId, DbType.String, ParameterDirection.Input);
                    param.Add("InventoryId", inventoryId, DbType.Int32, ParameterDirection.Input);
                    param.Add("ItemSerialNo", itemSerialNo, DbType.String, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync("InsertStockTransferDestinationItemSerialNosDetails_sp", param, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task InsertStockTransferSourceItemSerialNosDetails(string stockTransferId, int inventoryId, string itemSerialNo)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("StockTransferId", stockTransferId, DbType.String, ParameterDirection.Input);
                    param.Add("InventoryId", inventoryId, DbType.Int32, ParameterDirection.Input);
                    param.Add("ItemSerialNo", itemSerialNo, DbType.String, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync("InsertStockTransferSourceItemSerialNosDetails_sp", param, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<string> InsertUpdateStockTransferDestination(StockTransferDestinationDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    //param.Add("Id", model.Id, DbType.Int64, ParameterDirection.Input);
                    param.Add("StockTransferDate", model.StockTransferDate, DbType.DateTime, ParameterDirection.Input);
                    param.Add("StockTransferId", model.StockTransferId, DbType.String, ParameterDirection.Input);
                    param.Add("BranchId", model.BranchId, DbType.Int64, ParameterDirection.Input);
                    param.Add("TotalQty", model.TotalQty, DbType.Int32, ParameterDirection.Input);
                    param.Add("TotalAmount", model.TotalAmount, DbType.Decimal, ParameterDirection.Input);
                    param.Add("Narration", model.Narration, DbType.String, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<string>("InsertUpdateStockTransferDestination_sp", param, commandType: CommandType.StoredProcedure);
                    return lastInsertedId.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<int> InsertUpdateStockTransferDestinationDetails(StockTransferDestinationDetailsDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    //param.Add("Id", model.Id, DbType.Int64, ParameterDirection.Input);
                    param.Add("StockTransferId", model.StockTransferId, DbType.String, ParameterDirection.Input);
                    param.Add("InventoryId", model.InventoryId, DbType.Int64, ParameterDirection.Input);
                    param.Add("Qty", model.Qty, DbType.Int32, ParameterDirection.Input);
                    param.Add("Rate", model.Rate, DbType.Decimal, ParameterDirection.Input);
                    param.Add("Amount", model.Amount, DbType.Decimal, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<int>("InsertUpdateStockTransferDestinationDetails_sp", param, commandType: CommandType.StoredProcedure);
                    return lastInsertedId.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<string> InsertUpdateStockTransferSource(StockTransferSourceDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("Id", model.Id, DbType.String, ParameterDirection.Input);
                    param.Add("StockTransferDate", model.StockTransferDate, DbType.DateTime, ParameterDirection.Input);
                    param.Add("BranchId", model.BranchId, DbType.Int64, ParameterDirection.Input);
                    param.Add("TotalQty", model.TotalQty, DbType.Int32, ParameterDirection.Input);
                    param.Add("TotalAmount", model.TotalAmount, DbType.Decimal, ParameterDirection.Input);
                    param.Add("Narration", model.Narration, DbType.String, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<string>("InsertUpdateStockTransferSource_sp", param, commandType: CommandType.StoredProcedure);
                    return lastInsertedId.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<int> InsertUpdateStockTransferSourceDetails(StockTransferSourceDetailsDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    //param.Add("Id", model.Id, DbType.Int64, ParameterDirection.Input);
                    param.Add("StockTransferId", model.StockTransferId, DbType.String, ParameterDirection.Input);
                    param.Add("InventoryId", model.InventoryId, DbType.Int64, ParameterDirection.Input);
                    param.Add("Qty", model.Qty, DbType.Int32, ParameterDirection.Input);
                    param.Add("Rate", model.Rate, DbType.Decimal, ParameterDirection.Input);
                    param.Add("Amount", model.Amount, DbType.Decimal, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<int>("InsertUpdateStockTransferSourceDetails_sp", param, commandType: CommandType.StoredProcedure);
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
