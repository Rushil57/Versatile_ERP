using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class AccountPartyMasterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IsSundery { get; set; }
        //public string CurrencyOfLedger { get; set; }
        public bool IsMaintainBalanceByBill { get; set; }
        public int DefaultCreditPeriod { get; set; }
        public decimal CreditLimit { get; set; }
        //public bool IsInventoryValueEffected { get; set; }
        public bool IsActivateInterestCalculation { get; set; }
        public DateTime OpeningDate { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime? Modified_Date { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public decimal Interest { get; set; }
        public int AccountType { get; set; }
        public string SunderyType { get; set; }
        public string Landline { get; set; }
        public string Email2 { get; set; }
        public string Contact { get; set; }
        public string ContactPerson1 { get; set; }
        public string ContactPerson2 { get; set; }
        public string MainAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryAddressContactPersonName { get; set; }
        public string DeliveryAddressContactPersonNo { get; set; }
        public string InterestPeriod { get; set; }
        public string SalesPersonId { get; set; }
        public bool PDC { get; set; }
        public string GSTDetails { get; set; }
        public string GSTNumber { get; set; }
        public string PANNumber { get; set; }
        public string SalesPersonName { get; set; }
        public string State { get; set; }
        public string HSNCode { get; set; }
        public string TypesOfGood { get; set; }
        public string Taxability { get; set; }
        public decimal IGST { get; set; }
        public decimal SGST { get; set; }
        public decimal CGST { get; set; }
        public string CurrencyOfLedger { get; set; }
        public string OpeningBalance { get; set; }
        public string ACHolderName { get; set; }
        public string ACName { get; set; }
        public string IFSCCode { get; set; }
        public string BankName { get; set; }
        public string Branch { get; set; }
        public string BSRCode { get; set; }
        public bool IsChequeBooks { get; set; }
        public bool IsChequePrintingConfg { get; set; }
        public string Country { get; set; }
        public string PinCode { get; set; }
        public string FAXNo { get; set; }
        public string Website { get; set; }
        public string CCEmail { get; set; }
        public bool SetServiceTaxDetails { get; set; }
        public string TypesOfDuty { get; set; }
        public string TaxType { get; set; }
        public decimal ODLimit { get; set; }
    }
}











