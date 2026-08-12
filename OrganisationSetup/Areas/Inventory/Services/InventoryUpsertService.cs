using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OrganisationSetup.Models.DAL;
using OrganisationSetup.Models.DAL.StoredProcedure;
using OrganisationSetup.Services;
using SharedUI.Models.Contexts;
using SharedUI.Models.Custom;
using SharedUI.Models.Enums;
using SharedUI.Models.Responses;
using SharedUI.Models.SQLParameters;
using SharedUI.Models.TVP;
using System.Text.Json;



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
        Task<ServiceResult> updateInsertDataInto_IAdjustment(PostedData postedData);

        #region SOFT DELETE INVENTORY DOCUMENT
        Task<ServiceResult> updateDocument_BrandByGuID(Guid? guid, bool? status, int documentStatus = (int)DocumentStatus.active);
        Task<ServiceResult> updateDocument_CategoryByGuID(Guid? guid, bool? status, int documentStatus = (int)DocumentStatus.active);
        Task<ServiceResult> updateDocument_SubCategoryByGuID(Guid? guid, bool? status, int documentStatus = (int)DocumentStatus.active);
        #endregion
    }
    public class InventoryUpsertService : IInventoryUpsert
    {
        private readonly TempUser _currentUser;
        private readonly IOSDataLayer _repo;
        private readonly string _connectionString;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IInventoryValidation _validationService;
        private readonly ERPOrganisationSetupContext _eRPOSContext;
        private readonly ICommon _commonServices;
        public InventoryUpsertService(TempUser currentUser, IOSDataLayer repo, ERPOrganisationSetupContext context, IHttpContextAccessor httpContextAccessor, IInventoryValidation validationService, ERPOrganisationSetupContext eRPOSC, ICommon commonServices)
        {
            _currentUser = currentUser;
            _repo = repo;
            _connectionString = context.Database.GetDbConnection().ConnectionString;
            _httpContextAccessor = httpContextAccessor;
            _validationService = validationService;
            _eRPOSContext = eRPOSC;
            _commonServices = commonServices;
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
            bool? isOperationPermitted = await _validationService.isISectionValid(postedData.OperationType, sectionGuID, postedData.DepartmentId, postedData.Description);
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
            bool? isOperationPermitted = await _validationService.isISubCategoryValid(postedData.OperationType, subCategoryGuID, postedData.CategoryId, postedData.Description);
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
            bool? isOperationPermitted = await _validationService.isIProductValid(postedData.OperationType, postedData.GuID, postedData.Description, postedData.MachineNumber, postedData.SKU);

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
        public async Task<ServiceResult> updateInsertDataInto_IAdjustment(PostedData postedData)
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

            var validationResult = await _validationService.isAdjustmentValid(postedData.OperationType, adjustmentGuID, userInfo.CompanyId, postedData.LocationId, postedData.AdjustmentTypeId, postedData.PostedDataIAdjustmentPPQD);
            if (!validationResult.IsSuccess)
            {
                return ServiceResult.failure(validationResult.Message, (int)validationResult.StatusCode);
            }
            var adjustmentType = await _eRPOSContext.vInventoryAdjustmentType.AsNoTracking().FirstOrDefaultAsync(x => x.Id == postedData.AdjustmentTypeId);
            foreach (var item in postedData.PostedDataIAdjustmentPPQD)
            {
                item.Attribute = CSharedUtility.attributeKeyBuilder(item.Attribute);
            }
            bool? isOperationPermitted = true;
            #endregion

            await using var transaction = await _eRPOSContext.Database.BeginTransactionAsync();
            var con = (SqlConnection)_eRPOSContext.Database.GetDbConnection();
            var sqlTransaction = (SqlTransaction)transaction.GetDbTransaction();

            try
            {
                DateTime? transactionDate = postedData.TransactionDate;
                await iProductCCE_SPR((int)DocumentType.inventoryAdjustment, postedData.PostedDataIAdjustmentPPQD);

                #region PORTION FOR :: UPSERT INTO dbo.IAdjustment
                var IAdjustment = await _repo.UpsertInto_IAdjustment(
                    postedData.OperationType,
                    adjustmentGuID,
                    postedData.LocationId,
                    postedData.TransactionDate,
                    postedData.Description,
                    postedData.AdjustmentTypeId,
                    (int)Default.adjustmentStatus,
                    transactionDate,
                    userInfo.UserId,
                    transactionDate,
                    userInfo.UserId,
                    (int?)DocumentType.inventoryAdjustment,
                    (int?)DocumentStatus.active,
                    true,
                    userInfo.BranchId,
                    userInfo.CompanyId,
                    postedData.PostedDataIAdjustmentPPQD,
                    con, sqlTransaction
                );
                #endregion

                #region PORTION FOR :: UPSERT INTO dbo.AFInventoryLedger
                var AFInventoryLedgerInfo = postedData.PostedDataIAdjustmentPPQD.Select(ppqd => new AFInventoryLedger_TVP
                {
                    GuID = Guid.NewGuid(),
                    LocationId = postedData.LocationId,
                    TransactionDate = transactionDate,
                    ProductId = ppqd.ProductId,
                    ProductCombinationId = ppqd.ProductCombinationId,
                    RefDocumentType = (int?)DocumentType.inventoryAdjustment,
                    RefDocumentId = (int?)IAdjustment.insertedId,
                    Description = $"Inventory Adjustment Recorded ({adjustmentType.Description})",
                    QuantityIn = ppqd.QuantityIn,
                    QuantityOut = ppqd.QuantityOut,
                    UnitPurchasePrice = ppqd.UnitPurchasePrice,
                    UnitSalePrice = ppqd.UnitSalePrice,
                    Debit = ppqd.UnitPurchasePrice * ppqd.QuantityIn,
                    Credit = ppqd.UnitPurchasePrice * ppqd.QuantityOut,
                    Batch = ppqd.Batch,
                    ExpiryDate = ppqd.ExpiryDate,
                    ReceiptBalanceQuantity = ppqd.QuantityIn > 0 ? ppqd.QuantityIn : (decimal?)null,
                    ReconcillationStatus = (int)Default.reconcileStatus,
                    CreatedOn = transactionDate,
                    CreatedBy = userInfo.UserId,
                    UpdatedOn = transactionDate,
                    UpdatedBy = userInfo.UserId,
                    DocumentType = (int?)DocumentType.inventoryLedgerRecord,
                    DocumentStatus = (int?)DocumentStatus.active,
                    Status = true,
                    BranchId = userInfo.BranchId,
                    CompanyId = userInfo.CompanyId
                }).ToList();

                var AFInventoryLedger = await _repo.UpsertInto_AFInventoryLedger(
                    postedData.OperationType,
                    AFInventoryLedgerInfo.FirstOrDefault()?.RefDocumentType,
                    AFInventoryLedgerInfo,
                    con,
                    sqlTransaction
                );
                if (IAdjustment.response != (int)Code.Created && IAdjustment.response != (int)Code.Accepted)
                {
                    await transaction.RollbackAsync();
                    return ServiceResult.failure(Message.serverResponse((int?)Code.BadRequest), (int)Code.BadRequest);
                }
                #endregion

                #region PORTION FOR :: UPDATE PRICES IF AUTO PRICE UPDATE IS ENABLED

                var adjustmentLines = postedData.PostedDataIAdjustmentPPQD.Where(x => x.ProductId.HasValue && (x.UnitSalePrice > 0 || x.UnitPurchasePrice > 0)).Select(x => new
                {
                    ProductId = x.ProductId!.Value,
                    ProductCombinationId = x.ProductCombinationId
                }).Distinct().ToList();

                if (adjustmentType.IsAutoPriceUpdate == true)
                {
                    var productIds = postedData.PostedDataIAdjustmentPPQD.Where(x => x.ProductId.HasValue).Select(x => x.ProductId!.Value).Distinct().ToList();

                    var existingPriceLogs = await _eRPOSContext.AFProductPriceLog.Where(x => x.ProductId.HasValue && productIds.Contains(x.ProductId.Value)
                                 && x.Status == true && x.DocumentStatus == (int)DocumentStatus.active && x.CompanyId == userInfo.CompanyId && x.BranchId == userInfo.BranchId
                                 && x.TierTypeId == (int)Default.tierTypeId).ToListAsync();

                    var productATIMapping = await _commonServices.get_ActiveATIByParam(productIds);

                    foreach (var item in existingPriceLogs)
                    {
                        bool shouldExpire = adjustmentLines.Any(x =>
                            x.ProductId == item.ProductId &&
                            x.ProductCombinationId == item.ProductCombinationId);

                        if (!shouldExpire)
                            continue;

                        item.DocumentStatus = (int)DocumentStatus.expired;
                        item.UpdatedOn = transactionDate;
                        item.UpdatedBy = userInfo.UserId;
                    }

                    foreach (var item in postedData.PostedDataIAdjustmentPPQD)
                    {
                        bool hasPriceData = (item.UnitSalePrice ) > 0 || (item.UnitPurchasePrice) > 0;
                        if (hasPriceData == false) continue;
                        var priceLog = new AFProductPriceLog
                        {
                            GuID = Guid.NewGuid(),
                            ProductId = item.ProductId,
                            ProductATIId = item.ProductId.HasValue && productATIMapping.TryGetValue(item.ProductId.Value, out var atiId) ? atiId : null,
                            ProductCombinationId = item.ProductCombinationId,
                            TierTypeId = (int)Default.tierTypeId,
                            DefaultSalePrice = item.UnitSalePrice,
                            MinimumSalePrice = item.UnitPurchasePrice,
                            CreatedOn = transactionDate,
                            CreatedBy = userInfo.UserId,
                            DocumentType = (int)DocumentType.productPriceLog,
                            DocumentStatus = (int)DocumentStatus.active,
                            Status = true,
                            BranchId = userInfo.BranchId,
                            CompanyId = userInfo.CompanyId,
                        };
                        _eRPOSContext.AFProductPriceLog.Add(priceLog);
                    }
                    await _eRPOSContext.SaveChangesAsync();
                }
                #endregion

                #region PORTION FOR :: HANDLE TRANSACTION
                switch (IAdjustment.response)
                {
                    case (int)Code.Created:
                    case (int)Code.Accepted:
                        await transaction.CommitAsync();
                        return ServiceResult.success(Message.serverResponse(IAdjustment.response), (int)IAdjustment.response);
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

        #region SOFT DELETE INVENTORY DOCUMENT
        public async Task<ServiceResult> updateDocument_BrandByGuID(Guid? guid, bool? status, int documentStatus = (int)DocumentStatus.active)
        {
            var userInfo = _currentUser;
            if (!userInfo.IsAuthenticated)
                return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);

            try
            {
                var record = _eRPOSContext.IBrand.Where(x => x.GuID == guid).FirstOrDefault();
                if (record == null)
                {
                    return ServiceResult.failure(Message.serverResponse((int?)Code.BadRequest), (int)Code.BadRequest);
                }
                if (status == false)
                {
                    documentStatus = (int)DocumentStatus.deleted;
                }
                record.Status = status;
                record.DocumentStatus = documentStatus;
                await _eRPOSContext.SaveChangesAsync();
                return ServiceResult.success("Brand updated successfully.", (int)Code.OK);
            }
            catch (Exception ex)
            {
                return ServiceResult.failure($"Internal server error: {ex.Message}", (int)Code.InternalServerError);
            }
        }
        public async Task<ServiceResult> updateDocument_CategoryByGuID(Guid? guid, bool? status, int documentStatus = (int)DocumentStatus.active)
        {
            var userInfo = _currentUser;
            if (!userInfo.IsAuthenticated)
                return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);

            try
            {
                var record = _eRPOSContext.ICategory.Where(x => x.GuID == guid).FirstOrDefault();
                if (record == null)
                {
                    return ServiceResult.failure(Message.serverResponse((int?)Code.BadRequest), (int)Code.BadRequest);
                }
                if (status == false)
                {
                    documentStatus = (int)DocumentStatus.deleted;
                }
                record.Status = status;
                record.DocumentStatus = documentStatus;
                await _eRPOSContext.SaveChangesAsync();
                return ServiceResult.success("Category updated successfully.", (int)Code.OK);
            }
            catch (Exception ex)
            {
                return ServiceResult.failure($"Internal server error: {ex.Message}", (int)Code.InternalServerError);
            }
        }
        public async Task<ServiceResult> updateDocument_SubCategoryByGuID(Guid? guid, bool? status, int documentStatus = (int)DocumentStatus.active)
        {
            var userInfo = _currentUser;
            if (!userInfo.IsAuthenticated)
                return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);

            try
            {
                var record = _eRPOSContext.ISubCategory.Where(x => x.GuID == guid).FirstOrDefault();
                if (record == null)
                {
                    return ServiceResult.failure(Message.serverResponse((int?)Code.BadRequest), (int)Code.BadRequest);
                }
                if (status == false)
                {
                    documentStatus = (int)DocumentStatus.deleted;
                }
                record.Status = status;
                record.DocumentStatus = documentStatus;
                await _eRPOSContext.SaveChangesAsync();
                return ServiceResult.success("Sub Category updated successfully.", (int)Code.OK);
            }
            catch (Exception ex)
            {
                return ServiceResult.failure($"Internal server error: {ex.Message}", (int)Code.InternalServerError);
            }
        }
        #endregion

        private async Task iProductCCE_SPR(int refDocumentType, List<IInventoryAdjustmentPPQD_TVP> invADJ_items)
        {
            List<int?>productIds;
            List<IProductCCE> existingCombination;
            List<IProductCCE> newCombination = new List<IProductCCE>(); 

            switch (refDocumentType)
            {
                case (int)DocumentType.inventoryAdjustment:
                     productIds = invADJ_items.Select(x => x.ProductId).Distinct().ToList();
                     existingCombination = await _eRPOSContext.IProductCCE.Where(x => productIds.Contains(x.ProductId)).ToListAsync();
                    foreach(var item in invADJ_items)
                    {
                        var formattedDescription = item.Attribute?.Trim().ToLower();
                        var productCombination = existingCombination.FirstOrDefault(x => x.ProductId == item.ProductId && x.Description?.Trim().ToLower() == formattedDescription) ?? newCombination.FirstOrDefault(x => x.ProductId == item.ProductId && x.Description?.Trim().ToLower() == formattedDescription);
                        if (productCombination == null)
                        {
                            var combination = new IProductCCE
                            {
                                GuID = Guid.NewGuid(),
                                RefDocumentType = refDocumentType,
                                ProductId = item.ProductId,
                                Description = item.Attribute,
                                CreatedOn = DateTime.UtcNow,
                                CreatedBy = _currentUser.UserId,
                                DocumentType = (int)DocumentType.productCombination,
                                DocumentStatus = (int)DocumentStatus.active,
                                Status = true
                            };
                            newCombination.Add(combination);
                            _eRPOSContext.IProductCCE.Add(combination);
                        }
                    }
                    if (newCombination.Any())
                    {
                        await _eRPOSContext.SaveChangesAsync();
                        existingCombination.AddRange(newCombination);
                    }
                    foreach (var item in invADJ_items)
                    {
                        var cleanDesc = item.Attribute?.Trim().ToLower();
                        item.ProductCombinationId = existingCombination.FirstOrDefault(x => x.ProductId == item.ProductId && x.Description?.Trim().ToLower() == cleanDesc).Id;
                    }
                    break;
            }
        }
    }

}