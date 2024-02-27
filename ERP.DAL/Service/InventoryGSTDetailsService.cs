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
    public class InventoryGSTDetailsService : BaseRepository, IInventoryGSTDetailsService
    {
        IConfiguration _configuration;

        public InventoryGSTDetailsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DeleteInventoryGSTDetailsByKey(int id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                await connection.QueryAsync("DeleteInventoryGSTDetailsByKey_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }
        public async Task DeleteInventoryGSTDetailsByInventoryId(int id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("InventoryId", id, DbType.Int64, ParameterDirection.Input);
                await connection.QueryAsync("DeleteInventoryGSTDetailsByInventoryId_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<InventoryGSTDetailsDto>> GetAllInventoryGSTDetails()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<InventoryGSTDetailsDto>("GetAllInventoryGSTDetails_sp", commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<InventoryGSTDetailsDto> GetInventoryGSTDetailsByInventoryId(int InventoryId)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("InventoryId", InventoryId, DbType.Int64, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<InventoryGSTDetailsDto>("GetInventoryGSTDetailsByInventoryId_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.FirstOrDefault();
            }
        }

        public async Task<InventoryGSTDetailsDto> GetInventoryGSTDetailsByKey(int id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<InventoryGSTDetailsDto>("GetInventoryGSTDetailsByKey_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.FirstOrDefault();
            }
        }

        public async Task<int> InsertUpdateInventoryGSTDetails(InventoryGSTDetailsDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("Id", model.Id, DbType.Int64, ParameterDirection.Input);
                    param.Add("InventoryId", model.InventoryId, DbType.Int64, ParameterDirection.Input);
                    param.Add("HSNCode", model.HSNCode, DbType.String, ParameterDirection.Input);
                    param.Add("CalculationType", model.CalculationType, DbType.String, ParameterDirection.Input);
                    param.Add("Taxability", model.Taxability, DbType.String, ParameterDirection.Input);
                    param.Add("IntegratedTax", model.IntegratedTax, DbType.Decimal, ParameterDirection.Input);
                    param.Add("CentralTax", model.CentralTax, DbType.Decimal, ParameterDirection.Input);
                    param.Add("StateTax", model.StateTax, DbType.Decimal, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<int>("InsertUpdateInventoryGSTDetails_sp", param, commandType: CommandType.StoredProcedure);
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
