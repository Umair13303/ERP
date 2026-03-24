using System;
using System.Collections.Generic;

namespace OrganisationSetup.Models.DAL;

public partial class DCSChartOfAccount
{
    public int Id { get; set; }

    public int? InventoryAccountId { get; set; }

    public int? SaleRevenueAccountId { get; set; }

    public int? CostOfSaleAccountId { get; set; }

    public int? Status { get; set; }

    public int? CompanyId { get; set; }
}
