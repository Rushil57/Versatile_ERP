using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ERP.DAL.Dto;

namespace ERP.DAL.Abstract
{
    public interface ISalesReturnEntryService
    {
        Task<List<SalesReturnEntryDto>> GetAllSalesReturnEntry();
        Task<string> InsertSalesReturnEntry(SalesReturnEntryDto model);
        Task<int> InsertSalesReturnEntryDetails(SalesReturnEntryDetailsDto model);
        Task InsertSalesReturnEntryItemSerialNosDetails(string creditNoteNo, int inventoryId, string itemSerialNo);
        Task DeleteSalesReturnEntryByKey(string creditNoteNo);
        Task DeleteSalesReturnEntryDetailsByCreditNoteNo(string creditNoteNo);
        Task DeleteSalesReturnEntryOtherChargesDetailsByCreditNoteNo(string creditNoteNo);
        Task DeleteSalesReturnEntryItemSerialNosDetailsByCreditNoteNo(string creditNoteNo);
        Task<SalesReturnEntryDto> GetSalesReturnEntryByKey(string creditNoteNo);
        Task<List<SalesReturnEntryDetailsDto>> GetSalesReturnEntryDetailsByCreditNoteNo(string creditNoteNo);
        Task<int> InsertSalesReturnEntryOtherChargesDetails(SalesReturnEntryOtherChargesDetailsDto model);
        Task<List<SalesReturnEntryOtherChargesDetailsDto>> GetSalesReturnEntryOtherChargesDetailsByCreditNoteNo(string creditNoteNo);
        Task<int> GetCurrentBalanceBySupplierId(string supplierId);
        Task<List<string>> GetInValidSerialIdsForSalesReturnByInventoryId(int inventoryId, string CreditNoteNo);
        Task<int> IsSalesReturnCanDelete(string serialIds, string creditNoteNo);
        Task<List<string>> GetSerialNosByCreditNoteNo(string creditNoteNo);
    }
}
