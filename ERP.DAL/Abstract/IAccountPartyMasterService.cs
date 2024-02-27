using System;
using System.Collections.Generic;
using System.Text;
using ERP.DAL.Dto;
using System.Threading.Tasks;

namespace ERP.DAL.Abstract
{
    public interface IAccountPartyMasterService
    {
        Task<int> InsertUpdateAccountParty(AccountPartyMasterDto model);

        Task DeleteAccountPartyByKey(int id);

        Task<AccountPartyMasterDto> GetAccountPartyByKey(int id);

        Task<List<AccountPartyMasterDto>> GetAllAccountParty();

        Task<List<AccountPartyMasterDto>> GetAccountPartyBySundery(int IsSundery);
        Task<List<AccountPartyMasterDto>> GetAccountPartyBySunderyOtherCharge();
        Task<List<StateMasterDto>> GetAllState();
    }
}
