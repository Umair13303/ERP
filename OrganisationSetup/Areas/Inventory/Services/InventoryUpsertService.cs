using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OrganisationSetup.Models.DAL;
using OrganisationSetup.Models.DAL.StoredProcedure;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using SharedUI.Models.Responses;
using SharedUI.Models.SQLParameters;
using SharedUI.Models.TVP;
using System.Diagnostics;


namespace OrganisationSetup.Areas.Inventory.Services
{
    public interface IInventoryUpsert
    {
        Task<ServiceResult> updateInsertDataInto_ISection(PostedData postedData);
        Task<ServiceResult> updateInsertDataInto_ICategory(PostedData postedData);
        Task<ServiceResult> updateInsertDataInto_ISubCategory(PostedData postedData);
        Task<ServiceResult> updateInsertDataInto_IBrand(PostedData postedData);
        Task<ServiceResult> updateInsertDataInto_IProduct(PostedData postedData);
        Task<ServiceResult> updateInsertDataInto_IProductATI(PostedData postedData);
        Task<ServiceResult> updateInsertDataInto_ISupplier(PostedData postedData);
        Task<ServiceResult> updateInsertDataInto_IInventoryAdjustment(PostedData postedData, List<IInventoryAdjustmentPPQD_TVP> detailRows);

    }
    public class InventoryUpsertService : IInventoryUpsert
    {
        private readonly TempUser _currentUser;
        private readonly IOSDataLayer _repo;
        private readonly string _connectionString;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IInventoryValidation _validationService;
        private readonly ERPOrganisationSetupContext _eRPOSContext;
        public InventoryUpsertService(TempUser currentUser, IOSDataLayer repo, ERPOrganisationSetupContext context, IHttpContextAccessor httpContextAccessor ,IInventoryValidation validationService, ERPOrganisationSetupContext eRPOSC)
        {
            _currentUser = currentUser;
            _repo = repo;
            _connectionString = context.Database.GetDbConnection().ConnectionString;
            _httpContextAccessor = httpContextAccessor;
            _validationService = validationService;
            _eRPOSContext = eRPOSC;
        }
        public async Task<ServiceResult> updateInsertDataInto_ISection(PostedData postedData)
        {
            var userInfo = _currentUser;

            if (!userInfo.IsAuthenticated)
                return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);

            #region PORTION FOR :: DOCUMENT SETTING ON BASIS OF OperationType
            Guid? sectionGuID = Guid.Empty;
            if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
                sectionGuID = Guid.NewGuid();
            }
            else
            {
                sectionGuID = postedData.GuID;
            }
            bool? isOperationPermitted = await _validationService.isISectionValid(postedData.OperationType, sectionGuID,postedData.DepartmentId, postedData.Description);
            #endregion
            if (isOperationPermitted == true)
            {
                using var con = new SqlConnection(_connectionString);
                await con.OpenAsync();
                using var transaction = con.BeginTransaction();
                try
                {
                    #region PORTION FOR :: UPSERT INTO dbo.ISection
                    var ISection = await _repo.UpsertInto_ISection(
                                                            postedData.OperationType,
                                                            sectionGuID,
                                                            postedData.Description?.Trim(),
                                                            postedData.DepartmentId,
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            (int?)DocumentType.section,
                                                            (int?)DocumentStatus.active,
                                                            userInfo.BranchId,
                                                            userInfo.CompanyId,
                                                            con, transaction);
                    #endregion

                    #region PORTION FOR :: HANLDE TRANSACTION
                    switch (ISection.Value)
                    {
                        case (int)Code.Created:
                        case (int)Code.Accepted:
                            await transaction.CommitAsync();
                            return ServiceResult.success(Message.serverResponse(ISection.Value), (int)ISection.Value);
                        default:
                            await transaction.RollbackAsync();
                            return ServiceResult.failure(Message.serverResponse((int?)Code.BadRequest), (int)Code.BadRequest);
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return ServiceResult.failure(Message.serverResponse((int?)Code.InternalServerError), (int)Code.InternalServerError);
                }
            }
            else
            {
                return ServiceResult.failure(Message.serverResponse((int?)Code.Conflict), (int)Code.Conflict);
            }
        }
        public async Task<ServiceResult> updateInsertDataInto_ICategory(PostedData postedData)
        {
            var userInfo = _currentUser;

            if (!userInfo.IsAuthenticated)
                return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);

