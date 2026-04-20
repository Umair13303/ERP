using System;

namespace SharedUI.Models.ViewModels
{
    public class SOCustomerListItem
    {
        public int Id { get; set; }
        public string? Description { get; set; }
    }

    public class CustomerLedgerRow
    {
        public DateTime? Date { get; set; }
        public string? Description { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
    }
}

