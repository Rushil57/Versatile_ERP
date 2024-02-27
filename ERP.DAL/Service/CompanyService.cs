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
    public class CompanyService : BaseRepository, ICompanyService
    {
        IConfiguration _configuration;

        public CompanyService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<CompanyDto> GetAllCompany()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<CompanyDto>("GetAllCompany_sp", commandType: CommandType.StoredProcedure);
                return dataList.FirstOrDefault();
            }
        }

        public async Task<int> InsertUpdateCompany(CompanyDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("CompanyId", model.CompanyId, DbType.Int64, ParameterDirection.Input);
                    param.Add("CompanyName", model.CompanyName, DbType.String, ParameterDirection.Input);
                    param.Add("CompanyAddress", model.CompanyAddress, DbType.String, ParameterDirection.Input);
                    param.Add("City", model.City, DbType.String, ParameterDirection.Input);
                    param.Add("State", model.State, DbType.String, ParameterDirection.Input);
                    param.Add("Country", model.Country, DbType.String, ParameterDirection.Input);
                    param.Add("ZipCode", model.ZipCode, DbType.String, ParameterDirection.Input);
                    param.Add("ContactNo", model.ContactNo, DbType.String, ParameterDirection.Input);
                    param.Add("GSTNo", model.GSTNo, DbType.String, ParameterDirection.Input);
                    param.Add("CIN", model.CIN, DbType.String, ParameterDirection.Input);
                    param.Add("Email", model.Email, DbType.String, ParameterDirection.Input);
                    param.Add("MailingName", model.MailingName, DbType.String, ParameterDirection.Input);
                    param.Add("PhoneNo", model.PhoneNo, DbType.String, ParameterDirection.Input);
                    param.Add("FAXNo", model.FAXNo, DbType.String, ParameterDirection.Input);
                    param.Add("Website", model.Website, DbType.String, ParameterDirection.Input);
                    param.Add("FinancialYearBegins", model.FinancialYearBegins, DbType.DateTime, ParameterDirection.Input);
                    param.Add("BooksBeginningsFrom", model.BooksBeginningsFrom, DbType.DateTime, ParameterDirection.Input);
                    param.Add("PANNumber", model.PANNumber, DbType.String, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<int>("InsertUpdateCompany_sp", param, commandType: CommandType.StoredProcedure);
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
