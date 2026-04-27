using Azure;
using Dapper;
using Humanizer;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using SharedUI.Models.SQLParameters;
using SharedUI.Models.TVP;
using SharedUI.Models.ViewModels;
using System;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace OrganisationSetup.Models.DAL.StoredProcedure
{
    public interface IOSDataLayer
    {
        #region UPSERT OPERATION
        Task<int?> UpsertInto_ACCompany(string? operationType, Guid? guId, string? description, int? countryId, int? cityId, string? contact, string? email, string? address, string? website, string? logo, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans);
        Task<int?> UpsertInto_ACBranch(string? operationType, Guid? guId, string? description, int? organisationTypeId, int? countryId, int? cityId, string? contact, string? email, string? address, string? ntnNumber, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans);
        Task<int?> UpsertInto_ACUser(string? operationType, Guid? guId, string? description, string? password, string? contact, string? email, int? employeeId, int? roleId, string? allowedBranchIds, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans);
        Task<int?> UpsertInto_CSDepartment(string? operationType, Guid? guId, string? description, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans);
        Task<int?> UpsertInto_ISection(string? operationType, Guid? guId, string? description, int? departmentId, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans);
        Task<int?> UpsertInto_ICategory(string? operationType, Guid? guId, string? description, int? departmentId, int? sectionId, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans);
        Task<int?> UpsertInto_ISubCategory(string? operationType, Guid? guId, string? description, int? departmentId, int? sectionId, int? categoryId, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans);
        Task<int?> UpsertInto_IBrand(string? operationType, Guid? guId, string? description, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans);
        Task<(int? response, int? insertedId)> UpsertInto_IProduct(string? operationType, Guid? guId, string? description, string? machineNumber, string? sku, string? additionalDetail, string? attributeIds, int? brandId, bool? isFavorite, bool? isSaleTaxExclusive, int? departmentId, int? sectionId, int? categoryId, int? subCategoryId, decimal? criticalLimit, int? saleUnitId, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans);
        Task<int?> UpsertInto_IProductATI(string? operationType, Guid? guId, int? productId, int? inventoryAccountId, int? saleRevenueAccountId, int? costOfSaleAccountId, int? itemTypeId, int? hsCodeId, int? saleTaxTypeId, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, SqlConnection con, SqlTransaction trans);
        Task<(int? response, int? insertedId)> UpsertInto_SOCustomer(string? operationType, Guid? guId, string? description, string? contact, string? email, string? cnicNumber, string? address, string? additionalDetail, int? receivableAccountId, decimal? openingBalance, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans);
        Task<(int? response, int? insertedId)> UpsertInto_AFChartOfAccount(string? operationType, Guid? guId, string? description, int? accountCategoryId, int? financialStatementId, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans);
        Task<(int? response, int? insertedId, string? documentCode, decimal? totalInvoiceAmount)> UpsertInto_AFInvoice(string? operationType, Guid? guId, int? locationId, DateTime? transactionDate, int? customerId, string? description, string? fbrStamp, decimal dueAmount, int? invoiceTypeId, int? invoiceStatus, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, List<AFInvoiceProductPricing_TVP> invoicePPI, SqlConnection con, SqlTransaction trans);
        Task<(int? response, int? insertedId, string? documentCode)> UpsertInto_AFCustomerLedger(string? operationType, int? companyId, List<AFCustomerLedger_TVP> customerLedger, SqlConnection con, SqlTransaction trans);
        Task<int?> UpsertInto_AFJournalVoucher(string? operationType, int? companyId, List<AFJournalVoucher> journalVoucher, SqlConnection con, SqlTransaction trans);
        Task<(int? response, int? insertedId, string? documentCode)> UpsertInto_AFPaymentReceipt(string? operationType, Guid? guId, int? locationId, DateTime? transactionDate, int? customerId, int? invoiceId, string? description, int? paymentMethodId, string? reference, decimal? receiptAmount, int? paymentStatus, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans);
        #endregion

        #region RETRIEVE OPERATION
        Task<IReadOnlyList<DTObject.Invoice_List>> ret_Invoices_ByCustomer(Guid? guId, int? customerId, int?[] documentStatusIds, int?[] invoiceStatusIds,string connStr);
        #endregion

    }
    public class OSDataLayerRepository : IOSDataLayer
    {
        #region UPSERT OPERATION SP
        public async Task<int?> UpsertInto_ACCompany(string? operationType, Guid? guId, string? description, int? countryId, int? cityId, string? contact, string? email, string? address, string? website, string? logo, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans)
        {
            using var cmd = new SqlCommand("ACCompany_Upsert", con, trans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DB_OperationType", (object)operationType! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GuID", (object)guId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Description", (object)description! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CountryId", (object)countryId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CityId", (object)cityId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Contact", (object)contact! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Email", (object)email! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Address", (object)address! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Website", (object)website! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Logo", (object)logo! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedOn", (object)createdOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", (object?)createdBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedOn", (object?)updatedOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@UpdatedBy", (object?)updatedBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentType", (object?)documentType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentStatus", (object?)documentStatus ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", true);
            cmd.Parameters.AddWithValue("@BranchId", (object?)branchId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CompanyId", (object?)companyId ?? DBNull.Value);
            var responseParam = new SqlParameter("@Response", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(responseParam);
            await cmd.ExecuteNonQueryAsync();
            return responseParam.Value == DBNull.Value ? null : (int?)responseParam.Value;
        }
        public async Task<int?> UpsertInto_ACBranch(string? operationType, Guid? guId, string? description, int? organisationTypeId, int? countryId, int? cityId, string? contact, string? email, string? address, string? ntnNumber, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans)
        {
            using var cmd = new SqlCommand("ACBranch_Upsert", con, trans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DB_OperationType", (object)operationType! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GuID", (object)guId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Description", (object)description! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@OrganisationTypeId", (object)organisationTypeId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CountryId", (object)countryId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CityId", (object)cityId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Contact", (object)contact! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Email", (object)email! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Address", (object)address! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@NTNNumber", (object)ntnNumber! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedOn", (object)createdOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", (object?)createdBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedOn", (object?)updatedOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@UpdatedBy", (object?)updatedBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentType", (object?)documentType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentStatus", (object?)documentStatus ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", true);
            cmd.Parameters.AddWithValue("@BranchId", (object?)branchId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CompanyId", (object?)companyId ?? DBNull.Value);
            var responseParam = new SqlParameter("@Response", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(responseParam);
            await cmd.ExecuteNonQueryAsync();
            return responseParam.Value == DBNull.Value ? null : (int?)responseParam.Value;
        }
        public async Task<int?> UpsertInto_ACUser(string? operationType, Guid? guId, string? description, string? password, string? contact, string? email, int? employeeId, int? roleId, string? allowedBranchIds, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans)
        {
            using var cmd = new SqlCommand("ACUser_Upsert", con, trans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DB_OperationType", (object)operationType! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GuID", (object)guId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Description", (object)description! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Password", (object)password! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Contact", (object)contact! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Email", (object)email! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@EmployeeId", (object?)employeeId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@RoleId", (object?)roleId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@AllowedBranchIds", (object?)allowedBranchIds ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedOn", (object)createdOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", (object?)createdBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedOn", (object?)updatedOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@UpdatedBy", (object?)updatedBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentType", (object?)documentType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentStatus", (object?)documentStatus ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", true);
            cmd.Parameters.AddWithValue("@BranchId", (object?)branchId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CompanyId", (object?)companyId ?? DBNull.Value);
            var responseParam = new SqlParameter("@Response", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(responseParam);

            await cmd.ExecuteNonQueryAsync();
            return responseParam.Value == DBNull.Value ? null : (int?)responseParam.Value;
        }
        public async Task<int?> UpsertInto_CSDepartment(string? operationType, Guid? guId, string? description, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans)
        {
            using var cmd = new SqlCommand("CSDepartment_Upsert", con, trans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DB_OperationType", (object)operationType! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GuID", (object)guId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Description", (object)description! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedOn", (object)createdOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", (object?)createdBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedOn", (object?)updatedOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@UpdatedBy", (object?)updatedBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentType", (object?)documentType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentStatus", (object?)documentStatus ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", true);
            cmd.Parameters.AddWithValue("@BranchId", (object?)branchId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CompanyId", (object?)companyId ?? DBNull.Value);
            var responseParam = new SqlParameter("@Response", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(responseParam);

            await cmd.ExecuteNonQueryAsync();
            return responseParam.Value == DBNull.Value ? null : (int?)responseParam.Value;
        }
        public async Task<int?> UpsertInto_ISection(string? operationType, Guid? guId, string? description, int? departmentId, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans)
        {
            using var cmd = new SqlCommand("ISection_Upsert", con, trans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DB_OperationType", (object)operationType! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GuID", (object)guId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Description", (object)description! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DepartmentId", (object)departmentId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedOn", (object)createdOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", (object?)createdBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedOn", (object?)updatedOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@UpdatedBy", (object?)updatedBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentType", (object?)documentType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentStatus", (object?)documentStatus ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", true);
            cmd.Parameters.AddWithValue("@BranchId", (object?)branchId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CompanyId", (object?)companyId ?? DBNull.Value);
            var responseParam = new SqlParameter("@Response", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(responseParam);

            await cmd.ExecuteNonQueryAsync();
            return responseParam.Value == DBNull.Value ? null : (int?)responseParam.Value;
        }
        public async Task<int?> UpsertInto_ICategory(string? operationType, Guid? guId, string? description, int? departmentId, int? sectionId, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans)
        {
            using var cmd = new SqlCommand("ICategory_Upsert", con, trans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DB_OperationType", (object)operationType! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GuID", (object)guId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Description", (object)description! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DepartmentId", (object)departmentId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SectionId", (object)sectionId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedOn", (object)createdOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", (object?)createdBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedOn", (object?)updatedOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@UpdatedBy", (object?)updatedBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentType", (object?)documentType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentStatus", (object?)documentStatus ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", true);
            cmd.Parameters.AddWithValue("@BranchId", (object?)branchId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CompanyId", (object?)companyId ?? DBNull.Value);
            var responseParam = new SqlParameter("@Response", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(responseParam);

            await cmd.ExecuteNonQueryAsync();
            return responseParam.Value == DBNull.Value ? null : (int?)responseParam.Value;
        }
        public async Task<int?> UpsertInto_ISubCategory(string? operationType, Guid? guId, string? description, int? departmentId, int? sectionId, int? categoryId, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans)
        {
            using var cmd = new SqlCommand("ISubCategory_Upsert", con, trans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DB_OperationType", (object)operationType! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GuID", (object)guId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Description", (object)description! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DepartmentId", (object)departmentId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SectionId", (object)sectionId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CategoryId", (object)categoryId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedOn", (object)createdOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", (object?)createdBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedOn", (object?)updatedOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@UpdatedBy", (object?)updatedBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentType", (object?)documentType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentStatus", (object?)documentStatus ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", true);
            cmd.Parameters.AddWithValue("@BranchId", (object?)branchId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CompanyId", (object?)companyId ?? DBNull.Value);
            var responseParam = new SqlParameter("@Response", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(responseParam);

            await cmd.ExecuteNonQueryAsync();
            return responseParam.Value == DBNull.Value ? null : (int?)responseParam.Value;
        }
        public async Task<int?> UpsertInto_IBrand(string? operationType, Guid? guId, string? description, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans)
        {
            using var cmd = new SqlCommand("IBrand_Upsert", con, trans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DB_OperationType", (object)operationType! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GuID", (object)guId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Description", (object)description! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedOn", (object)createdOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", (object?)createdBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedOn", (object?)updatedOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@UpdatedBy", (object?)updatedBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentType", (object?)documentType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentStatus", (object?)documentStatus ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", true);
            cmd.Parameters.AddWithValue("@BranchId", (object?)branchId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CompanyId", (object?)companyId ?? DBNull.Value);
            var responseParam = new SqlParameter("@Response", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(responseParam);

            await cmd.ExecuteNonQueryAsync();
            return responseParam.Value == DBNull.Value ? null : (int?)responseParam.Value;
        }
        public async Task<(int? response, int? insertedId)> UpsertInto_IProduct(string? operationType, Guid? guId, string? description, string? machineNumber, string? sku, string? additionalDetail, string? attributeIds, int? brandId, bool? isFavorite, bool? isSaleTaxExclusive, int? departmentId, int? sectionId, int? categoryId, int? subCategoryId, decimal? criticalLimit, int? saleUnitId, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans)
        {
            using var cmd = new SqlCommand("IProduct_Upsert", con, trans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DB_OperationType", (object?)operationType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GuID", (object?)guId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Description", (object?)description ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MachineNumber", (object?)machineNumber ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SKU", (object?)sku ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@AdditionalDetail", (object?)additionalDetail ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@AttributeIds", (object?)attributeIds ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@BrandId", (object?)brandId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@IsFavorite", (object?)isFavorite ?? false);
            cmd.Parameters.AddWithValue("@IsSaleTaxExclusive", (object?)isSaleTaxExclusive ?? false);
            cmd.Parameters.AddWithValue("@DepartmentId", (object?)departmentId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SectionId", (object?)sectionId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CategoryId", (object?)categoryId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SubCategoryId", (object?)subCategoryId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CriticalLimit", (object?)criticalLimit ?? 0m);
            cmd.Parameters.AddWithValue("@SaleUnitId", (object?)saleUnitId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedOn", (object?)createdOn ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", (object?)createdBy ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedOn", (object?)updatedOn ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@UpdatedBy", (object?)updatedBy ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentType", (object?)documentType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentStatus", (object?)documentStatus ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", true);
            cmd.Parameters.AddWithValue("@BranchId", (object?)branchId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CompanyId", (object?)companyId ?? DBNull.Value);

            var insertedIdParam = new SqlParameter("@InsertedId", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var responseParam = new SqlParameter("@Response", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(insertedIdParam);
            cmd.Parameters.Add(responseParam);

            await cmd.ExecuteNonQueryAsync();
            return (response: responseParam.Value == DBNull.Value ? null : (int?)responseParam.Value, insertedId: insertedIdParam.Value == DBNull.Value ? null : (int?)insertedIdParam.Value);

        }
        public async Task<int?> UpsertInto_IProductATI(string? operationType, Guid? guId, int? productId, int? inventoryAccountId, int? saleRevenueAccountId, int? costOfSaleAccountId, int? itemTypeId, int? hsCodeId, int? saleTaxTypeId, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, SqlConnection con, SqlTransaction trans)
        {
            using var cmd = new SqlCommand("IProductATI_Upsert", con, trans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DB_OperationType", (object)operationType! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GuID", (object)guId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ProductId", (object)productId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@InventoryAccountId", (object)inventoryAccountId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SaleRevenueAccountId", (object)saleRevenueAccountId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@costOfSaleAccountId", (object)costOfSaleAccountId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ItemTypeId", (object)itemTypeId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@HSCodeId", (object)hsCodeId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SaleTaxTypeId", (object)saleTaxTypeId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedOn", (object)createdOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", (object?)createdBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedOn", (object?)updatedOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@UpdatedBy", (object?)updatedBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentType", (object?)documentType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentStatus", (object?)documentStatus ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", true);
            var responseParam = new SqlParameter("@Response", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(responseParam);

            await cmd.ExecuteNonQueryAsync();
            return responseParam.Value == DBNull.Value ? null : (int?)responseParam.Value;
        }
        public async Task<(int? response, int? insertedId)> UpsertInto_SOCustomer(string? operationType, Guid? guId, string? description, string? contact, string? email, string? cnicNumber, string? address, string? additionalDetail, int? receivableAccountId, decimal? openingBalance, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans)
        {
            using var cmd = new SqlCommand("SOCustomer_Upsert", con, trans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DB_OperationType", (object)operationType! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GuID", (object)guId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Description", (object?)description ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Contact", (object?)contact ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Email", (object?)email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CNICNumber", (object?)cnicNumber ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Address", (object?)address ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@AdditionalDetail", (object?)additionalDetail ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ReceivableAccountId", (object)receivableAccountId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@OpeningBalance", (object)openingBalance! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedOn", (object)createdOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", (object?)createdBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedOn", (object?)updatedOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@UpdatedBy", (object?)updatedBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentType", (object?)documentType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentStatus", (object?)documentStatus ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", true);
            cmd.Parameters.AddWithValue("@BranchId", (object?)branchId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CompanyId", (object?)companyId ?? DBNull.Value);
            var insertedIdParam = new SqlParameter("@InsertedId", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var responseParam = new SqlParameter("@Response", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(insertedIdParam);
            cmd.Parameters.Add(responseParam);

            await cmd.ExecuteNonQueryAsync();
            return (response: responseParam.Value == DBNull.Value ? null : (int?)responseParam.Value, insertedId: insertedIdParam.Value == DBNull.Value ? null : (int?)insertedIdParam.Value);
        }
        public async Task<(int? response, int? insertedId)> UpsertInto_AFChartOfAccount(string? operationType, Guid? guId, string? description, int? accountCategoryId, int? financialStatementId, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans)
        {
            using var cmd = new SqlCommand("AFChartOfAccount_Upsert", con, trans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DB_OperationType", (object)operationType! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GuID", (object)guId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Description", (object)description! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@AccountCategoryId", (object)accountCategoryId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@FinancialStatementId", (object)financialStatementId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedOn", (object)createdOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", (object?)createdBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedOn", (object?)updatedOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@UpdatedBy", (object?)updatedBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentType", (object?)documentType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentStatus", (object?)documentStatus ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", true);
            cmd.Parameters.AddWithValue("@BranchId", (object?)branchId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CompanyId", (object?)companyId ?? DBNull.Value);
            var insertedIdParam = new SqlParameter("@InsertedId", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var responseParam = new SqlParameter("@Response", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(insertedIdParam);
            cmd.Parameters.Add(responseParam);

            await cmd.ExecuteNonQueryAsync();
            return (response: responseParam.Value == DBNull.Value ? null : (int?)responseParam.Value, insertedId: insertedIdParam.Value == DBNull.Value ? null : (int?)insertedIdParam.Value);
        }
        public async Task<(int? response, int? insertedId, string? documentCode, decimal? totalInvoiceAmount)> UpsertInto_AFInvoice(string? operationType, Guid? guId, int? locationId, DateTime? transactionDate, int? customerId, string? description, string? fbrStamp, decimal dueAmount, int? invoiceTypeId, int? invoiceStatus, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, List<AFInvoiceProductPricing_TVP> invoicePPI, SqlConnection con, SqlTransaction trans)
        {
            using var cmd = new SqlCommand("AFInvoice_Upsert", con, trans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DB_OperationType", (object)operationType! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GuID", (object)guId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@LocationId", (object)locationId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TransactionDate", (object)transactionDate! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CustomerId", (object)customerId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Description", (object)description! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@FBRStamp", (object)fbrStamp! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DueAmount", (object)dueAmount! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@InvoiceTypeId", (object)invoiceTypeId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@InvoiceStatus", (object)invoiceStatus! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedOn", (object)createdOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", (object?)createdBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedOn", (object?)updatedOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@UpdatedBy", (object?)updatedBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentType", (object?)documentType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentStatus", (object?)documentStatus ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", true);
            cmd.Parameters.AddWithValue("@BranchId", (object?)branchId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CompanyId", (object?)companyId ?? DBNull.Value);
            DataTable table = new DataTable();
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("GuID", typeof(Guid));
            table.Columns.Add("InvoiceId", typeof(int));
            table.Columns.Add("ProductId", typeof(int));
            table.Columns.Add("Quantity", typeof(decimal));
            table.Columns.Add("ActualAmount", typeof(decimal));
            table.Columns.Add("DiscountAmount", typeof(decimal));
            table.Columns.Add("ChargedAmount", typeof(decimal));
            table.Columns.Add("CreatedOn", typeof(DateTime));
            table.Columns.Add("CreatedBy", typeof(int));
            table.Columns.Add("UpdatedOn", typeof(DateTime));
            table.Columns.Add("UpdatedBy", typeof(int));
            table.Columns.Add("DocumentType", typeof(int));
            table.Columns.Add("DocumentStatus", typeof(int));
            table.Columns.Add("Status", typeof(bool));

            foreach (var item in invoicePPI)
            {
                table.Rows.Add(
                    item.Id,
                    item.GuID,
                    (object?)item.InvoiceId ?? DBNull.Value,
                    (object?)item.ProductId ?? DBNull.Value,
                    (object)item.Quantity ?? DBNull.Value,
                    (object)item.ActualAmount ?? DBNull.Value,
                    (object)item.DiscountAmount ?? DBNull.Value,
                    (object)item.ChargedAmount ?? DBNull.Value,
                    (object?)item.CreatedOn ?? DBNull.Value,
                    (object?)item.CreatedBy ?? DBNull.Value,
                    (object?)item.UpdatedOn ?? DBNull.Value,
                    (object?)item.UpdatedBy ?? DBNull.Value,
                    (object?)item.DocumentType ?? DBNull.Value,
                    (object?)item.DocumentStatus ?? DBNull.Value,
                    (object?)item.Status ?? DBNull.Value
                );
            }

            var tableValuedParam = new SqlParameter("@AFInvoice_TVP", SqlDbType.Structured)
            {
                TypeName = "dbo.AFInvoiceProduct_TVP",
                Value = table
            };

            var documentCodeParam = new SqlParameter("@DocumentCode", SqlDbType.NVarChar, -1) { Direction = ParameterDirection.Output };
            var totalInvoiceAmountParam = new SqlParameter("@TotalInvoiceAmount", SqlDbType.Decimal) { Direction = ParameterDirection.Output,Precision = 18,Scale = 2};
            var insertedIdParam = new SqlParameter("@InsertedId", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var responseParam = new SqlParameter("@Response", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(tableValuedParam);
            cmd.Parameters.Add(documentCodeParam);
            cmd.Parameters.Add(totalInvoiceAmountParam);
            cmd.Parameters.Add(insertedIdParam);
            cmd.Parameters.Add(responseParam);

            await cmd.ExecuteNonQueryAsync();
            return (response: responseParam.Value == DBNull.Value ? null : (int?)responseParam.Value, insertedId: insertedIdParam.Value == DBNull.Value ? null : (int?)insertedIdParam.Value, documentCode: documentCodeParam.Value == DBNull.Value ? null : (string?)documentCodeParam.Value, totalInvoiceAmount: totalInvoiceAmountParam.Value == DBNull.Value ? null : Convert.ToDecimal(totalInvoiceAmountParam.Value));
        }
        public async Task<(int? response, int? insertedId, string? documentCode)> UpsertInto_AFCustomerLedger(string? operationType, int? companyId, List<AFCustomerLedger_TVP> customerLedger, SqlConnection con, SqlTransaction trans)
        {
            using var cmd = new SqlCommand("AFCustomerLedger_Upsert", con, trans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DB_OperationType", (object)operationType! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CompanyId", (object)companyId! ?? DBNull.Value);
            DataTable table = new DataTable();
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("GuID", typeof(Guid));
            table.Columns.Add("Code", typeof(string));
            table.Columns.Add("LocationId", typeof(int));
            table.Columns.Add("TransactionDate", typeof(DateTime));
            table.Columns.Add("CustomerId", typeof(int));
            table.Columns.Add("RefDocumentType", typeof(int));
            table.Columns.Add("RefDocumentId", typeof(int));
            table.Columns.Add("Description", typeof(string));
            table.Columns.Add("Debit", typeof(decimal));
            table.Columns.Add("Credit", typeof(decimal));
            table.Columns.Add("ReconcillationStatus", typeof(int));
            table.Columns.Add("CreatedOn", typeof(DateTime));
            table.Columns.Add("CreatedBy", typeof(int));
            table.Columns.Add("UpdatedOn", typeof(DateTime));
            table.Columns.Add("UpdatedBy", typeof(int));
            table.Columns.Add("DocumentType", typeof(int));
            table.Columns.Add("DocumentStatus", typeof(int));
            table.Columns.Add("Status", typeof(bool));
            table.Columns.Add("BranchId", typeof(int));
            table.Columns.Add("CompanyId", typeof(int));

            foreach (var item in customerLedger)
            {
                table.Rows.Add(
                    item.Id,
                    item.GuID,
                    (object?)item.Code ?? DBNull.Value,
                    (object?)item.LocationId ?? DBNull.Value,
                    (object?)item.TransactionDate ?? DBNull.Value,
                    (object?)item.CustomerId ?? DBNull.Value,
                    (object?)item.RefDocumentType ?? DBNull.Value,
                    (object?)item.RefDocumentId ?? DBNull.Value,
                    (object?)item.Description ?? DBNull.Value,
                    (object?)item.Debit ?? DBNull.Value,
                    (object?)item.Credit ?? DBNull.Value,
                    (object?)item.ReconcillationStatus ?? DBNull.Value,
                    (object?)item.CreatedOn ?? DBNull.Value,
                    (object?)item.CreatedBy ?? DBNull.Value,
                    (object?)item.UpdatedOn ?? DBNull.Value,
                    (object?)item.UpdatedBy ?? DBNull.Value,
                    (object?)item.DocumentType ?? DBNull.Value,
                    (object?)item.DocumentStatus ?? DBNull.Value,
                    (object?)item.Status ?? DBNull.Value,
                    (object?)item.BranchId ?? DBNull.Value,
                    (object?)item.CompanyId ?? DBNull.Value
                );
            }
            var tableValuedParam = new SqlParameter("@AFCustomerLedger_TVP", SqlDbType.Structured)
            {
                TypeName = "dbo.AFCustomerLedger_TVP",
                Value = table
            };
            var documentCodeParam = new SqlParameter("@DocumentCode", SqlDbType.NVarChar, -1) { Direction = ParameterDirection.Output };
            var insertedIdParam = new SqlParameter("@InsertedId", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var responseParam = new SqlParameter("@Response", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(tableValuedParam);
            cmd.Parameters.Add(documentCodeParam);
            cmd.Parameters.Add(insertedIdParam);
            cmd.Parameters.Add(responseParam);

            await cmd.ExecuteNonQueryAsync();
            return (response: responseParam.Value == DBNull.Value ? null : (int?)responseParam.Value, insertedId: insertedIdParam.Value == DBNull.Value ? null : (int?)insertedIdParam.Value, documentCode: documentCodeParam.Value == DBNull.Value ? null : (string?)documentCodeParam.Value);
        }
        public async Task<int?> UpsertInto_AFJournalVoucher(string? operationType, int? companyId, List<AFJournalVoucher> journalVoucher, SqlConnection con, SqlTransaction trans)
        {
            using var cmd = new SqlCommand("AFJournalVoucher_Upsert", con, trans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DB_OperationType", (object)operationType! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CompanyId", (object)companyId! ?? DBNull.Value);
            DataTable table = new DataTable();
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("GuID", typeof(Guid));
            table.Columns.Add("Code", typeof(string));
            table.Columns.Add("LocationId", typeof(int));
            table.Columns.Add("RefDocumentType", typeof(int));
            table.Columns.Add("RefDocumentId", typeof(int));
            table.Columns.Add("Description", typeof(string));
            table.Columns.Add("ChartOfAccountId", typeof(int));
            table.Columns.Add("Debit", typeof(decimal));
            table.Columns.Add("Credit", typeof(decimal));
            table.Columns.Add("PostingStatus", typeof(int));
            table.Columns.Add("CreatedOn", typeof(DateTime));
            table.Columns.Add("CreatedBy", typeof(int));
            table.Columns.Add("UpdatedOn", typeof(DateTime));
            table.Columns.Add("UpdatedBy", typeof(int));
            table.Columns.Add("DocumentType", typeof(int));
            table.Columns.Add("DocumentStatus", typeof(int));
            table.Columns.Add("Status", typeof(bool));
            table.Columns.Add("BranchId", typeof(int));
            table.Columns.Add("CompanyId", typeof(int));

            foreach (var item in journalVoucher)
            {
                table.Rows.Add(
                    item.Id,
                    item.GuID,
                    (object?)item.Code ?? DBNull.Value,
                    (object?)item.LocationId ?? DBNull.Value,
                    (object?)item.RefDocumentType ?? DBNull.Value,
                    (object?)item.RefDocumentId ?? DBNull.Value,
                    (object?)item.Description ?? DBNull.Value,
                    (object?)item.ChartOfAccountId ?? DBNull.Value,
                    (object?)item.Debit ?? DBNull.Value,
                    (object?)item.Credit ?? DBNull.Value,
                    (object?)item.PostingStatus ?? DBNull.Value,
                    (object?)item.CreatedOn ?? DBNull.Value,
                    (object?)item.CreatedBy ?? DBNull.Value,
                    (object?)item.UpdatedOn ?? DBNull.Value,
                    (object?)item.UpdatedBy ?? DBNull.Value,
                    (object?)item.DocumentType ?? DBNull.Value,
                    (object?)item.DocumentStatus ?? DBNull.Value,
                    (object?)item.Status ?? DBNull.Value,
                    (object?)item.BranchId ?? DBNull.Value,
                    (object?)item.CompanyId ?? DBNull.Value
                );
            }
            var tableValuedParam = new SqlParameter("@AFJournalVoucher_TVP", SqlDbType.Structured)
            {
                TypeName = "dbo.AFJournalVoucher_TVP",
                Value = table
            };
            var responseParam = new SqlParameter("@Response", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(tableValuedParam);
            cmd.Parameters.Add(responseParam);

            await cmd.ExecuteNonQueryAsync();
            return responseParam.Value == DBNull.Value ? null : (int?)responseParam.Value;

        }
        public async Task<(int? response, int? insertedId, string? documentCode)> UpsertInto_AFPaymentReceipt(string? operationType,Guid? guId,int? locationId,DateTime? transactionDate,int? customerId,int? invoiceId,string? description,int? paymentMethodId,string? reference,decimal? receiptAmount,int? paymentStatus,DateTime? createdOn,int? createdBy,DateTime? updatedOn,int? updatedBy,int? documentType,int? documentStatus,int? branchId,int? companyId,SqlConnection con,SqlTransaction trans)
        {
            using var cmd = new SqlCommand("AFPaymentReceipt_Upsert", con, trans);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DB_OperationType", (object)operationType! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GuID", (object)guId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@LocationId", (object)locationId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TransactionDate", (object)transactionDate! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CustomerId", (object)customerId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@InvoiceId", (object)invoiceId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Description", (object)description! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PaymentMethodId", (object)paymentMethodId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Reference", (object)reference! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ReceiptAmount", (object)receiptAmount! ?? 0);
            cmd.Parameters.AddWithValue("@PaymentStatus", (object)paymentStatus! ?? DBNull.Value);

            cmd.Parameters.AddWithValue("@CreatedOn", (object)createdOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", (object)createdBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedOn", (object)updatedOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@UpdatedBy", (object)updatedBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentType", (object)documentType! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentStatus", (object)documentStatus! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", true);
            cmd.Parameters.AddWithValue("@BranchId", (object)branchId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CompanyId", (object)companyId! ?? DBNull.Value);

            var insertedIdParam = new SqlParameter("@InsertedId", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var documentCodeParam = new SqlParameter("@DocumentCode", SqlDbType.NVarChar, -1) { Direction = ParameterDirection.Output };
            var responseParam = new SqlParameter("@Response", SqlDbType.Int) { Direction = ParameterDirection.Output };

            cmd.Parameters.Add(insertedIdParam);
            cmd.Parameters.Add(documentCodeParam);
            cmd.Parameters.Add(responseParam);

            await cmd.ExecuteNonQueryAsync();

            return (
                response: responseParam.Value == DBNull.Value ? null : (int?)responseParam.Value,
                insertedId: insertedIdParam.Value == DBNull.Value ? null : (int?)insertedIdParam.Value,
                documentCode: documentCodeParam.Value == DBNull.Value ? null : (string?)documentCodeParam.Value
            );
        }
        #endregion

        #region RETRIEVE OPERATION SP
        public async Task<IReadOnlyList<DTObject.Invoice_List>> ret_Invoices_ByCustomer( Guid? guId, int? customerId, int?[] documentStatusIds, int?[] invoiceStatusIds, string connStr)
        {
            using IDbConnection db = new SqlConnection(connStr);
            var parameters = new
            {
                GuID = guId,
                CustomerId = customerId,
                DocumentStatus = documentStatusIds != null ? string.Join(",", documentStatusIds) : null,
                InvoiceStatuses = invoiceStatusIds != null ? string.Join(",", invoiceStatusIds) : null,
            };
            try
            {
                var result = await db.QueryAsync<DTObject.Invoice_List>("[dbo].[AFInvoice_GLBParam]", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList().AsReadOnly();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
