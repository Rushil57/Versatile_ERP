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
    public class OutwardService : BaseRepository, IOutwardService
    {
        IConfiguration _configuration;

        public OutwardService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task DeleteOutwardByKey(int outwardId)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("OutwardId", outwardId, DbType.Int64, ParameterDirection.Input);
                await connection.QueryAsync("DeleteOutwardByKey_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<OutwardDto>> GetAllOutward()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<OutwardDto>("GetAllOutward_sp", commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<OutwardDto> GetOutwardByKey(int outwardId)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("OutwardId", outwardId, DbType.Int64, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<OutwardDto>("GetOutwardByKey_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.FirstOrDefault();
            }
        }

        public async Task<int> InsertUpdateOutward(OutwardDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("OutwardId", model.OutwardId, DbType.Int64, ParameterDirection.Input);
                    param.Add("OutwardDate", model.OutwardDate, DbType.DateTime, ParameterDirection.Input);
                    param.Add("InwardId", model.InwardId, DbType.Int64, ParameterDirection.Input);
                    param.Add("CourierId", model.CourierId, DbType.Int64, ParameterDirection.Input);
                    param.Add("CourierDate", model.CourierDate, DbType.DateTime, ParameterDirection.Input);
                    param.Add("DocumentNo", model.DocumentNo, DbType.String, ParameterDirection.Input);
                    param.Add("Charges", model.Charges, DbType.Decimal, ParameterDirection.Input);
                    param.Add("ImagePath", model.ImagePath, DbType.String, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<int>("InsertUpdateOutward_sp", param, commandType: CommandType.StoredProcedure);
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
