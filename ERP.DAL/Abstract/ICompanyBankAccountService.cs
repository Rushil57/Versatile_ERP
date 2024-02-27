using System;
using System.Collections.Generic;
using System.Text;
using ERP.DAL.Dto;
using System.Threading.Tasks;

namespace ERP.DAL.Abstract
{
    public interface ICompanyBankAccountService
    {
        Task<int> InsertUpdateCompanyBankAccount(CompanyBankAccountDto model);

        Task DeleteCompanyBankAccountByKey(int id);

        Task<CompanyBankAccountDto> GetCompanyBankAccountByKey(int id);

        Task<List<CompanyBankAccountDto>> GetAllCompanyBankAccount();
    }
}
