using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ERP.DAL.Dto;

namespace ERP.DAL.Abstract
{
    public interface ISalesEntryService
    {
        Task<List<SalesEntryDto>> GetAllSalesEntry();
        Task<string> InsertSalesEntry(SalesEntryDto model);
        Task<int> InsertSalesEntryDetails(SalesEntryDetailsDto model);
        Task InsertSalesEntryItemSerialNosDetails(string salesEntryNo, int inventoryId, string itemSerialNo);
        Task DeleteSalesEntryByKey(string salesEntryNo);
        Task DeleteSalesEntryDetailsBySalesEntryNo(string salesEntryNo);
        Task DeleteSEOtherChargesDetailsBySalesEntryNo(string salesEntryNo);
        Task DeleteSEItemSerialNosDetailsBySalesEntryNo(string salesEntryNo);
        Task<SalesEntryDto> GetSalesEntryByKey(string salesEntryNo);
        Task<List<SalesEntryDetailsDto>> GetSalesEntryDetailsBySalesEntryNo(string salesEntryNo);
        Task<int> InsertSEOtherChargesDetails(SEOtherChargesDetailsDto model);
        Task<List<SEOtherChargesDetailsDto>> GetSEOtherChargesDetailsBySalesEntryNo(string salesEntryNo);
        Task<int> GetCurrentSEBalanceBySupplierId(int supplierId);
        Task<List<string>> GetSESerialNosByInventoryId(int InventoryId);
        Task<List<SalesEntryDto>> GetAllSalesEntryBySupplierId(int supplierId);
        Task<List<string>> GetAvailableSerialIdByInventoryId(int inventoryId, int branchId);
        Task<List<SalesEntryDto>> GetAllSalesEntryforSRE();
        Task<SalesReturnEntryDto> GetSalesReturnEntryBySalesEntryNo(string salesEntryNo);
    }
}
