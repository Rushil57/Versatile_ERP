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
    public class InwardService : BaseRepository, IInwardService
    {
        IConfiguration _configuration;

        public InwardService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task DeleteInwardByKey(int inwardId)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("InwardId", inwardId, DbType.Int64, ParameterDirection.Input);
                await connection.QueryAsync("DeleteInwardByKey_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<InwardDto>> GetAllInward()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<InwardDto>("GetAllInward_sp", commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<InwardDto> GetInwardByKey(int inwardId)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("InwardId", inwardId, DbType.Int64, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<InwardDto>("GetInwardByKey_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.FirstOrDefault();
            }
        }

        public async Task<int> InsertUpdateInward(InwardDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("InwardId", model.InwardId, DbType.Int64, ParameterDirection.Input);
                    param.Add("InwardDate", model.InwardDate, DbType.DateTime, ParameterDirection.Input);
                    param.Add("ACPartyId", model.ACPartyId, DbType.Int64, ParameterDirection.Input);
                    param.Add("InventoryId", model.InventoryId, DbType.Int64, ParameterDirection.Input);
                    param.Add("BrandId", model.BrandId, DbType.Int64, ParameterDirection.Input);
                    param.Add("SerialNo", model.SerialNo, DbType.String, ParameterDirection.Input);
                    param.Add("ProblemId", model.ProblemId, DbType.Int64, ParameterDirection.Input);
                    param.Add("ApproxCharge", model.ApproxCharge, DbType.Decimal, ParameterDirection.Input);
                    param.Add("ImagePath", model.ImagePath, DbType.String, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<int>("InsertUpdateInward_sp", param, commandType: CommandType.StoredProcedure);
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
