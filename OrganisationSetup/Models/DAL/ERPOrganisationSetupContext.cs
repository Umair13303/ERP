using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OrganisationSetup.Models.DAL;

public partial class ERPOrganisationSetupContext : DbContext
{
    public ERPOrganisationSetupContext(DbContextOptions<ERPOrganisationSetupContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ACBranch> ACBranch { get; set; }

    public virtual DbSet<ACCompany> ACCompany { get; set; }

    public virtual DbSet<ACSaleUnit> ACSaleUnit { get; set; }

    public virtual DbSet<ACUser> ACUser { get; set; }

    public virtual DbSet<AFChartOfAccount> AFChartOfAccount { get; set; }

    public virtual DbSet<AFCustomerLedger> AFCustomerLedger { get; set; }

    public virtual DbSet<AFInvoice> AFInvoice { get; set; }

    public virtual DbSet<AFInvoicePPI> AFInvoicePPI { get; set; }

    public virtual DbSet<AFJournalVoucher> AFJournalVoucher { get; set; }

    public virtual DbSet<AFPaymentReceipt> AFPaymentReceipt { get; set; }

    public virtual DbSet<CSDepartment> CSDepartment { get; set; }

    public virtual DbSet<IBrand> IBrand { get; set; }

    public virtual DbSet<ICategory> ICategory { get; set; }

    public virtual DbSet<IProduct> IProduct { get; set; }

    public virtual DbSet<IProductATI> IProductATI { get; set; }

    public virtual DbSet<ISection> ISection { get; set; }

    public virtual DbSet<ISubCategory> ISubCategory { get; set; }

    public virtual DbSet<SOCustomer> SOCustomer { get; set; }

    public virtual DbSet<osvChartOfAccount> osvChartOfAccount { get; set; }

    public virtual DbSet<vAccountCatagory> vAccountCatagory { get; set; }

    public virtual DbSet<vAccountType> vAccountType { get; set; }

    public virtual DbSet<vAttribute> vAttribute { get; set; }

    public virtual DbSet<vCity> vCity { get; set; }

    public virtual DbSet<vCountry> vCountry { get; set; }

    public virtual DbSet<vFinancialStatement> vFinancialStatement { get; set; }

    public virtual DbSet<vHSCode> vHSCode { get; set; }

    public virtual DbSet<vItemType> vItemType { get; set; }

    public virtual DbSet<vOrganisationType> vOrganisationType { get; set; }

    public virtual DbSet<vPaymentMethod> vPaymentMethod { get; set; }

    public virtual DbSet<vRight> vRight { get; set; }

    public virtual DbSet<vRole> vRole { get; set; }

    public virtual DbSet<vSaleTaxType> vSaleTaxType { get; set; }

    public virtual DbSet<vUnit> vUnit { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ACBranch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ACBranch_Id");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<ACCompany>(entity =>
        {
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<ACSaleUnit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ACSaleUn__3214EC07C84D4C79");

            entity.Property(e => e.CreatedBy).HasDefaultValue(1);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.GuID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.PackingQuantity)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<ACUser>(entity =>
        {
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<AFChartOfAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_AFChartOfAccount_Id");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<AFCustomerLedger>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_AFCustomerLedger_Id");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Credit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Debit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<AFInvoice>(entity =>
        {
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DueAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<AFInvoicePPI>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AFInvoic__3214EC07A5281937");

            entity.Property(e => e.ActualAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ChargedAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<AFJournalVoucher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_AFJournalVoucher_Id");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Credit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Debit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<AFPaymentReceipt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AFPaymen__3214EC0795F8B3E6");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ReceiptAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<CSDepartment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ACDepartment");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<IBrand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_IBrand_Id");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<ICategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ICategor__3214EC07ADE2DB2A");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<IProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__IProduct__3214EC074BA48951");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.CriticalLimit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<IProductATI>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__IProduct__3214EC076FEE2B1F");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<ISection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ACSection");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<ISubCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ISubCate__3214EC0768BF3384");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<SOCustomer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SOCustom__3214EC079C9ACE51");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.OpeningBalance)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<osvChartOfAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CSChartOfAccount");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<vAccountCatagory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vAccount__3214EC07B345D331");
        });

        modelBuilder.Entity<vAccountType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vAccount__3214EC07B22329BB");
        });

        modelBuilder.Entity<vAttribute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vAttribu__3214EC07F532D3FB");
        });

        modelBuilder.Entity<vCity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vCity__3214EC079B843DE9");
        });

        modelBuilder.Entity<vCountry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vCountry__3214EC076392BB8C");
        });

        modelBuilder.Entity<vFinancialStatement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vFinanci__3214EC07A17F5B2D");
        });

        modelBuilder.Entity<vHSCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vHSCode__3214EC0703BE51B4");
        });

        modelBuilder.Entity<vItemType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vItemTyp__3214EC07E26166D9");
        });

        modelBuilder.Entity<vOrganisationType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vOrganis__3214EC07E64209F4");
        });

        modelBuilder.Entity<vPaymentMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vPayment__3214EC0799BB60D9");
        });

        modelBuilder.Entity<vRight>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vRight__3214EC071C1E2EA9");
        });

        modelBuilder.Entity<vRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vRole__3214EC076B95D2D9");
        });

        modelBuilder.Entity<vSaleTaxType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vSaleTax__3214EC0713061C25");

            entity.Property(e => e.AdditionalRate).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DefaultRate).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<vUnit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vUnit__3214EC07FEFCA41C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
