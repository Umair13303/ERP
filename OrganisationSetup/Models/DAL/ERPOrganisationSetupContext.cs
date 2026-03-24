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

    public virtual DbSet<vOrganizationType> vOrganizationType { get; set; }

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
            entity.HasKey(e => e.Id).HasName("PK__AFCustom__3214EC07175DF11F");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Credit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Debit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<AFInvoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AFInvoic__3214EC070FE36FF7");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
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
            entity.HasKey(e => e.Id).HasName("PK__AFJourna__3214EC07FD9C6573");

            entity.Property(e => e.Credit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Debit).HasColumnType("decimal(18, 2)");
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
            entity
                .HasNoKey()
                .ToView("vAccountCatagory");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<vAccountType>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vAccountType");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<vAttribute>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vAttribute");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<vCity>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vCity");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<vCountry>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vCountry");
        });

        modelBuilder.Entity<vFinancialStatement>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vFinancialStatement");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<vHSCode>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vHSCode");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<vItemType>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vItemType");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<vOrganisationType>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vOrganisationType");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<vOrganizationType>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vOrganizationType");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<vRight>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vRight");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<vRole>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vRole");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<vSaleTaxType>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vSaleTaxType");

            entity.Property(e => e.AdditionalRate).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DefaultRate).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<vUnit>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vUnit");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
