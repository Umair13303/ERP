using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUI.Models.Enums
{
    public enum AccountCategory
    {
        PROPERTY_PLANT_AND_EQUIPMENTS = 1,
        LONG_TERM_ASSETS = 2,
        ACCUMULATED_DEPRECIATION = 3,
        CURRENT_ASSETS = 4,
        OTHER_CURRENT_ASSETS = 5,
        LONG_TERM_LIABILITIES = 6,
        CURRENT_LIABILITIES = 7,
        OTHER_CURRENT_LIABILITIES = 8,
        EQUITY_DOES_NOT_CLOSE = 9,
        EQUITY_GETS_CLOSED = 10,
        EQUITY_RETAINED_EARNINGS = 11,
        SALES__REVENUES = 12,
        OTHER_INCOME = 13,
        COST_OF_SALES = 14,
        EXPENSES_DIRECT = 15,
        EXPENSES_ADMIN = 16,
        EXPENSES_MARKETING = 17,
        EXPENSES_FINANCIAL = 18,
        EXPENSES_OTHER_INDIRECT = 19,
        ACCOUNTS_RECEIVABLE = 20,
        BANK_AND_CASH = 21,
        ACCOUNTS_PAYABLE = 22,
        INVENTORY = 23,
    }
    public enum FinancialStatement
    {
        BALANCE_SHEET=1,
        INCOME_STATEMENT=2
    }
}
