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
    public class EmployeeMasterService : BaseRepository, IEmployeeMasterService
    {
        IConfiguration _configuration;

        public EmployeeMasterService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DeleteEmployeeByKey(string id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("EmpId", id, DbType.String, ParameterDirection.Input);
                await connection.QueryAsync("DeleteEmployeeByKey_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<EmployeeMasterDto>> GetAllEmployee()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<EmployeeMasterDto>("GetAllEmployee_sp", commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<EmployeeMasterDto> GetEmployeeByKey(string id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("EmpId", id, DbType.String, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<EmployeeMasterDto>("GetEmployeeByKey_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<EmployeeMasterDto>> GetEmployeeBySalesPersonRole()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<EmployeeMasterDto>("GetEmployeeBySalesPersonRole_sp", commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<string> InsertUpdateEmployee(EmployeeMasterDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("EmpId", model.EmpId, DbType.String, ParameterDirection.Input);
                    param.Add("Title", model.Title, DbType.String, ParameterDirection.Input);
                    param.Add("FirstName", model.FirstName, DbType.String, ParameterDirection.Input);
                    param.Add("LastName", model.LastName, DbType.String, ParameterDirection.Input);
                    param.Add("Gender", model.Gender, DbType.String, ParameterDirection.Input);
                    param.Add("DateOfBirth", model.DateOfBirth, DbType.DateTime, ParameterDirection.Input);
                    param.Add("HireDate", model.HireDate, DbType.DateTime, ParameterDirection.Input);
                    param.Add("Email", model.Email, DbType.String, ParameterDirection.Input);
                    param.Add("Phone", model.Phone, DbType.String, ParameterDirection.Input);
                    param.Add("IsUser", model.IsUser, DbType.Boolean, ParameterDirection.Input);
                    param.Add("Address", model.Address, DbType.String, ParameterDirection.Input);
                    param.Add("Created_Date", model.Created_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("Modified_Date", model.Modified_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("IsActive", model.IsActive, DbType.Boolean, ParameterDirection.Input);
                    param.Add("RoleId", model.RoleId, DbType.Int64, ParameterDirection.Input);
                    param.Add("PhotoUrl", model.PhotoUrl, DbType.String, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<string>("InsertUpdateEmployee_sp", param, commandType: CommandType.StoredProcedure);
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