            #region PORTION FOR :: DOCUMENT SETTING ON BASIS OF OperationType
            Guid? categoryGuID = Guid.Empty;
            if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
                categoryGuID = Guid.NewGuid();
            }
            else
            {
                categoryGuID = postedData.GuID;
            }
            bool? isOperationPermitted = await _validationService.isICategoryValid(postedData.OperationType, categoryGuID, postedData.DepartmentId, postedData.Description);
            #endregion

            if (isOperationPermitted == true)
            {
                using var con = new SqlConnection(_connectionString);
                await con.OpenAsync();
                using var transaction = con.BeginTransaction();
                try
                {
                    #region PORTION FOR :: UPSERT INTO dbo.ICategory
                    var ICategory = await _repo.UpsertInto_ICategory(
                                                            postedData.OperationType,
                                                            categoryGuID,
                                                            postedData.Description?.Trim(),
                                                            postedData.DepartmentId,
                                                            postedData.SectionId,
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            (int?)DocumentType.category,
                                                            (int?)DocumentStatus.active,
                                                            userInfo.BranchId,
                                                            userInfo.CompanyId,
                                                            con, transaction);
                    #endregion

                    #region PORTION FOR :: HANLDE TRANSACTION
                    switch (ICategory.Value)
                    {
                        case (int)Code.Created:
                        case (int)Code.Accepted:
                            await transaction.CommitAsync();
                            return ServiceResult.success(Message.serverResponse(ICategory.Value), (int)ICategory.Value);
                        default:
                            await transaction.RollbackAsync();
                            return ServiceResult.failure(Message.serverResponse((int?)Code.BadRequest), (int)Code.BadRequest);
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return ServiceResult.failure(Message.serverResponse((int?)Code.InternalServerError), (int)Code.InternalServerError);
                }
            }
            else
            {
                return ServiceResult.failure(Message.serverResponse((int?)Code.Conflict), (int)Code.Conflict);
            }
        }
        public async Task<ServiceResult> updateInsertDataInto_ISubCategory(PostedData postedData)
        {
            var userInfo = _currentUser;

            if (!userInfo.IsAuthenticated)
                return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);

            #region PORTION FOR :: DOCUMENT SETTING ON BASIS OF OperationType
            Guid? subCategoryGuID = Guid.Empty;
            if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
                subCategoryGuID = Guid.NewGuid();
            }
            else
            {
                subCategoryGuID = postedData.GuID;
            }
            bool? isOperationPermitted = await _validationService.isISubCategoryValid(postedData.OperationType, subCategoryGuID,postedData.CategoryId, postedData.Description);
            #endregion

            if (isOperationPermitted == true)
            {
                using var con = new SqlConnection(_connectionString);
                await con.OpenAsync();
                using var transaction = con.BeginTransaction();
                try
                {
                    #region PORTION FOR :: UPSERT INTO dbo.ISubCategory
                    var ISubCategory = await _repo.UpsertInto_ISubCategory(
                                                            postedData.OperationType,
                                                            subCategoryGuID,
                                                            postedData.Description?.Trim(),
                                                            postedData.DepartmentId,
                                                            postedData.SectionId,
                                                            postedData.CategoryId,
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            (int?)DocumentType.subCategory,
                                                            (int?)DocumentStatus.active,
                                                            userInfo.BranchId,
                                                            userInfo.CompanyId,
                                                            con, transaction);
                    #endregion

                    #region PORTION FOR :: HANLDE TRANSACTION
                    switch (ISubCategory.Value)
                    {
                        case (int)Code.Created:
                        case (int)Code.Accepted:
                            await transaction.CommitAsync();
                            return ServiceResult.success(Message.serverResponse(ISubCategory.Value), (int)ISubCategory.Value);
                        default:
                            await transaction.RollbackAsync();
                            return ServiceResult.failure(Message.serverResponse((int?)Code.BadRequest), (int)Code.BadRequest);
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return ServiceResult.failure(Message.serverResponse((int?)Code.InternalServerError), (int)Code.InternalServerError);
                }
            }
            else
            {
                return ServiceResult.failure(Message.serverResponse((int?)Code.Conflict), (int)Code.Conflict);
            }
        }
        public async Task<ServiceResult> updateInsertDataInto_IBrand(PostedData postedData)
        {
            var userInfo = _currentUser;

            if (!userInfo.IsAuthenticated)
                return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);

            #region PORTION FOR :: DOCUMENT SETTING ON BASIS OF OperationType
            Guid? brandGuID = Guid.Empty;
            if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
                brandGuID = Guid.NewGuid();
            }
            else
            {
                brandGuID = postedData.GuID;
            }
            bool? isOperationPermitted = await _validationService.isIBrandValid(postedData.OperationType, brandGuID, postedData.Description);
            #endregion

            if (isOperationPermitted == true)
            {
                using var con = new SqlConnection(_connectionString);
                await con.OpenAsync();
                using var transaction = con.BeginTransaction();
                try
                {
                    #region PORTION FOR :: UPSERT INTO dbo.IBrand
                    var IBrand = await _repo.UpsertInto_IBrand(
                                                            postedData.OperationType,
                                                            brandGuID,
                                                            postedData.Description?.Trim(),
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            (int?)DocumentType.brand,
                                                            (int?)DocumentStatus.active,
                                                            userInfo.BranchId,
                                                            userInfo.CompanyId,
                                                            con, transaction);
                    #endregion

                    #region PORTION FOR :: HANLDE TRANSACTION
                    switch (IBrand.Value)
                    {
                        case (int)Code.Created:
                        case (int)Code.Accepted:
                            await transaction.CommitAsync();
                            return ServiceResult.success(Message.serverResponse(IBrand.Value), (int)IBrand.Value);
                        default:
                            await transaction.RollbackAsync();
                            return ServiceResult.failure(Message.serverResponse((int?)Code.BadRequest), (int)Code.BadRequest);
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return ServiceResult.failure(Message.serverResponse((int?)Code.InternalServerError), (int)Code.InternalServerError);
                }
            }
            else
            {
                return ServiceResult.failure(Message.serverResponse((int?)Code.Conflict), (int)Code.Conflict);
            }
        }
        public async Task<ServiceResult> updateInsertDataInto_IProduct(PostedData postedData)
        {
            var userInfo = _currentUser;

            if (!userInfo.IsAuthenticated)
                return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);

            #region PORTION FOR :: DOCUMENT SETTING ON BASIS OF OperationType
            Guid? productGuID = Guid.Empty;
            Guid? productATIGuID = Guid.Empty;
            if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
                productGuID = Guid.NewGuid();
                productATIGuID = Guid.NewGuid();
            }
            else
            {
                productGuID = postedData.GuID;
                productATIGuID = postedData.ProductATIGuID;
            }
            bool? isOperationPermitted = await _validationService.isIProductValid(postedData.OperationType, productGuID, postedData.Description, postedData.MachineNumber, postedData.SKU);
            #endregion

            if (isOperationPermitted == true)
            {
                using var con = new SqlConnection(_connectionString);
                await con.OpenAsync();
                using var transaction = con.BeginTransaction();
                try
                {
                    #region PORTION FOR :: UPSERT INTO dbo.IProduct
                    var IProduct = await _repo.UpsertInto_IProduct(
                                                            postedData.OperationType,
                                                            productGuID,
                                                            postedData.Description?.Trim(),
                                                            postedData.MachineNumber?.Trim(),
                                                            postedData.SKU?.Trim(),
                                                            postedData.AdditionalDetail?.Trim(),
                                                            postedData.AttributeIds?.Trim(),
                                                            postedData.BrandId,
                                                            postedData.ProductTypeId,
                                                            postedData.IsFavorite,
                                                            postedData.IsSaleTaxExclusive,
                                                            postedData.DepartmentId,
                                                            postedData.SectionId,
                                                            postedData.CategoryId,
                                                            postedData.SubCategoryId,
                                                            postedData.IsExpiryApplicable,
                                                            postedData.CriticalLimit,
                                                            postedData.SaleUnitId,
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            (int?)DocumentType.product,
                                                            (int?)DocumentStatus.active,
                                                            userInfo.BranchId,
                                                            userInfo.CompanyId,
                                                            con, transaction);
                    #endregion

                    #region PORTION FOR :: UPSERT INTO dbo.IProductATI
                    var IProductATI = await _repo.UpsertInto_IProductATI(
                                                    postedData.OperationType,
                                                    productATIGuID,
                                                    IProduct.insertedId!.Value,
                                                    postedData.InventoryAccountId,
                                                    postedData.SaleRevenueAccountId,
                                                    postedData.CostOfSaleAccountId,
                                                    postedData.ItemTypeId,
                                                    postedData.HSCodeId,
                                                    postedData.SaleTaxTypeId,
                                                    postedData.CostingModeId,
                                                    DateTime.Now,
                                                    userInfo.UserId,
                                                    DateTime.Now,
                                                    userInfo.UserId,
                                                    (int?)DocumentType.productATI,
                                                    (int?)DocumentStatus.active,
                                                    con, transaction);
                    #endregion

                    #region PORTION FOR :: HANLDE TRANSACTION
                    switch (IProductATI.Value)
                    {
                        case (int)Code.Created:
                        case (int)Code.Accepted:
                            await transaction.CommitAsync();
                            return ServiceResult.success(Message.serverResponse(IProductATI.Value), (int)IProductATI.Value);
                        default:
                            await transaction.RollbackAsync();
                            return ServiceResult.failure(Message.serverResponse((int?)Code.BadRequest), (int)Code.BadRequest);
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return ServiceResult.failure(Message.serverResponse((int?)Code.InternalServerError), (int)Code.InternalServerError);
                }
            }
            else
            {
                return ServiceResult.failure(Message.serverResponse((int?)Code.Conflict), (int)Code.Conflict);
            }

        }
        public async Task<ServiceResult> updateInsertDataInto_IProductATI(PostedData postedData)
        {
            var userInfo = _currentUser;

            if (!userInfo.IsAuthenticated)
                return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);

            if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
                postedData.GuID = Guid.NewGuid();
            }
            bool? isOperationPermitted = await _validationService.isIProductValid(postedData.OperationType, postedData.GuID, postedData.Description, postedData.MachineNumber,postedData.SKU);

            if (isOperationPermitted == true)
            {
                using var con = new SqlConnection(_connectionString);
                await con.OpenAsync();
                using var transaction = con.BeginTransaction();
                try
                {
                    var result = await _repo.UpsertInto_IProductATI(
                                                            postedData.OperationType,
                                                            postedData.GuID,
                                                            postedData.ProductId,
                                                            postedData.InventoryAccountId,
                                                            postedData.SaleRevenueAccountId,
                                                            postedData.CostOfSaleAccountId,
                                                            postedData.ItemTypeId,
                                                            postedData.HSCodeId,
                                                            postedData.SaleTaxTypeId,
                                                            postedData.CostingModeId,
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            (int?)DocumentType.productATI,
                                                            (int?)DocumentStatus.active,
                                                            con, transaction);
                    await transaction.CommitAsync();

                    return ServiceResult.success(Message.serverResponse(result), result.Value);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return ServiceResult.failure(Message.serverResponse((int?)Code.InternalServerError), (int)Code.InternalServerError);
                }
            }
            else
            {
                return ServiceResult.failure(Message.serverResponse((int?)Code.Conflict), (int)Code.Conflict);
            }

        }
        public async Task<ServiceResult> updateInsertDataInto_ISupplier(PostedData postedData)
        {
            var userInfo = _currentUser;

            if (!userInfo.IsAuthenticated)
                return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);

            #region PORTION FOR :: DOCUMENT SETTING ON BASIS OF OperationType
            Guid? supplierGuID = Guid.Empty;
            Guid? chartOfAccountGuID = Guid.Empty;
            Guid? billGuID = Guid.Empty;
            Guid? supplierLedgerGuID = Guid.Empty;
            Guid? journalVoucherCreditGuID = Guid.Empty;
            Guid? journalVoucherDebitGuID = Guid.Empty;
            if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
                supplierGuID = Guid.NewGuid();
                chartOfAccountGuID = Guid.NewGuid();
                billGuID = Guid.NewGuid();
                supplierLedgerGuID = Guid.NewGuid();
                journalVoucherCreditGuID = Guid.NewGuid();
                journalVoucherDebitGuID = Guid.NewGuid();
            }
            else
            {
                supplierGuID = postedData.GuID;
                chartOfAccountGuID = postedData.GuID;
                billGuID = postedData.GuID;
                supplierLedgerGuID = postedData.GuID;
                journalVoucherCreditGuID = postedData.GuID;
                journalVoucherDebitGuID = postedData.GuID;
            }
            bool? isOperationPermitted = true; //await _validationService.isOSCustomerValid(postedData.OperationType, customerGuID, postedData.Description);
            #endregion

            if (isOperationPermitted == true)
            {
                using var con = new SqlConnection(_connectionString);
                await con.OpenAsync();
                using var transaction = con.BeginTransaction();
                try
                {
                    #region PORTION FOR :: UPSERT INTO dbo.AFChartOfAccount
                    var AFChartOfAccount = await _repo.UpsertInto_AFChartOfAccount(
                                                      postedData.OperationType,
                                                      chartOfAccountGuID,
                                                      postedData.DefaultPayableAccount?.Trim(),
                                                      (int?)AccountCategory.ACCOUNTS_PAYABLE,
                                                      (int?)FinancialStatement.INCOME_STATEMENT,
                                                      DateTime.Now,
                                                      userInfo.UserId,
                                                      DateTime.Now,
                                                      userInfo.UserId,
                                                      (int?)DocumentType.accountChartOfAccount,
                                                      (int?)DocumentStatus.active,
                                                      userInfo.BranchId,
                                                      userInfo.CompanyId,
                                                      con, transaction);
                    #endregion

                    #region PORTION FOR :: UPSERT INTO dbo.ISupplier
                    var ISupplier = await _repo.UpsertInto_ISupplier(
                                                    postedData.OperationType,
                                                    supplierGuID,
                                                    postedData.Description?.Trim(),
                                                    postedData.Contact?.Trim(),
                                                    postedData.Email?.Trim(),
                                                    postedData.CNICNumber?.Trim(),
                                                    postedData.Address?.Trim(),
                                                    postedData.AdditionalDetail?.Trim(),
                                                    AFChartOfAccount.insertedId,
                                                    postedData.OpeningBalance,
                                                    DateTime.Now,
                                                    userInfo.UserId,
                                                    DateTime.Now,
                                                    userInfo.UserId,
                                                    (int?)DocumentType.supplier,
                                                    (int?)DocumentStatus.active,
                                                    userInfo.BranchId,
                                                    userInfo.CompanyId,
                                                    con, transaction);
                    #endregion

                    if (postedData.OpeningBalance > 0)
                    {
                        DateTime transactionDate = DateTime.Now;
                        int? supplierId = ISupplier.insertedId;
                        #region PRE-PARE DOCUMENTS IN CASE OPENING BALANCE > 0

                        #region PORTION FOR :: FILL & UPSERT Bill
                        string Description = "Opening Balance Till: " + DateTime.Now.ToString("dd-MMM-yyyy") + " . ";
                        List<AFBillPPI_TVP> billPI = new List<AFBillPPI_TVP>
                        {
                            new AFBillPPI_TVP
                            {
                                Id = 0,
                                GuID = Guid.NewGuid(),
                                BillId = 0,
                                ProductId = 0,
                                Quantity = 0,
                                ActualAmount = (decimal)postedData.OpeningBalance!,
                                DiscountAmount = 0,
                                ChargedAmount =  (decimal)postedData.OpeningBalance!,
                                CreatedOn = DateTime.Now,
                                CreatedBy = userInfo.UserId,
                                UpdatedOn = DateTime.Now,
                                UpdatedBy = userInfo.UserId,
                                DocumentType = (int?)DocumentType.billProductSupplierOB,
                                DocumentStatus = (int?)DocumentStatus.active,
                                Status = true
                            }
                        };

                        #region PORTION FOR :: UPSERT INTO dbo.AFBill
                        var AFBill = await _repo.UpsertInto_AFBill(
                                                        postedData.OperationType,
                                                        billGuID,
                                                        userInfo.BranchId,
                                                        transactionDate,
                                                        supplierId,
                                                        Description,
                                                        billPI.Sum(x => x.ChargedAmount),
                                                        (int?)BillType.OpeningBalanceBILL,
                                                        (int?)BillStatus.unPaid,
                                                        DateTime.Now,
                                                        userInfo.UserId,
                                                        DateTime.Now,
                                                        userInfo.UserId,
                                                        (int?)DocumentType.bill,
                                                        (int?)DocumentStatus.active,
                                                        userInfo.BranchId,
                                                        userInfo.CompanyId,
                                                        billPI,
                                                        con, transaction);

                        ISupplier.response = AFBill.response;
                        #endregion
                        #endregion

                        #region PORTION FOR :: FILL & UPSERT SupplierLedger
                        Description = Description + " Having Document Code:-  " + AFBill.documentCode;
                        List<AFSupplierLedger_TVP> supplierLedger = new List<AFSupplierLedger_TVP>
                        {
                            new AFSupplierLedger_TVP
                            {
                                Id = 0,
                                GuID = supplierLedgerGuID,
                                Code= "",
                                LocationId = userInfo.BranchId,
                                TransactionDate= transactionDate,
                                SupplierId = supplierId,
                                RefDocumentType = (int?)DocumentType.bill,
                                RefDocumentId=AFBill.insertedId,
                                Description= Description,
                                Debit= (decimal)AFBill.totalBillAmount,
                                Credit =0,
                                ReconcillationStatus= (int?)ReconcileStatus.reconciled,
                                CreatedOn = DateTime.Now,
                                CreatedBy = userInfo.UserId,
                                UpdatedOn = DateTime.Now,
                                UpdatedBy = userInfo.UserId,
                                DocumentType = (int?)DocumentType.supplierLedgerRecord,
                                DocumentStatus = (int?)DocumentStatus.active,
                                Status = true,
                                BranchId= userInfo.BranchId,
                                CompanyId = userInfo.CompanyId
                            }
                        };

                        #region PORTION FOR :: UPSERT INTO dbo.AFSupplierLedger
                        var AFSupplierLedger = await _repo.UpsertInto_AFSupplierLedger(
                                                    postedData.OperationType,
                                                    userInfo.CompanyId,
                                                    supplierLedger,
                                                    con, transaction);

                        #endregion

                        #endregion

                        //   ISupplier.response = AFSupplierLedger.response!.Value;

                        #endregion
                    }

                    #region PORTION FOR :: HANLDE TRANSACTION

                    switch (ISupplier.response)
                    {
                        case (int)Code.Created:
                        case (int)Code.Accepted:
                            await transaction.CommitAsync();
                            return ServiceResult.internalSuccess(Message.serverResponse(ISupplier.response), (int)ISupplier.response, ISupplier.insertedId);
                        default:
                            await transaction.RollbackAsync();
                            return ServiceResult.failure(Message.serverResponse((int?)Code.BadRequest), (int)Code.BadRequest);
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return ServiceResult.failure(Message.serverResponse((int?)Code.InternalServerError), (int)Code.InternalServerError);
                }
            }
            else
            {
                return ServiceResult.failure(Message.serverResponse((int?)Code.Conflict), (int)Code.Conflict);
            }

        }
        //public async Task<ServiceResult> updateInsertDataInto_IInventoryAdjustment(PostedData postedData, List<IInventoryAdjustmentPPQD_TVP> postedDataPPQD)
        //{
        //    var userInfo = _currentUser;
        //    if (!userInfo.IsAuthenticated)
        //        return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);

        //    using var con = new SqlConnection(_connectionString);
        //    await con.OpenAsync();
        //    using var transaction = con.BeginTransaction();
        //    try
        //    {
        //        var result = await _repo.UpsertInto_IInventoryAdjustment(
        //            postedData.OperationType,
        //            postedData.GuID,
        //            postedData.LocationId,
        //            postedData.TransactionDate,
        //            postedData.Description,
        //            (int)AdjustmentStatus.approved,
        //            DateTime.Now,
        //            userInfo.UserId,
        //            DateTime.Now,
        //            userInfo.UserId,
        //            (int?)DocumentType.inventoryAdjustment,
        //            (int?)DocumentStatus.active,
        //            true,
        //            userInfo.BranchId,
        //            userInfo.CompanyId,
        //            detailRows,
        //            con, transaction
        //        );

        //        switch (result.response)
        //        {
        //            case (int)Code.Created:
        //            case (int)Code.Accepted:
        //                await transaction.CommitAsync();
        //                return ServiceResult.success("Adjustment saved", (int)Code.Created);
        //            default:
        //                await transaction.RollbackAsync();
        //                return ServiceResult.failure("Adjustment error", (int)Code.BadRequest);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await transaction.RollbackAsync();
        //        return ServiceResult.failure("System error: " + ex.Message, (int)Code.InternalServerError);
        //    }
        //}
        //{
        //    var userInfo = _currentUser;

        //    if (!userInfo.IsAuthenticated)
        //        return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);

        //    #region PORTION FOR :: DOCUMENT SETTING ON BASIS OF OperationType
        //    Guid? adjustmentGuID = Guid.Empty;
        //    if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
        //    {
        //        adjustmentGuID = Guid.NewGuid();
        //    }
        //    else
        //    {
        //        adjustmentGuID = postedData.GuID;
        //    }
        //    bool? isOperationPermitted = true; //await _validationService.isOSCustomerValid(postedData.OperationType, customerGuID, postedData.Description);
        //    #endregion

        //    if (isOperationPermitted == true)
        //    {

        //        using var con = new SqlConnection(_connectionString);
        //        await con.OpenAsync();
        //        using var transaction = con.BeginTransaction();
        //        try
        //        {
        //            #region PORTION FOR :: UPSERT INTO dbo.IInventoryAdjustment
        //            var IInventoryAdjustment = await _repo.UpsertInto_IInventoryAdjustment(
        //                postedData.OperationType,
        //                adjustmentGuID,
        //                postedData.LocationId,
        //                postedData.TransactionDate,
        //                postedData.Description?.Trim(),
        //                postedData.ProductId,
        //                postedData.AttributeIds?.Trim(),
        //                postedData.InventoryAdjustmentTypeId,
        //                postedData.UnitPurchasePrice,
        //                postedData.UnitSalePrice,
        //                postedData.QuantityIn,
        //                postedData.QuantityOut,
        //                (int?)AdjustmentStatus.approved,
        //                DateTime.Now,
        //                userInfo.UserId,
        //                DateTime.Now,
        //                userInfo.UserId,
        //                (int?)DocumentType.inventoryAdjustment,
        //                (int?)DocumentStatus.active,
        //                true,
        //                userInfo.BranchId,
        //                userInfo.CompanyId,
        //                con, transaction);
        //            #endregion

        //            #region PORTION FOR :: CREATE STOCK LEDGER ENTRY
        //            var netQuantity = (postedData.QuantityIn ?? 0) - (postedData.QuantityOut ?? 0);
        //            var unitCost = netQuantity > 0 ? (decimal?)postedData.UnitPurchasePrice ?? 0 : (decimal?)postedData.UnitSalePrice ?? 0;

        //            var productATI = await _eRPOSContext.IProductATI.FirstOrDefaultAsync(x => x.ProductId == postedData.ProductId);

        //            var stockLedgerEntries = new List<IStockLedger_TVP>
        //{
        //    new IStockLedger_TVP
        //    {
        //        Id = 0,
        //        GuID = Guid.NewGuid(),
        //        LocationId = postedData.LocationId,
        //        TransactionDate = postedData.TransactionDate ?? DateTime.Now,
        //        ProductId = postedData.ProductId,
        //        RefDocumentType = (int?)DocumentType.inventoryAdjustment,
        //        RefDocumentId = IInventoryAdjustment.insertedId,
        //        Description = $"Adjustment: {postedData.Description}",
        //        InQty = postedData.QuantityIn ?? 0,
        //        OutQty = postedData.QuantityOut ?? 0,
        //        UnitCost = unitCost,
        //        CostingModeId = productATI?.CostingModeId,
        //        CreatedOn = DateTime.Now,
        //        CreatedBy = userInfo.UserId,
        //        UpdatedOn = DateTime.Now,
        //        UpdatedBy = userInfo.UserId,
        //        DocumentType = (int?)DocumentType.stockLedgerRecord,
        //        DocumentStatus = (int?)DocumentStatus.active,
        //        Status = true,
        //        BranchId = userInfo.BranchId,
        //        CompanyId = userInfo.CompanyId
        //    }
        //};

        //            // var stockLedgerResponse = await _repo.UpsertInto_IStockLedger(
        //            //     userInfo.CompanyId,
        //            //     stockLedgerEntries,
        //            //     con,
        //            //     transaction);
        //            #endregion

        //            #region PORTION FOR :: HANDLE TRANSACTION
        //            switch (IInventoryAdjustment.response)
        //            {
        //                case (int)Code.Created:
        //                case (int)Code.Accepted:
        //                    await transaction.CommitAsync();
        //                    return ServiceResult.success(Message.serverResponse(IInventoryAdjustment.response), (int)IInventoryAdjustment.response);
        //                default:
        //                    await transaction.RollbackAsync();
        //                    return ServiceResult.failure(Message.serverResponse((int?)Code.BadRequest), (int)Code.BadRequest);
        //            }
        //            #endregion
        //        }
        //        catch (Exception ex)
        //        {
        //            await transaction.RollbackAsync();
        //            return ServiceResult.failure(Message.serverResponse((int?)Code.InternalServerError), (int)Code.InternalServerError);
        //        }
        //    }
        //    else
        //    {
        //        return ServiceResult.failure(Message.serverResponse((int?)Code.Conflict), (int)Code.Conflict);
        //    }
        //}
        public async Task<ServiceResult> updateInsertDataInto_IInventoryAdjustment(
        PostedData postedData, List<IInventoryAdjustmentPPQD_TVP> detailRows)
        {
            var userInfo = _currentUser;
            if (!userInfo.IsAuthenticated)
                return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);
            #region PORTION FOR :: DOCUMENT SETTING ON BASIS OF OperationType
            Guid? adjustmentGuID = Guid.Empty;
            if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
                adjustmentGuID = Guid.NewGuid();
            }
            else
            {
                adjustmentGuID = postedData.GuID;
            }
            bool? isOperationPermitted = true; //await _validationService.isOSCustomerValid(postedData.OperationType, customerGuID, postedData.Description);
            #endregion
            using var con = new SqlConnection(_connectionString);
            await con.OpenAsync();
            using var transaction = con.BeginTransaction();
            try
            {

                #region PORTION FOR :: UPSERT INTO dbo.IInventoryAdjustment
                var IInventoryAdjustment = await _repo.UpsertInto_IInventoryAdjustment(
                    postedData.OperationType,
                    adjustmentGuID,
                    postedData.LocationId,
                    postedData.TransactionDate,
                    postedData.Description,
                    postedData.AdjustmentTypeId,
                    (int)AdjustmentStatus.approved,
                    DateTime.Now,
                    userInfo.UserId,
                    DateTime.Now,
                    userInfo.UserId,
                    (int?)DocumentType.inventoryAdjustment,
                    (int?)DocumentStatus.active,
                    true,
                    userInfo.BranchId,
                    userInfo.CompanyId,
                    postedData.PostedDataIAdjustmentPPQD,
                    con, transaction
                );
                #endregion
                #region PORTION FOR :: UPSERT INTO dbo.IStockLedger
                var IStockLedger = new List<IStockLedger_TVP>
                {
                    new IStockLedger_TVP
                    {
                        Id = 0,
                        GuID = Guid.NewGuid(),
                        LocationId = postedData.LocationId,
                        TransactionDate = postedData.TransactionDate ?? DateTime.Now,
                        ProductId = postedData.ProductId,
                        RefDocumentType = (int?)DocumentType.inventoryAdjustment,
                        RefDocumentId = IInventoryAdjustment.insertedId,
                        Description = $"Adjustment: {postedData.Description} Recorded",
                        InQty = postedData.QuantityIn ?? 0,
                        OutQty = postedData.QuantityOut ?? 0,
                     //   UnitCost = unitCost,
                     //   CostingModeId = productATI?.CostingModeId,
                        CreatedOn = DateTime.Now,
                        CreatedBy = userInfo.UserId,
                        UpdatedOn = DateTime.Now,
                        UpdatedBy = userInfo.UserId,
                        DocumentType = (int?)DocumentType.stockLedgerRecord,
                        DocumentStatus = (int?)DocumentStatus.active,
                        Status = true,
                        BranchId = userInfo.BranchId,
                        CompanyId = userInfo.CompanyId
                    }
                };
                #endregion

                #region PORTION FOR :: HANDLE TRANSACTION
                switch (IInventoryAdjustment.response)
                {
                    case (int)Code.Created:
                    case (int)Code.Accepted:
                        await transaction.CommitAsync();
                        return ServiceResult.success(Message.serverResponse(IInventoryAdjustment.response), (int)IInventoryAdjustment.response);
                    default:
                        await transaction.RollbackAsync();
                        return ServiceResult.failure(Message.serverResponse((int?)Code.BadRequest), (int)Code.BadRequest);
                }
                #endregion
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return ServiceResult.failure("System error: " + ex.Message, (int)Code.InternalServerError);
            }
        }
    }
}