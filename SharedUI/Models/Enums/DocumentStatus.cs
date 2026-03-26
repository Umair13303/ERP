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
    public class PostingStatus
    {
        public enum InvoiceStatus
        {
            unPaid = 1,
            partialPaid = 2,
            paid = 3,
            overDue=4,
            cancelled=5
        }
        public enum LedgerStatus
        {
            unreconciled = 6, // Entry exists but hasn't been matched with bank/statement
            reconciled = 7,   // Matched and verified against external records
            adjusted = 8,     // Original entry modified by a subsequent adjustment
            cleared = 9       // Final state where the transaction is fully settled
        }
        public enum VoucherStatus
        {
            pending = 10,  // Draft/Entered by clerk
            verified = 11, // Checked by a supervisor
            posted = 12,   // Committed to the General Ledger (Locked)
            rejected = 13, // Sent back for correction during verification
            voided = 14    // Marked as invalid/deleted after being posted
        }


    }

}
