using System;
using System.Collections.Generic;
using System.Text;
using ERP.DAL.Dto;
using System.Threading.Tasks;

namespace ERP.DAL.Abstract
{
    public interface IEmployeeMasterService
    {
        Task<string> InsertUpdateEmployee(EmployeeMasterDto model);

        Task DeleteEmployeeByKey(string id);

        Task<EmployeeMasterDto> GetEmployeeByKey(string id);

        Task<List<EmployeeMasterDto>> GetAllEmployee();

        Task<List<EmployeeMasterDto>> GetEmployeeBySalesPersonRole();
        
    }
}
