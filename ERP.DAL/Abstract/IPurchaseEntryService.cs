using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ERP.DAL.Dto;

namespace ERP.DAL.Abstract
{
    public interface IPurchaseEntryService
    {
        Task<List<PurchaseEntryDto>> GetAllPurchaseEntry();
        Task<string> InsertPurchaseEntry(PurchaseEntryDto model);
        Task<int> InsertPurchaseEntryDetails(PurchaseEntryDetailsDto model);
        Task InsertPurchaseEntryItemSerialNosDetails(string pENo, int inventoryId, string itemSerialNo);
        Task DeletePurchaseEntryByKey(string peno);
        Task DeletePurchaseEntryDetailsByPENo(string peno);
        Task DeletePEOtherChargesDetailsByPENo(string peno);
        Task DeletePEItemSerialNosDetailsByPENo(string peno);
        Task<PurchaseEntryDto> GetPurchaseEntryByKey(string peno);
        Task<List<PurchaseEntryDetailsDto>> GetPurchaseEntryDetailsByPENo(string peno);
        Task<int> InsertPEOtherChargesDetails(PEOtherChargesDetailsDto model);
        Task<List<PEOtherChargesDetailsDto>> GetPEOtherChargesDetailsByPENo(string peno);
        Task<int> GetCurrentBalanceBySupplierId(int supplierId);
        Task<List<string>> GetPESerialNosByInventoryId(int InventoryId);
        Task<List<PurchaseEntryDto>>GetAllPurchaseEntryforPRE();
        Task<string> CheckValidItemSerialNos(string serialIds, string peno);
        Task<PurchaseReturnEntryDto> GetPurchaseReturnEntryByPENo(string peno);
    }
}
