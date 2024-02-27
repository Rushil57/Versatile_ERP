using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ERP.DAL.Abstract;
using ERP.DAL.Dto;
using ERP.DAL.Helper;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using System.Linq;

namespace ERP.DAL.Service
{
    public class PartyPaymentDebitEntryService : BaseRepository, IPartyPaymentDebitEntryService
    {
        IConfiguration _configuration;

        public PartyPaymentDebitEntryService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DeletePartyPaymentDebitEntryByKey(string receiptNo)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("ReceiptNo", receiptNo, DbType.String, ParameterDirection.Input);
                await connection.QueryAsync("DeletePartyPaymentDebitEntryByKey_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task DeletePartyPaymentDebitEntryDetailsByReceiptNo(string receiptNo)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("ReceiptNo", receiptNo, DbType.String, ParameterDirection.Input);
                await connection.QueryAsync("DeletePartyPaymentDebitEntryDetailsByReceiptNo_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<PartyPaymentDebitEntryDto>> GetAllPartyPaymentDebitEntry()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<PartyPaymentDebitEntryDto>("GetAllPartyPaymentDebitEntry_sp", commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<PartyPaymentDebitEntryDto> GetPartyPaymentDebitEntryByKey(string receiptNo)
        {
            PartyPaymentDebitEntryDto retObj = new PartyPaymentDebitEntryDto();
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("ReceiptNo", receiptNo, DbType.String, ParameterDirection.Input);
                using (var dataObj = await connection.QueryMultipleAsync("GetPartyPaymentDebitEntryByKey_sp", param, commandType: CommandType.StoredProcedure))
                {
                    retObj = dataObj.Read<PartyPaymentDebitEntryDto>().FirstOrDefault();
                    var PartyPaymentDebitData = dataObj.Read<PartyPaymentDebitEntryDetailsDto>().ToList();
                    retObj.PartyPaymentDebitEntryDetailsData = PartyPaymentDebitData;
                }
            }
            return retObj; ;
        }

        public async Task<int> InsertPartyPaymentDebitEntryDetails(PartyPaymentDebitEntryDetailsDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("ReceiptNo", model.ReceiptNo, DbType.String, ParameterDirection.Input);
                    param.Add("InvoiceNo", model.InvoiceNo, DbType.String, ParameterDirection.Input);
                    param.Add("Amount", model.Amount, DbType.Decimal, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<int>("InsertPartyPaymentDebitEntryDetails_sp", param, commandType: CommandType.StoredProcedure);
                    return lastInsertedId.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<string> InsertUpdatePartyPaymentDebitEntry(PartyPaymentDebitEntryDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("ReceiptNo", model.ReceiptNo, DbType.String, ParameterDirection.Input);
                    param.Add("PaymentDate", model.PaymentDate, DbType.DateTime, ParameterDirection.Input);
                    param.Add("CompanyBankId", model.CompanyBankId, DbType.Int64, ParameterDirection.Input);
                    param.Add("AccountPartyId", model.AccountPartyId, DbType.Int64, ParameterDirection.Input);
                    param.Add("SalesEntryId", model.SalesEntryId, DbType.String, ParameterDirection.Input);
                    param.Add("Amount", model.Amount, DbType.Decimal, ParameterDirection.Input);
                    param.Add("Narration", model.Narration, DbType.String, ParameterDirection.Input);
                    param.Add("Created_Date", model.Created_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("Modified_Date", model.Modified_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("APCurrentBalance", model.APCurrentBalance, DbType.Decimal, ParameterDirection.Input);
                    param.Add("AgstRef", model.AgstRef, DbType.String, ParameterDirection.Input);
                    param.Add("CompanyCurrentBalance", model.CompanyCurrentBalance, DbType.Decimal, ParameterDirection.Input);
                    param.Add("TotalPaidAmount", model.TotalPaidAmount, DbType.Decimal, ParameterDirection.Input);
                    param.Add("PaymentType", model.PaymentType, DbType.String, ParameterDirection.Input);
                    param.Add("ChequeNo", model.ChequeNo, DbType.String, ParameterDirection.Input);
                    param.Add("ChequeDate", model.ChequeDate, DbType.DateTime, ParameterDirection.Input);
                    param.Add("Bank", model.Bank, DbType.String, ParameterDirection.Input);
                    param.Add("Branch", model.Branch, DbType.String, ParameterDirection.Input);
                    param.Add("Remarks", model.Remarks, DbType.String, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<string>("InsertUpdatePartyPaymentDebitEntry_sp", param, commandType: CommandType.StoredProcedure);
                    return lastInsertedId.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
