using ERP.DAL.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DAL.Abstract
{
    public interface IPurchaseQuotationService
    {
        Task<List<PurchaseQuotationDto>> GetAllPurchaseQuotation();
        Task<string> InsertPurchaseQutotaion(PurchaseQuotationDto model);
        Task<int> InsertPurchaseQutotaionDetails(PurchaseQuotationDetailsDto model);
        Task InsertPurchaseQutotaionItemSerialNosDetails(string pQNo, int inventoryId, string itemSerialNo);
        Task DeletePurchaseQuotationByKey(string pqno);
        Task DeletePurchaseQuotationDetailsByPQNo(string pqno);
        Task<PurchaseQuotationDto> GetPurchaseQuotationByKey(string pqno);
        Task<List<PurchaseQuotationDetailsDto>> GetPurchaseQuotationDetailsByPQNo(string pqno);
        Task<int> InsertPQOtherChargesDetails(PQOtherChargesDetailsDto model);
        Task<List<PQOtherChargesDetailsDto>> GetPQOtherChargesDetailsByPQNo(string pqno);
    }
}
