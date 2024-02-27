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
    public class RolesAndRightsMasterService : BaseRepository, IRolesAndRightsMasterService
    {
        IConfiguration _configuration;

        public RolesAndRightsMasterService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DeleteRolesAndRightsByKeys(int RoleId, int PageId)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("RoleId", RoleId, DbType.Int64, ParameterDirection.Input);
                param.Add("PageId", PageId, DbType.Int64, ParameterDirection.Input);
                await connection.QueryAsync("DeleteRolesAndRightsByKeys_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<RolesAndRightsMasterDto>> GetRolesAndRights(int RoleId, int PageId)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("RoleId", RoleId, DbType.Int64, ParameterDirection.Input);
                    param.Add("PageId", PageId, DbType.Int64, ParameterDirection.Input);
                    var dataObj = await connection.QueryAsync<RolesAndRightsMasterDto>("GetRolesAndRightsByKeys_sp", param, commandType: CommandType.StoredProcedure);
                    return dataObj.ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async Task<List<RolesAndRightsMasterDto>> GetRolesAndRightsByRoleId(int RoleId)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("RoleId", RoleId, DbType.Int64, ParameterDirection.Input);
                    var dataObj = await connection.QueryAsync<RolesAndRightsMasterDto>("GetRolesAndRightsByRoleId_sp", param, commandType: CommandType.StoredProcedure);
                    return dataObj.ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<int> InsertUpdateRolesAndRights(RolesAndRightsMasterDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("RightsId", model.RightsId, DbType.Int64, ParameterDirection.Input);
                    param.Add("RoleId", model.RoleId, DbType.Int64, ParameterDirection.Input);
                    param.Add("ActionId", model.ActionId, DbType.Int64, ParameterDirection.Input);
                    param.Add("PageId", model.PageId, DbType.Int64, ParameterDirection.Input);
                    param.Add("IsRight", model.IsRight, DbType.Boolean, ParameterDirection.Input);
                    param.Add("Created_Date", model.Created_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("Modified_Date", model.Modified_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("IsActive", model.IsActive, DbType.Boolean, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<int>("InsertUpdateRolesAndRights_sp", param, commandType: CommandType.StoredProcedure);
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
