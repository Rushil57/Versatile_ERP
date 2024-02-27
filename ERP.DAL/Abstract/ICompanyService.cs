using System;
using System.Collections.Generic;
using System.Text;
using ERP.DAL.Dto;
using System.Threading.Tasks;

namespace ERP.DAL.Abstract
{
    public interface ICompanyService
    {
        Task<int> InsertUpdateCompany(CompanyDto model);

        Task<CompanyDto> GetAllCompany();
    }
}
