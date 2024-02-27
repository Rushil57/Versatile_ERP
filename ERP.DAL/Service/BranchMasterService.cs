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
    public class BranchMasterService : BaseRepository, IBranchMasterService
    {
        IConfiguration _configuration;

        public BranchMasterService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task DeleteBranchByKey(int id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                await connection.QueryAsync("DeleteBranchByKey_sp", param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<List<BranchMasterDto>> GetAllBranch()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<BranchMasterDto>("GetAllBranch_sp", commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<BranchMasterDto> GetBranchByKey(int id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<BranchMasterDto>("GetBranchByKey_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.FirstOrDefault();
            }
        }

       

        public async Task<int> InsertUpdateBranch(BranchMasterDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("Id", model.Id, DbType.Int64, ParameterDirection.Input);
                    param.Add("BranchName", model.BranchName, DbType.String, ParameterDirection.Input);
                    param.Add("Address", model.Address, DbType.String, ParameterDirection.Input);
                    param.Add("Contact", model.Contact, DbType.String, ParameterDirection.Input);
                    param.Add("Created_Date", model.Created_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("Modified_Date", model.Modified_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("IsActive", model.IsActive, DbType.Boolean, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<int>("InsertUpdateBranch_sp", param, commandType: CommandType.StoredProcedure);
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
