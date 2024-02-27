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
    public class UserService : BaseRepository, IUserService
    {
        IConfiguration _configuration;

        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DeleteUserByKey(int id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("UserId", id, DbType.Int64, ParameterDirection.Input);
                await connection.QueryAsync("DeleteUserByKey_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<UserDto>> GetAllUser()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<UserDto>("GetAllUser_sp", commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<UserDto> GetUserByKey(int id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("UserId", id, DbType.Int64, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<UserDto>("GetUserByKey_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.FirstOrDefault();
            }
        }

        public async Task<int> InsertUpdateUser(UserDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("UserId", model.UserId, DbType.Int64, ParameterDirection.Input);
                    param.Add("EmpId", model.EmpId, DbType.String, ParameterDirection.Input);
                    param.Add("Email", model.Email, DbType.String, ParameterDirection.Input);
                    param.Add("Password", model.Password, DbType.String, ParameterDirection.Input);
                    param.Add("Created_Date", model.Created_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("Modified_Date", model.Modified_Date, DbType.DateTime, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<int>("InsertUpdateUser_sp", param, commandType: CommandType.StoredProcedure);
                    return lastInsertedId.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<UserDto> GetUserByEmpId(string EmpId)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("EmpId", EmpId, DbType.String, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<UserDto>("GetUserByEmpId_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.FirstOrDefault();
            }
        }

        public async Task<EmployeeMasterDto> GetUserByEmailPswd(string Email, string Password)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("Email", Email, DbType.String, ParameterDirection.Input);
                param.Add("Password", Password, DbType.String, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<EmployeeMasterDto>("GetUserByEmailPswd_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.FirstOrDefault();
            }
        }

        public async Task<UserDto> GetUserByEmail(string EmailId)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("Email", EmailId, DbType.String, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<UserDto>("GetUserByEmail_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.FirstOrDefault();
            }
        }
    }
}
