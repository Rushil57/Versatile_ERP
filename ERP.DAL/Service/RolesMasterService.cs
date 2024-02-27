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
    public class RolesMasterService : BaseRepository, IRolesMasterService
    {
        IConfiguration _configuration;

        public RolesMasterService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task DeleteRoleByKey(int id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("RoleId", id, DbType.Int64, ParameterDirection.Input);
                await connection.QueryAsync("DeleteRoleByKey_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<RolesMasterDto>> GetAllRole()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<RolesMasterDto>("GetAllRole_sp", commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<RolesMasterDto> GetRoleByKey(int id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("RoleId", id, DbType.Int64, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<RolesMasterDto>("GetRoleByKey_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.FirstOrDefault();
            }
        }

        public async Task<int> InsertUpdateRole(RolesMasterDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("RoleId", model.RoleId, DbType.Int64, ParameterDirection.Input);
                    param.Add("RoleName", model.RoleName, DbType.String, ParameterDirection.Input);
                    param.Add("Description", model.Description, DbType.String, ParameterDirection.Input);
                    param.Add("Created_Date", model.Created_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("Modified_Date", model.Modified_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("IsActive", model.IsActive, DbType.Boolean, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<int>("InsertUpdateRole_sp", param, commandType: CommandType.StoredProcedure);
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
