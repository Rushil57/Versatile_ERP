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
    public class CompanyBankAccountService : BaseRepository, ICompanyBankAccountService
    {
        IConfiguration _configuration;

        public CompanyBankAccountService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DeleteCompanyBankAccountByKey(int id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                await connection.QueryAsync("DeleteCompanyBankAccountByKey_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<CompanyBankAccountDto>> GetAllCompanyBankAccount()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<CompanyBankAccountDto>("GetAllCompanyBankAccount_sp", commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<CompanyBankAccountDto> GetCompanyBankAccountByKey(int id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<CompanyBankAccountDto>("GetCompanyBankAccountByKey_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.FirstOrDefault();
            }
        }

        public async Task<int> InsertUpdateCompanyBankAccount(CompanyBankAccountDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("Id", model.Id, DbType.Int64, ParameterDirection.Input);
                    param.Add("BankName", model.BankName, DbType.String, ParameterDirection.Input);
                    param.Add("CurrencyOfLedger", model.CurrencyOfLedger, DbType.String, ParameterDirection.Input);
                    param.Add("IsActivateInterestCalculation", model.IsActivateInterestCalculation, DbType.Boolean, ParameterDirection.Input);
                    param.Add("ODLimit", model.ODLimit, DbType.Decimal, ParameterDirection.Input);
                    param.Add("IsChequeBooks", model.IsChequeBooks, DbType.Boolean, ParameterDirection.Input);
                    param.Add("IsChequePrintingConfg", model.IsChequePrintingConfg, DbType.Boolean, ParameterDirection.Input);
                    param.Add("ACHolderName", model.ACHolderName, DbType.String, ParameterDirection.Input);
                    param.Add("ACName", model.ACName, DbType.String, ParameterDirection.Input);
                    param.Add("IFSCCode", model.IFSCCode, DbType.String, ParameterDirection.Input);
                    param.Add("Branch", model.Branch, DbType.String, ParameterDirection.Input);
                    param.Add("Balance", model.Balance, DbType.Decimal, ParameterDirection.Input);
                    param.Add("AccountGroup", model.AccountGroup, DbType.String, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<int>("InsertUpdateCompanyBankAccount_sp", param, commandType: CommandType.StoredProcedure);
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
