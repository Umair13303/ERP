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

    public virtual DbSet<AFBill> AFBill { get; set; }

    public virtual DbSet<AFBillPPI> AFBillPPI { get; set; }

    public virtual DbSet<AFChartOfAccount> AFChartOfAccount { get; set; }

    public virtual DbSet<AFCustomerLedger> AFCustomerLedger { get; set; }

    public virtual DbSet<AFInventoryLedger> AFInventoryLedger { get; set; }

    public virtual DbSet<AFInvoice> AFInvoice { get; set; }

    public virtual DbSet<AFInvoicePPI> AFInvoicePPI { get; set; }

    public virtual DbSet<AFInvoiceReceipt> AFInvoiceReceipt { get; set; }

    public virtual DbSet<AFJournalVoucher> AFJournalVoucher { get; set; }

    public virtual DbSet<AFProductPriceLog> AFProductPriceLog { get; set; }

    public virtual DbSet<AFSupplierLedger> AFSupplierLedger { get; set; }

    public virtual DbSet<AppForm> AppForm { get; set; }

    public virtual DbSet<AppUser> AppUser { get; set; }

    public virtual DbSet<Branch> Branch { get; set; }

    public virtual DbSet<CSDepartment> CSDepartment { get; set; }

    public virtual DbSet<Company> Company { get; set; }

    public virtual DbSet<IAdjustment> IAdjustment { get; set; }

    public virtual DbSet<IAdjustmentPPQD> IAdjustmentPPQD { get; set; }

    public virtual DbSet<IBrand> IBrand { get; set; }

    public virtual DbSet<ICategory> ICategory { get; set; }

    public virtual DbSet<IProduct> IProduct { get; set; }

    public virtual DbSet<IProductATI> IProductATI { get; set; }

    public virtual DbSet<ISection> ISection { get; set; }

    public virtual DbSet<ISubCategory> ISubCategory { get; set; }

    public virtual DbSet<PSupplier> PSupplier { get; set; }

    public virtual DbSet<SOCustomer> SOCustomer { get; set; }

    public virtual DbSet<UserRight> UserRight { get; set; }

    public virtual DbSet<confClientProductSetting> confClientProductSetting { get; set; }

    public virtual DbSet<confClientSetting> confClientSetting { get; set; }

    public virtual DbSet<osvChartOfAccount> osvChartOfAccount { get; set; }

    public virtual DbSet<osvProductCombination> osvProductCombination { get; set; }

    public virtual DbSet<vAccountCatagory> vAccountCatagory { get; set; }

    public virtual DbSet<vAccountType> vAccountType { get; set; }

    public virtual DbSet<vAttribute> vAttribute { get; set; }

    public virtual DbSet<vCity> vCity { get; set; }

    public virtual DbSet<vCostingMode> vCostingMode { get; set; }

    public virtual DbSet<vCountry> vCountry { get; set; }

    public virtual DbSet<vFinancialStatement> vFinancialStatement { get; set; }

    public virtual DbSet<vHSCode> vHSCode { get; set; }

    public virtual DbSet<vInventoryAdjustmentType> vInventoryAdjustmentType { get; set; }

    public virtual DbSet<vItemType> vItemType { get; set; }

    public virtual DbSet<vOrganisationType> vOrganisationType { get; set; }

    public virtual DbSet<vPaymentMethod> vPaymentMethod { get; set; }

    public virtual DbSet<vProductType> vProductType { get; set; }

    public virtual DbSet<vRight> vRight { get; set; }

    public virtual DbSet<vRole> vRole { get; set; }

    public virtual DbSet<vSaleTaxType> vSaleTaxType { get; set; }

    public virtual DbSet<vTierType> vTierType { get; set; }

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

        modelBuilder.Entity<AFBill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AFBill__3214EC07A9ABD5AD");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DueAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<AFBillPPI>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AFBillPP__3214EC0769720BF3");

            entity.Property(e => e.ActualAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ChargedAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 2)");
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

        modelBuilder.Entity<AFInventoryLedger>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ILedger__3214EC07240B5AAA");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Credit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Debit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.QuantityIn).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.QuantityOut).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status).HasDefaultValue(true);
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

        modelBuilder.Entity<AFInvoiceReceipt>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ReceiptAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
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

        modelBuilder.Entity<AFProductPriceLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AFProduc__3214EC07ACC0F658");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DefaultSalePrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MinimumSalePrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<AFSupplierLedger>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AFSuppli__3214EC0779569B61");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Credit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Debit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<AppForm>(entity =>
        {
            entity.HasKey(e => e.FormId).HasName("PK__AppForm__FB05B7DDEF947BA3");

            entity.HasIndex(e => e.FormCode, "UQ__AppForm__F69A6BF716A3223F").IsUnique();

            entity.Property(e => e.FormCode).HasMaxLength(50);
            entity.Property(e => e.FormName).HasMaxLength(200);
            entity.Property(e => e.ModuleName).HasMaxLength(100);
        });

        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__AppUser__1788CC4CF0BF2759");

            entity.HasIndex(e => e.UserName, "UQ__AppUser__C9F28456886B991E").IsUnique();

            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.FullName).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsSuperAdmin).HasDefaultValue(false);
            entity.Property(e => e.PasswordHash).HasMaxLength(500);
            entity.Property(e => e.UserName).HasMaxLength(100);

            entity.HasOne(d => d.Branch).WithMany(p => p.AppUser)
                .HasForeignKey(d => d.BranchId)
                .HasConstraintName("FK__AppUser__BranchI__72B0FDB1");

            entity.HasOne(d => d.Company).WithMany(p => p.AppUser)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AppUser__Company__71BCD978");
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.BranchId).HasName("PK__Branch__A1682FC5C0039C3D");

            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.BranchName).HasMaxLength(200);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Phone).HasMaxLength(50);

            entity.HasOne(d => d.Company).WithMany(p => p.Branch)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Branch__CompanyI__6C040022");
        });

        modelBuilder.Entity<CSDepartment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ACDepartment");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__Company__2D971CAC4A4D5488");

            entity.Property(e => e.CompanyName).HasMaxLength(200);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<IAdjustment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__IAdjustm__3214EC073D21BC49");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<IAdjustmentPPQD>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__IAdjustm__3214EC0733D4010F");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.QuantityIn).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.QuantityOut).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.UnitPurchasePrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UnitSalePrice).HasColumnType("decimal(18, 2)");
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

        modelBuilder.Entity<PSupplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ISupplie__3214EC0750900A43");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.OpeningBalance).HasColumnType("decimal(18, 2)");
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

        modelBuilder.Entity<UserRight>(entity =>
        {
            entity.HasKey(e => e.UserRightId).HasName("PK__UserRigh__956097A2B7FE4983");

            entity.HasIndex(e => new { e.UserId, e.FormId }, "UQ_UserForm").IsUnique();

            entity.Property(e => e.CanAdd).HasDefaultValue(false);
            entity.Property(e => e.CanDelete).HasDefaultValue(false);
            entity.Property(e => e.CanEdit).HasDefaultValue(false);
            entity.Property(e => e.CanView).HasDefaultValue(false);

            entity.HasOne(d => d.Form).WithMany(p => p.UserRight)
                .HasForeignKey(d => d.FormId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserRight__FormI__7D2E8C24");

            entity.HasOne(d => d.User).WithMany(p => p.UserRight)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserRight__UserI__7C3A67EB");
        });

        modelBuilder.Entity<confClientProductSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__confClie__3214EC07BF3A289D");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.EnableATI).HasDefaultValue(false);
            entity.Property(e => e.EnableAttribute).HasDefaultValue(false);
            entity.Property(e => e.EnableFavorite).HasDefaultValue(false);
            entity.Property(e => e.EnableMachineNumber).HasDefaultValue(false);
            entity.Property(e => e.EnableSKU).HasDefaultValue(false);
            entity.Property(e => e.EnableTaxSetting).HasDefaultValue(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<confClientSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__confClie__3214EC07CF3B99B2");
        });

        modelBuilder.Entity<osvChartOfAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CSChartOfAccount");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<osvProductCombination>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__osvProdu__3214EC07D19CECAB");
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

        modelBuilder.Entity<vCostingMode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vCosting__3214EC07CE0C2E53");

            entity.Property(e => e.Status).HasDefaultValue(true);
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

        modelBuilder.Entity<vInventoryAdjustmentType>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.IsAutoPriceUpdate).HasDefaultValue(false);
            entity.Property(e => e.Status).HasDefaultValue(true);
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

        modelBuilder.Entity<vProductType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vProduct__3214EC0744AC60CF");

            entity.Property(e => e.Status).HasDefaultValue(true);
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

        modelBuilder.Entity<vTierType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vTierTyp__3214EC0780D8C274");

            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        modelBuilder.Entity<vUnit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vUnit__3214EC07FEFCA41C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
