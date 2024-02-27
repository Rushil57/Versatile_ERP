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
    public class AccountPartyMasterService : BaseRepository, IAccountPartyMasterService
    {
        IConfiguration _configuration;

        public AccountPartyMasterService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DeleteAccountPartyByKey(int id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                await connection.QueryAsync("DeleteAccountPartyByKey_sp", param, commandType: CommandType.StoredProcedure);
                // return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<AccountPartyMasterDto>> GetAccountPartyBySundery(int IsSundery)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("IsSundery", IsSundery, DbType.Int32, ParameterDirection.Input);
               // var dataList = await connection.QueryAsync<AccountPartyMasterDto>("GetAccountPartyBySundery_sp", commandType: CommandType.StoredProcedure);
                var dataList = await connection.QueryAsync<AccountPartyMasterDto>("GetAccountPartyBySundery_sp", param, commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<AccountPartyMasterDto> GetAccountPartyByKey(int id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                var dataObj = await connection.QueryAsync<AccountPartyMasterDto>("GetAccountPartyByKey_sp", param, commandType: CommandType.StoredProcedure);
                return dataObj.FirstOrDefault();
            }
        }

        public async Task<List<AccountPartyMasterDto>> GetAllAccountParty()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<AccountPartyMasterDto>("GetAllAccountParty_sp", commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<List<StateMasterDto>> GetAllState()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<StateMasterDto>("GetAllState_sp", commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<int> InsertUpdateAccountParty(AccountPartyMasterDto model)
        {
            try
            {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("Id", model.Id, DbType.Int64, ParameterDirection.Input);
                    param.Add("Name", model.Name, DbType.String, ParameterDirection.Input);
                    param.Add("IsSundery", model.IsSundery, DbType.Int32, ParameterDirection.Input);
                    //param.Add("CurrencyOfLedger", model.CurrencyOfLedger, DbType.String, ParameterDirection.Input);
                    param.Add("IsMaintainBalanceByBill", model.IsMaintainBalanceByBill, DbType.Boolean, ParameterDirection.Input);
                    param.Add("DefaultCreditPeriod", model.DefaultCreditPeriod, DbType.Int32, ParameterDirection.Input);
                    param.Add("CreditLimit", model.CreditLimit, DbType.Decimal, ParameterDirection.Input);
                    //param.Add("IsInventoryValueEffected", model.IsInventoryValueEffected, DbType.Boolean, ParameterDirection.Input);
                    param.Add("IsActivateInterestCalculation", model.IsActivateInterestCalculation, DbType.Boolean, ParameterDirection.Input);
                    param.Add("OpeningDate", model.OpeningDate, DbType.DateTime, ParameterDirection.Input);
                    param.Add("Created_Date", model.Created_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("Modified_Date", model.Modified_Date, DbType.DateTime, ParameterDirection.Input);
                    param.Add("IsActive", model.IsActive, DbType.Boolean, ParameterDirection.Input);
                    param.Add("Email", model.Email, DbType.String, ParameterDirection.Input);
                    param.Add("MobileNo", model.MobileNo, DbType.String, ParameterDirection.Input);
                    param.Add("Interest", model.Interest, DbType.Decimal, ParameterDirection.Input);
                    //param.Add("AccountType", model.AccountType, DbType.Int32, ParameterDirection.Input);
                    param.Add("Landline", model.Landline, DbType.String, ParameterDirection.Input);
                    param.Add("Email2", model.Email2, DbType.String, ParameterDirection.Input);
                    param.Add("Contact", model.Contact, DbType.String, ParameterDirection.Input);
                    param.Add("ContactPerson1", model.ContactPerson1, DbType.String, ParameterDirection.Input);
                    param.Add("ContactPerson2", model.ContactPerson2, DbType.String, ParameterDirection.Input);
                    param.Add("MainAddress", model.MainAddress, DbType.String, ParameterDirection.Input);
                    param.Add("DeliveryAddress", model.DeliveryAddress, DbType.String, ParameterDirection.Input);
                    param.Add("DeliveryAddressContactPersonName", model.DeliveryAddressContactPersonName, DbType.String, ParameterDirection.Input);
                    param.Add("DeliveryAddressContactPersonNo", model.DeliveryAddressContactPersonNo, DbType.String, ParameterDirection.Input);
                    param.Add("InterestPeriod", model.InterestPeriod, DbType.String, ParameterDirection.Input);
                    param.Add("SalesPersonId", model.SalesPersonId, DbType.String, ParameterDirection.Input);
                    param.Add("PDC", model.PDC, DbType.Boolean, ParameterDirection.Input);
                    param.Add("GSTDetails", model.GSTDetails, DbType.String, ParameterDirection.Input);
                    param.Add("GSTNumber", model.GSTNumber, DbType.String, ParameterDirection.Input);
                    param.Add("PANNumber", model.PANNumber, DbType.String, ParameterDirection.Input);
                    param.Add("State", model.State, DbType.String, ParameterDirection.Input);
                    param.Add("HSNCode", model.HSNCode, DbType.String, ParameterDirection.Input);
                    param.Add("TypesOfGood", model.TypesOfGood, DbType.String, ParameterDirection.Input);
                    param.Add("Taxability", model.Taxability, DbType.String, ParameterDirection.Input);
                    param.Add("IGST", model.IGST, DbType.Decimal, ParameterDirection.Input);
                    param.Add("SGST", model.SGST, DbType.Decimal, ParameterDirection.Input);
                    param.Add("CGST", model.CGST, DbType.Decimal, ParameterDirection.Input);
                    param.Add("CurrencyOfLedger", model.CurrencyOfLedger, DbType.String, ParameterDirection.Input);
                    param.Add("OpeningBalance", model.OpeningBalance, DbType.String, ParameterDirection.Input);
                    param.Add("ACHolderName", model.ACHolderName, DbType.String, ParameterDirection.Input);
                    param.Add("ACName", model.ACName, DbType.String, ParameterDirection.Input);
                    param.Add("IFSCCode", model.IFSCCode, DbType.String, ParameterDirection.Input);
                    param.Add("BankName", model.BankName, DbType.String, ParameterDirection.Input);
                    param.Add("Branch", model.Branch, DbType.String, ParameterDirection.Input);
                    param.Add("BSRCode", model.BSRCode, DbType.String, ParameterDirection.Input);
                    param.Add("IsChequeBooks", model.IsChequeBooks, DbType.Boolean, ParameterDirection.Input);
                    param.Add("IsChequePrintingConfg", model.IsChequePrintingConfg, DbType.Boolean, ParameterDirection.Input);
                    param.Add("Country", model.Country, DbType.String, ParameterDirection.Input);
                    param.Add("PinCode", model.PinCode, DbType.String, ParameterDirection.Input);
                    param.Add("FAXNo", model.FAXNo, DbType.String, ParameterDirection.Input);
                    param.Add("Website", model.Website, DbType.String, ParameterDirection.Input);
                    param.Add("CCEmail", model.CCEmail, DbType.String, ParameterDirection.Input);
                    param.Add("SetServiceTaxDetails", model.SetServiceTaxDetails, DbType.Boolean, ParameterDirection.Input);
                    param.Add("TypesOfDuty", model.TypesOfDuty, DbType.String, ParameterDirection.Input);
                    param.Add("TaxType", model.TaxType, DbType.String, ParameterDirection.Input);
                    param.Add("ODLimit", model.ODLimit, DbType.Decimal, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<int>("InsertUpdateAccountParty_sp", param, commandType: CommandType.StoredProcedure);
                    return lastInsertedId.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<AccountPartyMasterDto>> GetAccountPartyBySunderyOtherCharge()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<AccountPartyMasterDto>("GetAccountPartyBySunderyOtherCharge_sp", commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }
    }
}
