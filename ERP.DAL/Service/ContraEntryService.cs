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
    public class ContraEntryService : BaseRepository, IContraEntryService
    {
        IConfiguration _configuration;

        public ContraEntryService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DeleteContraEntryByKey(string id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("CONo", id, DbType.String, ParameterDirection.Input);
                await connection.QueryAsync("DeleteContraEntryByKey_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<ContraEntryDto>> GetAllContraEntry()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<ContraEntryDto>("GetAllContraEntry_sp", commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<ContraEntryDto> GetContraEntryByKey(string id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("CONo", id, DbType.String, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<ContraEntryDto>("GetContraEntryByKey_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.FirstOrDefault();
            }
        }

        public async Task<string> InsertUpdateContraEntry(ContraEntryDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("CONo", model.CONo, DbType.String, ParameterDirection.Input);
                    param.Add("SourceBankId", model.SourceBankId, DbType.Int64, ParameterDirection.Input);
                    param.Add("CurrentBalance", model.CurrentBalance, DbType.Decimal, ParameterDirection.Input);
                    param.Add("DestinationBankId", model.DestinationBankId, DbType.Int64, ParameterDirection.Input);
                    param.Add("Amount", model.Amount, DbType.Decimal, ParameterDirection.Input);
                    param.Add("ContraDate", model.ContraDate, DbType.DateTime, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<string>("InsertUpdateContraEntry_sp", param, commandType: CommandType.StoredProcedure);
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
