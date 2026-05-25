using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUI.Models.Enums
{
    public enum PaymentStatus
    {
        underProcess=1,
        verified=2,
        declined=3,
    }
    
    public enum PaymentType
    {
        InvoiceWise=1,
        BillWise=2,
        CustomerAccount=3,
        SupplierAccount=4
    }

}
