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
    public class CourierMasterService : BaseRepository, ICourierMasterService
    {
        IConfiguration _configuration;

        public CourierMasterService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task DeleteCourierByKey(int id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                await connection.QueryAsync("DeleteCourierByKey_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<CourierMasterDto>> GetAllCourier()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<CourierMasterDto>("GetAllCourier_sp", commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<CourierMasterDto> GetCourierByKey(int id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<CourierMasterDto>("GetCourierByKey_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.FirstOrDefault();
            }
        }

        public async Task<int> InsertUpdateCourier(CourierMasterDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("Id", model.Id, DbType.Int64, ParameterDirection.Input);
                    param.Add("CourierName", model.CourierName, DbType.String, ParameterDirection.Input);
                    param.Add("Address", model.Address, DbType.String, ParameterDirection.Input);
                    param.Add("Website", model.Website, DbType.String, ParameterDirection.Input);
                    param.Add("Mobile", model.Mobile, DbType.String, ParameterDirection.Input);
                    param.Add("Contact", model.Contact, DbType.String, ParameterDirection.Input);
                    param.Add("Remarks", model.Remarks, DbType.String, ParameterDirection.Input);
                    param.Add("Created_Date", model.Created_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("Modified_Date", model.Modified_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("IsActive", model.IsActive, DbType.Boolean, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<int>("InsertUpdateCourier_sp", param, commandType: CommandType.StoredProcedure);
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
