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

    public virtual DbSet<AFInvoiceProduct> AFInvoiceProduct { get; set; }

    public virtual DbSet<AFJournalVoucher> AFJournalVoucher { get; set; }

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

    public virtual DbSet<vRight> vRight { get; set; }

    public virtual DbSet<vRole> vRole { get; set; }

    public virtual DbSet<vSaleTaxType> vSaleTaxType { get; set; }

    public virtual DbSet<vUnit> vUnit { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ACBranch>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<ACCompany>(entity =>
        {
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<ACSaleUnit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ACSaleUn__3214EC0763996922");

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
            entity.HasNoKey();

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<AFCustomerLedger>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Credit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Debit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<AFInvoice>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DueAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<AFInvoiceProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AFInvoic__3214EC07F192FFB7");

            entity.Property(e => e.ActualAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ChargedAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<AFJournalVoucher>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Credit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Debit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
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
            entity.HasNoKey();

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<ICategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ICategor__3214EC07D6924099");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<IProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__IProduct__3214EC0780302123");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.CriticalLimit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<IProductATI>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__IProduct__3214EC07FF31483B");

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
            entity.HasKey(e => e.Id).HasName("PK__ISubCate__3214EC0703940CCF");

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
            entity.HasKey(e => e.Id).HasName("PK__vAccount__3214EC078E23DC11");
        });

        modelBuilder.Entity<vAccountType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vAccount__3214EC07572BD4C1");
        });

        modelBuilder.Entity<vAttribute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vAttribu__3214EC07A4A8A04C");
        });

        modelBuilder.Entity<vCity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vCity__3214EC0796BB681D");
        });

        modelBuilder.Entity<vCountry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vCountry__3214EC0776834BC7");
        });

        modelBuilder.Entity<vFinancialStatement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vFinanci__3214EC07453601A1");
        });

        modelBuilder.Entity<vHSCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vHSCode__3214EC0776531CCF");
        });

        modelBuilder.Entity<vItemType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vItemTyp__3214EC0757839F2A");
        });

        modelBuilder.Entity<vOrganisationType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vOrganis__3214EC074F2D5E25");
        });

        modelBuilder.Entity<vRight>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vRight__3214EC07A6D604E0");
        });

        modelBuilder.Entity<vRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vRole__3214EC07E09D9ECD");
        });

        modelBuilder.Entity<vSaleTaxType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vSaleTax__3214EC07F21D3FAF");

            entity.Property(e => e.AdditionalRate).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DefaultRate).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<vUnit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vUnit__3214EC07AEADDD23");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
