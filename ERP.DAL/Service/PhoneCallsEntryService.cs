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
    public class PhoneCallsEntryService : BaseRepository, IPhoneCallsEntryService
    {
        IConfiguration _configuration;

        public PhoneCallsEntryService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DeletePhoneCallsEntryByKey(int id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                await connection.QueryAsync("DeletePhoneCallsEntryByKey_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<PhoneCallsEntryDto>> GetAllPhoneCallsEntry()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<PhoneCallsEntryDto>("GetAllPhoneCallsEntry_sp", commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<PhoneCallsEntryDto> GetPhoneCallsEntryByKey(int id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<PhoneCallsEntryDto>("GetPhoneCallsEntryByKey_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.FirstOrDefault();
            }
        }

        public async Task<int> InsertUpdatePhoneCallsEntry(PhoneCallsEntryDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("Id", model.Id, DbType.Int64, ParameterDirection.Input);
                    param.Add("ACPartyName", model.ACPartyName, DbType.String, ParameterDirection.Input);
                    param.Add("SalesPersonRef", model.SalesPersonRef, DbType.String, ParameterDirection.Input);
                    param.Add("ModelName", model.ModelName, DbType.String, ParameterDirection.Input);
                    param.Add("SerialNumber", model.SerialNumber, DbType.String, ParameterDirection.Input);
                    param.Add("Date", model.Date, DbType.DateTime, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<int>("InsertUpdatePhoneCallsEntry_sp", param, commandType: CommandType.StoredProcedure);
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
