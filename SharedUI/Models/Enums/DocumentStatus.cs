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
        unpaid=5,
        partiallyPaid=6,
        completePaid=7,
        pendingVoucher=8,
        verifiedVoucher=9,
        postedVoucher=10
    }

}
