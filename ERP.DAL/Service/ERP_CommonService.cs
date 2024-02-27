using Dapper;
using ERP.DAL.Abstract;
using ERP.DAL.Helper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.DAL.Service
{
    public class ERP_CommonService : BaseRepository, IERP_CommonService
    {
        IConfiguration _configuration;
        public ERP_CommonService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> GetIdByTable(string tableName)
        {
            try
            {
                string retObj = string.Empty;
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("tableName", tableName, DbType.String, ParameterDirection.Input);
                    var obj = await connection.QueryAsync("GetIdByTable", param, commandType: CommandType.StoredProcedure);
                    var parmObj = obj.FirstOrDefault();
                    retObj = parmObj.PrefixKey + Convert.ToString(parmObj.CurrentId + 1).PadLeft(parmObj.MinDigit, '0');
                    return retObj;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
