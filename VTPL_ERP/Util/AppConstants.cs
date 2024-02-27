using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTPL_ERP.Util
{
    public static class AppConstants
    {
        public static class SessionKey
        {
            public const string CURRENT_USER = "CURRENT_USER";
            public const string CURRENT_USER_RIGHTS = "CURRENT_USER_RIGHTS";
        }
        public static class Messages
        {
            public const string Not_Authorized = "You are not authorized user for this action.";
            
        }
        public static class Tran
        {
            public const string SalesInvoice = "SI";
            public const string SalesReturn = "SR";
            public const string PurchaseInvoice = "PI";
            public const string PurchaseReturn = "PR";
            public const string Opening = "OP";
            public const string StockAdjastment = "SA";
            public const string StockScrap = "SS";
            public const string StockTransfer = "ST";
            public const string Credit = "CR";
            public const string Debit = "DR";
        }
        public enum AppPages
        {
            AccountParty_Master = 1,
            Branch_Master = 2,
            Courier_Master = 3,
            Employee_Master = 4,
            InventoryBrand_Master = 5,
            InventoryCategory_Master = 6,
            Inventory_Master = 7,
            OtherCharges_Master = 8,
            Problem_Master = 9,
            RoleAction_Master = 10,
            Roles_Master = 11,
            Unit_Master = 12,
            PurchaseQuotation = 10002,
            PurchaseEntry = 20002,
            Company = 30002,
            PurchaseReturnEntry = 40002,
            PhoneCallsEntry = 40003,
            CompanyBankAccount = 50003,
            SalesOrder = 50004,
            SalesEntry = 50005,
            SalesReturnEntry = 50006,
            PartyPaymentDebitEntry = 50007,
            Inward = 50008,
            Outward = 50009,
            ContraEntry = 50010,
            StockTransfer = 50011
        }
        public enum RoleActions
        {
            InsertUpdate = 1,
            //Edit =2,
            Delete = 3,
            View = 4,
            Print = 5,
            Export = 6,
            IsPageVisible = 7
        }

    }
}
