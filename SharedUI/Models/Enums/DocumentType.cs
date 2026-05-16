using System;

namespace SharedUI.Models.Enums
{
    /* USE camelCase FOR ENUMS as per project requirements */
    public enum DocumentType
    {
        // CONFIGURATION
        company = 1,
        user = 2,
        rightSetting = 3,
        userRight = 4,
        branch = 5,

        // COMPANY SETUP
        accountChartOfAccount = 6,
        department = 7,

        // INVENTORY
        saleUnit = 8,
        section = 9,
        category = 10,
        subCategory = 11,
        brand = 12,
        product = 13,
        productATI = 14,

        // SALE
        customer = 15,
        invoice = 16,
        invoiceProduct = 17,
        invoiceProductCustomerOB = 18,
        customerLedgerRecord = 19,
        paymentReceipt = 20,

        // PURCHASE
        supplier = 21,
        bill = 22,
        billProduct = 23,
        billProductSupplierOB = 24,
        supplierLedgerRecord = 25


    }
    public enum StockAdjustmentType
    {
        OPENING_BALANCE = 1,
        STOCK_IN_SURPLUS = 2,
        STOCK_OUT_DAMAGED = 3,
        THEFT_OR_LOSS = 4,
        DATA_ENTRY_CORRECTION = 5
    }
}