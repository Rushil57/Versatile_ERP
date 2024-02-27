using ERP.DAL.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DAL.Abstract
{
    public interface IPartyPaymentDebitEntryService
    {
        Task<string> InsertUpdatePartyPaymentDebitEntry(PartyPaymentDebitEntryDto model);

        Task DeletePartyPaymentDebitEntryByKey(string receiptNo);

        Task<PartyPaymentDebitEntryDto> GetPartyPaymentDebitEntryByKey(string receiptNo);

        Task<List<PartyPaymentDebitEntryDto>> GetAllPartyPaymentDebitEntry();

        Task DeletePartyPaymentDebitEntryDetailsByReceiptNo(string receiptNo);

        Task<int> InsertPartyPaymentDebitEntryDetails(PartyPaymentDebitEntryDetailsDto model);
    }
}
