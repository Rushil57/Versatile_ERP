using ERP.DAL.Abstract;
using ERP.DAL.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ERP.DAL.Dto;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using System.Linq;

namespace ERP.DAL.Service
{
    public class InventoryMasterService : BaseRepository, IInventoryMasterService
    {
        IConfiguration _configuration;

        public InventoryMasterService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task DeleteInventoryByKey(int id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                await connection.QueryAsync("DeleteInventoryByKey_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<InventoryMasterDto>> GetAllAccountPartyWithAvailableStock(int branchId)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("BranchId", branchId, DbType.Int64, ParameterDirection.Input);
                var dataList = await connection.QueryAsync<InventoryMasterDto>("GetAllInventoryWithAvailableStock_sp", param, commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<List<InventoryMasterDto>> GetAllInventory()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<InventoryMasterDto>("GetAllInventory_sp", commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<InventoryMasterDto> GetInventoryByKey(int id)
        {
            InventoryMasterDto retObj = new InventoryMasterDto();
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                using (var dataObj = await connection.QueryMultipleAsync("GetInventoryByKey_sp", param, commandType: CommandType.StoredProcedure))
                {
                    retObj = dataObj.Read<InventoryMasterDto>().FirstOrDefault();
                    var GSTObj = dataObj.Read<InventoryGSTDetailsDto>().FirstOrDefault();
                    if (retObj.IsGSTApplicable) {
                        retObj.GSTDetail = GSTObj;
                    }
                }
            }
            return retObj;
        }

        public async Task<int> InsertUpdateInventory(InventoryMasterDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("Id", model.Id, DbType.Int64, ParameterDirection.Input);
                    param.Add("BrandId", model.BrandId, DbType.Int64, ParameterDirection.Input);
                    param.Add("CategoryId", model.CategoryId, DbType.Int64, ParameterDirection.Input);
                    param.Add("ItemName", model.ItemName, DbType.String, ParameterDirection.Input);
                    param.Add("UnitId", model.UnitId, DbType.Int64, ParameterDirection.Input);
                  //  param.Add("BranchId", model.BranchId, DbType.Int64, ParameterDirection.Input);
                    param.Add("TypesOfGood", model.TypesOfGood, DbType.String, ParameterDirection.Input);
                    param.Add("ServiceType", model.ServiceType, DbType.String, ParameterDirection.Input);
                    param.Add("Created_Date", model.Created_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("Modified_Date", model.Modified_Date, DbType.DateTime, ParameterDirection.Input);
                   // param.Add("Rate", model.Rate, DbType.Decimal, ParameterDirection.Input);
                    param.Add("IsGSTApplicable", model.IsGSTApplicable, DbType.Boolean, ParameterDirection.Input);
                    param.Add("IsActive", model.IsActive, DbType.Boolean, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<int>("InsertUpdateInventory_sp", param, commandType: CommandType.StoredProcedure);
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
