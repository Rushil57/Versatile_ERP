using ERP.DAL.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DAL.Abstract
{
    public interface IPageMasterService
    {
        Task<List<PageMasterDto>> GetAllPage();
    }
}
