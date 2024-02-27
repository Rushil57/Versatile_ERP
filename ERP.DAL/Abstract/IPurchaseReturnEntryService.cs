using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ERP.DAL.Dto;

namespace ERP.DAL.Abstract
{
    public interface IPurchaseReturnEntryService
    {
        Task<List<PurchaseReturnEntryDto>> GetAllPurchaseReturnEntry();
        Task<string> InsertPurchaseReturnEntry(PurchaseReturnEntryDto model);
        Task<int> InsertPurchaseReturnEntryDetails(PurchaseReturnEntryDetailsDto model);
        Task InsertPurchaseReturnEntryItemSerialNosDetails(string debitNoteNo, int inventoryId, string itemSerialNo);
        Task DeletePurchaseReturnEntryByKey(string debitNoteNo);
        Task DeletePurchaseReturnEntryDetailsByDebitNoteNo(string debitNoteNo);
        Task DeletePurchaseReturnEntryOtherChargesDetailsByDebitNoteNo(string debitNoteNo);
        Task DeletePurchaseReturnEntryItemSerialNosDetailsByDebitNoteNo(string debitNoteNo);
        Task<PurchaseReturnEntryDto> GetPurchaseReturnEntryByKey(string debitNoteNo);
        Task<List<PurchaseReturnEntryDetailsDto>> GetPurchaseReturnEntryDetailsByDebitNoteNo(string debitNoteNo);
        Task<int> InsertPurchaseReturnEntryOtherChargesDetails(PurchaseReturnEntryOtherChargesDetailsDto model);
        Task<List<PurchaseReturnEntryOtherChargesDetailsDto>> GetPurchaseReturnEntryOtherChargesDetailsByDebitNoteNo(string debitNoteNo);
        Task<int> GetCurrentBalanceBySupplierId(string supplierId);
        Task<List<string>> GetInValidSerialIdsForPurchReturnByInventoryId(int inventoryId, string PRENo);
        Task<int> IsPurchaseReturnCanDelete(string serialIds, string debitNoteNo);
        Task<List<string>> GetSerialNosByDebitNoteNo(string debitNoteNo);
    }
}
