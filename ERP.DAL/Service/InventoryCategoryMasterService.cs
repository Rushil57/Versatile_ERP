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
   public class InventoryCategoryMasterService : BaseRepository, IInventoryCategoryMasterService
    {
        IConfiguration _configuration;

        public InventoryCategoryMasterService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task DeleteInventoryCategoryByKey(int id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                await connection.QueryAsync("DeleteInventoryCategoryByKey_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<InventoryCategoryMasterDto>> GetAllInventoryCategory()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<InventoryCategoryMasterDto>("GetAllInventoryCategory_sp", commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<InventoryCategoryMasterDto> GetInventoryCategoryByKey(int id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<InventoryCategoryMasterDto>("GetInventoryCategoryByKey_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.FirstOrDefault();
            }
        }

        public async Task<int> InsertUpdateInventoryCategory(InventoryCategoryMasterDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("Id", model.Id, DbType.Int64, ParameterDirection.Input);
                    param.Add("CategoryName", model.CategoryName, DbType.String, ParameterDirection.Input);
                    param.Add("Created_Date", model.Created_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("Modified_Date", model.Modified_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("IsActive", model.IsActive, DbType.Boolean, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<int>("InsertUpdateInventoryCategory_sp", param, commandType: CommandType.StoredProcedure);
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
