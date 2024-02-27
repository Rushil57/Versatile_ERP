using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DAL.Abstract
{
    public interface IERP_CommonService
    {
        Task<string> GetIdByTable(string tableName);
    }
}
