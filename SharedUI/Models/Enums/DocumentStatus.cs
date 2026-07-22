using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUI.Models.Enums
{
    public enum DocumentStatus
    {
        active = 1,
        inactive = 2,
        deleted = 3,
        expired = 4,
    }
    public enum InvoiceStatus
    {
        unPaid = 1,
        partialPaid = 2,
        paid = 3,
        overDue = 4,
        cancelled = 5
    }
    public enum BillStatus
    {
        unPaid = 1,
        partialPaid = 2,
        paid = 3,
        overDue = 4,
        cancelled = 5
    }
    public enum ReconcileStatus
    {
        unreconciled = 1,
        reconciled = 2,  
        adjusted = 3,    
        cleared = 4       
    }
    public enum PostingStatus
    {
        pending = 1, 
        verified = 2,
        posted = 3,  
        rejected = 4,
        voided = 5   
    }
    public enum PaymentStatus
    {
        underProcess = 1,
        verified = 2,
        declined = 3,
    }
    public enum PaymentType
    {
        InvoiceWise = 1,
        BillWise = 2,
        CustomerAccount = 3,
        SupplierAccount = 4
    }
    public enum Default
    {
        productId = 0,
        productCombinationId = 0,
        tierTypeId = 0,
        productPriceLogId = 0,
        adjustmentStatus = (int)AdjustmentStatus.approved,
        invoiceStatus = (int)InvoiceStatus.unPaid,
        reconcileStatus = (int)ReconcileStatus.reconciled,
        paymentStatus = (int)PaymentStatus.verified,
        billStatus = (int)BillStatus.unPaid,
    }

}
