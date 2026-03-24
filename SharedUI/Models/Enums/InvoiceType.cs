using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUI.Models.Enums
{
    public enum InvoiceType
    {
        CustomerOpeningBalanceInvoice=1,
        CustomerDirectSaleInvoice=2
    }
    public enum InvoiceItemType
    {
        OpeningBalance=1,
        Product=2,
        Service=3
    }
}
