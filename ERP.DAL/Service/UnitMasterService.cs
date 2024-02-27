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
    public class UnitMasterService : BaseRepository, IUnitMasterService
    {
        IConfiguration _configuration;

        public UnitMasterService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task DeleteUnitByKey(int id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                await connection.QueryAsync("DeleteUnitByKey_sp", param, commandType: CommandType.StoredProcedure);
               // return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<UnitMasterDto>> GetAllUnit()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<UnitMasterDto>("GetAllUnit_sp", commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<UnitMasterDto> GetUnitByKey(int id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<UnitMasterDto>("GetUnitByKey_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.FirstOrDefault();
            }
        }

        public async Task<int> InsertUpdateUnit(UnitMasterDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("Id", model.Id, DbType.Int64, ParameterDirection.Input);
                    param.Add("UnitName", model.UnitName, DbType.String, ParameterDirection.Input);
                    param.Add("Alias", model.Alias, DbType.String, ParameterDirection.Input);
                    param.Add("Created_Date", model.Created_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("Modified_Date", model.Modified_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("IsActive", model.IsActive, DbType.Boolean, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<int>("InsertUpdateUnit_sp", param, commandType: CommandType.StoredProcedure);
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
