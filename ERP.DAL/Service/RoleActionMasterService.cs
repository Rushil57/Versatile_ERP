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
    public class RoleActionMasterService : BaseRepository, IRoleActionMasterService
    {
        IConfiguration _configuration;

        public RoleActionMasterService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task DeleteRoleActionByKey(int id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("ActionId", id, DbType.Int64, ParameterDirection.Input);
                await connection.QueryAsync("DeleteRoleActionByKey_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<RoleActionMasterDto>> GetAllRoleAction()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<RoleActionMasterDto>("GetAllRoleAction_sp", commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<RoleActionMasterDto> GetRoleActionByKey(int id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("ActionId", id, DbType.Int64, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<RoleActionMasterDto>("GetRoleActionByKey_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.FirstOrDefault();
            }
        }

        public async Task<int> InsertUpdateRoleAction(RoleActionMasterDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("ActionId", model.ActionId, DbType.Int64, ParameterDirection.Input);
                    param.Add("ActionName", model.ActionName, DbType.String, ParameterDirection.Input);
                    param.Add("Created_Date", model.Created_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("Modified_Date", model.Modified_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("IsActive", model.IsActive, DbType.Boolean, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<int>("InsertUpdateRoleAction_sp", param, commandType: CommandType.StoredProcedure);
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
