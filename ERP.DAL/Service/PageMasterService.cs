using ERP.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using ERP.DAL.Dto;
using System.Threading.Tasks;
using ERP.DAL.Helper;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using System.Linq;

namespace ERP.DAL.Service
{
    public class PageMasterService : BaseRepository, IPageMasterService
    {
        IConfiguration _configuration;

        public PageMasterService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<PageMasterDto>> GetAllPage()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<PageMasterDto>("GetAllPage_sp", commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }
    }
}
