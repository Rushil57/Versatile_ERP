using System;
using System.Collections.Generic;
using System.Text;
using ERP.DAL.Dto;
using System.Threading.Tasks;

namespace ERP.DAL.Abstract
{
    public interface IUserService
    {
        Task<int> InsertUpdateUser(UserDto model);

        Task DeleteUserByKey(int id);

        Task<UserDto> GetUserByKey(int id);
        Task<UserDto> GetUserByEmail(string EmailId);
        Task<List<UserDto>> GetAllUser();
        Task<UserDto> GetUserByEmpId(string EmpId);
        Task<EmployeeMasterDto> GetUserByEmailPswd(string Email, string Password);
    }
}
