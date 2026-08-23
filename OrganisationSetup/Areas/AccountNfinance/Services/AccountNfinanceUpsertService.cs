using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OrganisationSetup.Areas.Inventory.Services;
using OrganisationSetup.Models.DAL;
using OrganisationSetup.Models.DAL.StoredProcedure;
using OrganisationSetup.Services;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using SharedUI.Models.Responses;
using SharedUI.Models.SQLParameters;
using SharedUI.Models.TVP;
using System.Configuration;
using System.Diagnostics;
using System.Linq;


namespace OrganisationSetup.Areas.AccountNfinance.Services
{
    public interface IAccountNfinanceUpsert
    {
        Task<ServiceResult> updateInsertDataInto_AFChartOfAccount(PostedData postedData, bool? isCustomerAutoAccount);
        Task<ServiceResult> updateInsertDataInto_AFInvoice(PostedData postedData);
        Task<ServiceResult> updateInsertDataInto_AFInvoiceReceipt(PostedData postedData);
        Task<ServiceResult> updateInsertDataInto_AFBill(PostedData postedData);
        Task<ServiceResult> updateInsertDataInto_AFBillReceipt(PostedData postedData);
    }
    public class AccountNfinanceUpsertService : IAccountNfinanceUpsert
    {
        private readonly IOSDataLayer _repo;
        private readonly string _connectionString;
        private readonly IAccountNfinanceValidation _validationService;
        private readonly IInventoryRetriever _inventoryRetriever;
        private readonly TempUser _currentUser;
        private readonly ERPOrganisationSetupContext _eRPOSContext;
        private readonly ICommon _commonServices;


        public AccountNfinanceUpsertService(TempUser currentUser, IOSDataLayer repo, IInventoryRetriever inventoryRetriever, ERPOrganisationSetupContext eRPOSContext, IHttpContextAccessor httpContextAccessor, IAccountNfinanceValidation validationService, IAccountNfinanceRetriever retrieverService, ICommon commonServices)
        {
            _currentUser = currentUser;
            _repo = repo;
            _eRPOSContext = eRPOSContext;
            _connectionString = _eRPOSContext.Database.GetDbConnection().ConnectionString;
            _validationService = validationService;
            _commonServices = commonServices;
            _inventoryRetriever = inventoryRetriever;
        }
        public async Task<ServiceResult> updateInsertDataInto_AFChartOfAccount(PostedData postedData, bool? isCustomerAutoAccount)
        {
            var userInfo = _currentUser;

            if (!userInfo.IsAuthenticated)
                return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);

            #region PORTION FOR :: DOCUMENT SETTING ON BASIS OF OperationType
            Guid? chartOfAccountGuID = Guid.Empty;
            if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
                chartOfAccountGuID = Guid.NewGuid();
            }
            else
            {
                chartOfAccountGuID = postedData.GuID;
            }
            bool? isOperationPermitted = await _validationService.isAFChartOfAccountValid(postedData.OperationType, chartOfAccountGuID, postedData.Description);
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
                                                      isCustomerAutoAccount == false ? postedData.Description?.Trim() : postedData.DefaultReceivableAccount?.Trim(),
                                                      postedData.AccountCategoryId,
                                                      postedData.FinancialStatementId,
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

                    #region PORTION FOR :: HANLDE TRANSACTION
                    switch (AFChartOfAccount.response)
                    {
                        case (int)Code.Created:
                        case (int)Code.Accepted:
                            await transaction.CommitAsync();
                            return ServiceResult.success(Message.serverResponse(AFChartOfAccount.response), (int)AFChartOfAccount.response);
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
        public async Task<ServiceResult> updateInsertDataInto_AFInvoice(PostedData postedData)
        {
            bool? isWhatsAppMessagingAllowed = await _eRPOSContext.confApplicationRule.Where(x => x.ClientKEY == 1).Select(x => x.IsWhatsAppInvoicing).FirstOrDefaultAsync();
            var userInfo = _currentUser;

            if (!userInfo.IsAuthenticated)
                return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);
            #region PORTION FOR :: DOCUMENT SETTING ON BASIS OF OperationType
            Guid? invoiceGuID = Guid.Empty;
            Guid? customerLedgerGuID = Guid.Empty;

            if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
                invoiceGuID = Guid.NewGuid();
                customerLedgerGuID = Guid.NewGuid();
            }
            else
            {
                invoiceGuID = postedData.GuID;
            }
            var validationResult = await _validationService.isAFInvoiceValid(postedData.OperationType, invoiceGuID, postedData);

            if (!validationResult.IsSuccess)
            {
                return ServiceResult.failure(validationResult.Message, (int)validationResult.StatusCode);
            }
            bool? isOperationPermitted = true;
            #endregion
            if (isOperationPermitted == true)
            {
                using var con = new SqlConnection(_connectionString);
                await con.OpenAsync();
                using var transaction = con.BeginTransaction();
                try
                {

                    #region PORTION FOR :: FILL & UPSERT Invoice
                    decimal invoiceChargedAmount = postedData.PostedDataAFInvoicePPI.Sum(x => x.ChargedAmount);
                    decimal receiptAmount = postedData.ReceiptAmount ?? 0m;
                    decimal dueAmount = Math.Max(0, invoiceChargedAmount - receiptAmount);
                    int computedInvoiceStatus = dueAmount <= 0 ? (int)InvoiceStatus.paid : dueAmount < invoiceChargedAmount ? (int)InvoiceStatus.partialPaid : (int)InvoiceStatus.unPaid;
                    string invoiceStatus = Enum.GetName(typeof(InvoiceStatus), computedInvoiceStatus) ?? "UNKNOWN";
                    var productIds = postedData.PostedDataAFInvoicePPI.Where(x => x.ProductId.HasValue).Select(x => x.ProductId!.Value).Distinct().ToList();
                    var productATIMapping = await _commonServices.get_ActiveATIByParam(productIds);
                    postedData.Description = "POS Direct Invoice Generated, Amounting " + invoiceChargedAmount + " @ " + DateTime.UtcNow;
                    foreach (var i in postedData.PostedDataAFInvoicePPI)
                    {
                        i.GuID = Guid.NewGuid();
                        i.ProductATIId = i.ProductId.HasValue && productATIMapping.TryGetValue(i.ProductId.Value, out var atiId) ? atiId : null;
                        i.DocumentType = (int)DocumentType.invoiceProduct;
                        i.DocumentStatus = (int)DocumentStatus.active;
                        i.Status = true;
                    }
                    #region PORTION FOR :: UPSERT INTO dbo.AFInvoice
                    var AFInvoice = await _repo.UpsertInto_AFInvoice(
                                                  postedData.OperationType,
                                                  invoiceGuID,
                                                  postedData.LocationId,
                                                  postedData.TransactionDate,
                                                  postedData.CustomerId,
                                                  postedData.Description,
                                                  postedData.FBRStamp,
                                                  invoiceChargedAmount,
                                                  postedData.InvoiceTypeId = (int)InvoiceType.POSSale,
                                                  (int?)computedInvoiceStatus,
                                                  DateTime.Now,
                                                  userInfo.UserId,
                                                  DateTime.Now,
                                                  userInfo.UserId,
                                                  (int?)DocumentType.invoice,
                                                  (int?)DocumentStatus.active,
                                                  userInfo.BranchId,
                                                  userInfo.CompanyId,
                                                  postedData.PostedDataAFInvoicePPI,
                                                  con, transaction
                                                  );
                    #endregion
                    #endregion

                    #region PORTION FOR :: FILL & UPSERT CustomerLedger
                    List<AFCustomerLedger_TVP> customerLedger = new List<AFCustomerLedger_TVP>
                    {
                            new AFCustomerLedger_TVP
                            {
                                Id = 0,
                                GuID = customerLedgerGuID,
                                Code= "",
                                LocationId = userInfo.BranchId,
                                TransactionDate= postedData.TransactionDate,
                                CustomerId = postedData.CustomerId,
                                RefDocumentType = (int?)DocumentType.invoice,
                                RefDocumentId=AFInvoice.insertedId,
                                Description= postedData.Description,
                                Debit= (decimal)AFInvoice.totalInvoiceAmount,
                                Credit =0,
                                ReconcillationStatus= (int?)Default.reconcileStatus,
                                CreatedOn = DateTime.Now,
                                CreatedBy = userInfo.UserId,
                                UpdatedOn = DateTime.Now,
                                UpdatedBy = userInfo.UserId,
                                DocumentType = (int?)DocumentType.customerLedgerRecord,
                                DocumentStatus = (int?)DocumentStatus.active,
                                Status = true,
                                BranchId= userInfo.BranchId,
                                CompanyId = userInfo.CompanyId
                            }
                    };

                    #region PORTION FOR :: UPSERT INTO dbo.AFCustomerLedger
                    var AFCustomerLedger = await _repo.UpsertInto_AFCustomerLedger(
                                                postedData.OperationType,
                                                userInfo.CompanyId,
                                                customerLedger,
                                                con, transaction);

                    #endregion
                    #endregion

                    #region PORTION FOR :: IF PAID, GENERATE RECEIPT AND LEDGER CREDIT
                    string receiptDescription = "POS Cash received against Invoice #" + AFInvoice.documentCode;
                    if (receiptAmount > 0)
                    {
                        var receiptResult = await _repo.UpsertInto_AFInvoiceReceipt(
                            postedData.OperationType,
                            Guid.NewGuid(),
                            postedData.LocationId,
                            postedData.TransactionDate,
                            postedData.CustomerId,
                            AFInvoice.insertedId,
                            receiptDescription,
                            (int?)PaymentType.InvoiceWise,
                            postedData.PaymentMethodId ?? 1,
                            postedData.Reference,
                            receiptAmount,
                            (int?)Default.paymentStatus,
                            DateTime.Now,
                            userInfo.UserId,
                            DateTime.Now,
                            userInfo.UserId,
                            (int?)DocumentType.invoiceReceipt,
                            (int?)DocumentStatus.active,
                            userInfo.BranchId,
                            userInfo.CompanyId,
                            con, transaction
                        );

                        List<AFCustomerLedger_TVP> receiptCustomerLedger = new List<AFCustomerLedger_TVP>
                        {
                            new AFCustomerLedger_TVP
                            {
                                Id = 0,
                                GuID = Guid.NewGuid(),
                                Code = "",
                                LocationId = userInfo.BranchId,
                                TransactionDate = postedData.TransactionDate,
                                CustomerId = postedData.CustomerId,
                                RefDocumentType = (int?)DocumentType.invoiceReceipt,
                                RefDocumentId = receiptResult.insertedId,
                                Description = receiptDescription,
                                Debit = 0,
                                Credit = receiptAmount,
                                ReconcillationStatus = (int?)Default.reconcileStatus,
                                CreatedOn = DateTime.Now,
                                CreatedBy = userInfo.UserId,
                                UpdatedOn = DateTime.Now,
                                UpdatedBy = userInfo.UserId,
                                DocumentType = (int?)DocumentType.customerLedgerRecord,
                                DocumentStatus = (int?)DocumentStatus.active,
                                Status = true,
                                BranchId = userInfo.BranchId,
                                CompanyId = userInfo.CompanyId
                            }
                        };

                        await _repo.UpsertInto_AFCustomerLedger(
                            postedData.OperationType,
                            userInfo.CompanyId,
                            receiptCustomerLedger,
                            con, transaction
                        );
                    }
                    #endregion

                    #region PORTION FOR :: FILL & UPSERT InventoryLedger TVP (Stock OUT)
                    var InventoryLedger = new List<AFInventoryLedger_TVP>();
                    var errorMessage = new List<string>();
                    if (postedData.PostedDataAFInvoicePPI != null && postedData.PostedDataAFInvoicePPI.Any())
                    {
                        foreach (var item in postedData.PostedDataAFInvoicePPI)
                        {
                            var costLayerInfo = await _repo.srp_IProductConsumeLayer(item.ProductId, item.ProductCombinationId, item.vCostingModeId.Value, item.Quantity, postedData.LocationId, userInfo.CompanyId, con, transaction);
                            var stockDeficit = costLayerInfo.FirstOrDefault(x => x.IsStockDeficit);
                            decimal invoicePPIDerivedUnitSalePrice = item.Quantity > 0    ? Math.Round(item.ChargedAmount / item.Quantity, 0, MidpointRounding.AwayFromZero): 0;
                            if (stockDeficit != null)
                            {
                                errorMessage.Add($"Product {item.ProductId}: short {stockDeficit.QuantityOut} unit(s).");
                                continue;
                            }
                            Guid? vInvoicePPIGuID = item.GuID;
                            foreach (var layer in costLayerInfo)
                            {
                                InventoryLedger.Add(new AFInventoryLedger_TVP
                                {
                                    GuID = Guid.NewGuid(),
                                    LocationId = postedData.LocationId,
                                    TransactionDate = postedData.TransactionDate,
                                    ProductId = item.ProductId,
                                    ProductCombinationId = item.ProductCombinationId,
                                    RefDocumentType = (int?)DocumentType.invoice,
                                    RefDocumentId = AFInvoice.insertedId,
                                    RefDocumentDetailGuID = vInvoicePPIGuID,
                                    Description = postedData.Description?.Trim(),
                                    QuantityIn = 0,
                                    QuantityOut = layer.QuantityOut,
                                    UnitPurchasePrice = layer.UnitPurchasePrice,
                                    UnitSalePrice = invoicePPIDerivedUnitSalePrice,
                                    Debit = 0,
                                    Credit = layer.QuantityOut * layer.UnitPurchasePrice,
                                    Batch = string.IsNullOrWhiteSpace(layer.Batch) ? null : layer.Batch.Trim(),
                                    ExpiryDate = layer.ExpiryDate,
                                    ConsumedInventoryLedgerId = layer.ConsumedInventoryLedgerId,
                                    ReceiptBalanceQuantity = null,
                                    ReconcillationStatus = (int?)Default.reconcileStatus,
                                    CreatedOn = DateTime.Now,
                                    CreatedBy = userInfo.UserId,
                                    UpdatedOn = DateTime.Now,
                                    UpdatedBy = userInfo.UserId,
                                    DocumentType = (int?)DocumentType.inventoryLedgerRecord,
                                    DocumentStatus = (int?)DocumentStatus.active,
                                    Status = true,
                                    BranchId = userInfo.BranchId,
                                    CompanyId = userInfo.CompanyId
                                });
                            }
                        }
                    }
                    if (errorMessage.Any())
                    {
                        await transaction.RollbackAsync();
                        return ServiceResult.failure(string.Join(" ", errorMessage), (int)Code.BadRequest);
                    }

                    #region PORTION FOR :: UPSERT INTO dbo.AFInventoryLedger (Stock OUT)
                    var AFInventoryLedger = await _repo.UpsertInto_AFInventoryLedger(
                                                    postedData.OperationType,
                                                    (int?)DocumentType.invoice,
                                                    InventoryLedger,
                                                    con, transaction);
                    #endregion
                    #endregion

                    if (isWhatsAppMessagingAllowed == true)
                    {
                        #region PLACE HOLDER :: LATER SAVE DATA IN TABLE -- WILL NEED TO MAKE ANOTHER OPEN API ON BASIS OF KEY GUID TO FETC PENDING INVOICE 
                        #endregion
                    }

                    #region PORTION FOR :: HANLDE TRANSACTION
                    switch (AFInventoryLedger.response)
                    {
                        case (int)Code.Created:
                        case (int)Code.Accepted:
                            await transaction.CommitAsync();
                            return ServiceResult.success(Message.serverResponse(AFInvoice.response), (int)AFInvoice.response, guID: invoiceGuID.ToString());
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
        public async Task<ServiceResult> updateInsertDataInto_AFInvoiceReceipt(PostedData postedData)
        {
            var userInfo = _currentUser;

            if (!userInfo.IsAuthenticated)
                return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);

            #region PORTION FOR :: DOCUMENT SETTING ON BASIS OF OperationType
            Guid? invoiceReceiptGuID = Guid.Empty;
            Guid? customerLedgerGuID = Guid.Empty;
            if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB) || postedData.OperationType == nameof(OperationType.MPO_LIST))
            {
                invoiceReceiptGuID = Guid.NewGuid();
                customerLedgerGuID = Guid.NewGuid();
            }
            else
            {
                invoiceReceiptGuID = postedData.GuID;
                customerLedgerGuID = postedData.GuID;

            }
            bool? isOperationPermitted = true;


            #endregion

            #region PORTION FOR :: VALIDATE INVOICE EXISTS BEFORE SAVING RECEIPT
            AFInvoice? AFInvoice = null;
            if (postedData.PaymentTypeId == (int)PaymentType.InvoiceWise)
            {
                AFInvoice = await _eRPOSContext.AFInvoice
                    .Where(x => x.Id == postedData.InvoiceId && x.Status == true)
                    .FirstOrDefaultAsync();

                if (AFInvoice == null)
                {
                    return ServiceResult.failure(Message.serverResponse((int?)Code.CEM_InvalidInvoice), (int)Code.CEM_InvalidInvoice);
                }
                if ((decimal)postedData.ReceiptAmount > AFInvoice.DueAmount)
                {
                    return ServiceResult.failure(Message.serverResponse((int?)Code.CEM_WrongInvoiceAmount), (int)Code.CEM_WrongInvoiceAmount);
                }
            }
            #endregion


            if (isOperationPermitted == true)
            {
                var con = (SqlConnection)_eRPOSContext.Database.GetDbConnection();
                await con.OpenAsync();
                using var transaction = con.BeginTransaction();
                try
                {
                    await _eRPOSContext.Database.UseTransactionAsync(transaction);

                    #region PORTION FOR :: UPSERT INTO dbo.AFInvoiceReceipt
                    var AFInvoiceReceipt = await _repo.UpsertInto_AFInvoiceReceipt(
                                                      postedData.OperationType,
                                                      invoiceReceiptGuID,
                                                      postedData.LocationId,
                                                      postedData.TransactionDate,
                                                      postedData.CustomerId,
                                                      postedData.InvoiceId,
                                                      postedData.Description,
                                                      postedData.PaymentTypeId,
                                                      postedData.PaymentMethodId,
                                                      postedData.Reference,
                                                      postedData.ReceiptAmount,
                                                      (int?)Default.paymentStatus,
                                                      DateTime.Now,
                                                      userInfo.UserId,
                                                      DateTime.Now,
                                                      userInfo.UserId,
                                                      (int?)DocumentType.invoiceReceipt,
                                                      (int?)DocumentStatus.active,
                                                      userInfo.BranchId,
                                                      userInfo.CompanyId,
                                                      con, transaction);
                    #endregion

                    #region PORTION FOR :: FILL & UPSERT CustomerLedger
                    string? customerLedgerDescription = postedData.Description;
                    List<AFCustomerLedger_TVP> customerLedger = new List<AFCustomerLedger_TVP>
                        {
                            new AFCustomerLedger_TVP
                            {
                                Id = 0,
                                GuID = customerLedgerGuID,
                                Code= "",
                                LocationId = userInfo.BranchId,
                                TransactionDate= postedData.TransactionDate,
                                CustomerId = postedData.CustomerId,
                                RefDocumentType = (int?)DocumentType.invoiceReceipt,
                                RefDocumentId=AFInvoiceReceipt.insertedId,
                                Description= customerLedgerDescription,
                                Debit=0,
                                Credit =(decimal)postedData.ReceiptAmount,
                                ReconcillationStatus= (int?)Default.reconcileStatus,
                                CreatedOn = DateTime.Now,
                                CreatedBy = userInfo.UserId,
                                UpdatedOn = DateTime.Now,
                                UpdatedBy = userInfo.UserId,
                                DocumentType = (int?)DocumentType.customerLedgerRecord,
                                DocumentStatus = (int?)DocumentStatus.active,
                                Status = true,
                                BranchId= userInfo.BranchId,
                                CompanyId = userInfo.CompanyId
                            }
                        };

                    #region PORTION FOR :: UPSERT INTO dbo.AFCustomerLedger
                    var AFCustomerLedger = await _repo.UpsertInto_AFCustomerLedger(
                                                postedData.OperationType,
                                                userInfo.CompanyId,
                                                customerLedger,
                                                con, transaction);

                    #endregion

                    #endregion

                    switch (postedData.PaymentTypeId)
                    {
                        case (int)PaymentType.InvoiceWise:
                            if (AFInvoice != null)
                            {
                                decimal oldDueAmount = AFInvoice.DueAmount;
                                decimal newDueAmount = Math.Max(0m, oldDueAmount - (decimal)postedData.ReceiptAmount);
                                AFInvoice.DueAmount = newDueAmount;

                                if (newDueAmount == 0)
                                {
                                    AFInvoice.InvoiceStatus = (int?)InvoiceStatus.paid;
                                }
                                else if (newDueAmount < oldDueAmount)
                                {
                                    AFInvoice.InvoiceStatus = (int?)InvoiceStatus.partialPaid;
                                }

                                _eRPOSContext.Entry(AFInvoice).Property(x => x.DueAmount).IsModified = true;
                                _eRPOSContext.Entry(AFInvoice).Property(x => x.InvoiceStatus).IsModified = true;
                                await _eRPOSContext.SaveChangesAsync();
                            }
                            break;

                        case (int)PaymentType.CustomerAccount:
                            // INTENTIONALLY LEFT BLANK
                            break;
                    }
                    #region PORTION FOR :: HANDLE TRANSACTION
                    switch (AFInvoiceReceipt.response)
                    {
                        case (int)Code.Created:
                        case (int)Code.Accepted:
                            await transaction.CommitAsync();
                            return ServiceResult.success(Message.serverResponse(AFInvoiceReceipt.response), (int)AFInvoiceReceipt.response);
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
                finally
                {
                    if (con.State == System.Data.ConnectionState.Open)
                        await con.CloseAsync();
                }
            }
            else
            {
                return ServiceResult.failure(Message.serverResponse((int?)Code.Conflict), (int)Code.Conflict);
            }
        }
        public async Task<ServiceResult> updateInsertDataInto_AFBill(PostedData postedData)
        {
            var userInfo = _currentUser;

            if (!userInfo.IsAuthenticated)
                return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);

            #region PORTION FOR :: DOCUMENT SETTING ON BASIS OF OperationType
            Guid? billGuID = Guid.Empty;
            Guid? supplierLedgerGuID = Guid.Empty;

            if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
                billGuID = Guid.NewGuid();
                supplierLedgerGuID = Guid.NewGuid();
            }
            else
            {
                billGuID = postedData.GuID;
                supplierLedgerGuID = postedData.GuID;
            }
            foreach (var item in postedData.PostedDataAFBillPPI)
            {
                item.Attribute = CSharedUtility.attributeKeyBuilder(item.Attribute);
            }
            bool? isOperationPermitted = true; // validation baad mein add karna
            #endregion

            if (isOperationPermitted == true)
            {

                using var con = new SqlConnection(_connectionString);
                await con.OpenAsync();
                using var transaction = con.BeginTransaction();
                try
                {
                    #region PORTION FOR :: FILL & UPSERT Bill
                    await iProductCCE_SPR((int)DocumentType.inventoryAdjustment, postedData.PostedDataAFBillPPI);
                    var productIds = postedData.PostedDataAFBillPPI.Where(x => x.ProductId.HasValue).Select(x => x.ProductId!.Value).Distinct().ToList();
                    var productATIMapping = await _commonServices.get_ActiveATIByParam(productIds);
                    decimal billChargedAmount = postedData.PostedDataAFBillPPI.Sum(x => x.ChargedAmount);


                    foreach (var item in postedData.PostedDataAFBillPPI)
                    {
                        item.GuID = Guid.NewGuid();
                        item.ProductATIId = item.ProductId.HasValue && productATIMapping.TryGetValue(item.ProductId.Value, out var atiId) ? atiId : null;
                        item.DocumentType = (int)DocumentType.billProduct;
                        item.DocumentStatus = (int)DocumentStatus.active;
                        item.Status = true;
                    }

                    #region PORTION FOR :: UPSERT INTO dbo.AFBill
                    var AFBill = await _repo.UpsertInto_AFBill(
                                                    postedData.OperationType,
                                                    billGuID,
                                                    userInfo.BranchId,
                                                    postedData.TransactionDate,
                                                    postedData.SupplierId,
                                                    postedData.Description,
                                                    billChargedAmount,
                                                    (int?)BillType.PurchaseBILLDirect,
                                                    (int?)Default.billStatus,
                                                    DateTime.Now,
                                                    userInfo.UserId,
                                                    DateTime.Now,
                                                    userInfo.UserId,
                                                    (int?)DocumentType.bill,
                                                    (int?)DocumentStatus.active,
                                                    userInfo.BranchId,
                                                    userInfo.CompanyId,
                                                    postedData.PostedDataAFBillPPI,
                                                    con, transaction);
                    #endregion
                    #endregion

                    #region PORTION FOR :: FILL & UPSERT SupplierLedger

                    List<AFSupplierLedger_TVP> supplierLedger = new List<AFSupplierLedger_TVP>
                    {
                            new AFSupplierLedger_TVP{
                            Id = 0,
                            GuID = supplierLedgerGuID,
                            Code = "",
                            LocationId = userInfo.BranchId,
                            TransactionDate = postedData.TransactionDate,
                            SupplierId = postedData.SupplierId,
                            RefDocumentType = (int?)DocumentType.bill,
                            RefDocumentId = AFBill.insertedId,
                            Description = postedData.Description,
                            Debit = (decimal)AFBill.totalBillAmount,
                            Credit = 0,
                            ReconcillationStatus = (int?)Default.reconcileStatus,
                            CreatedOn = DateTime.Now,
                            CreatedBy = userInfo.UserId,
                            UpdatedOn = DateTime.Now,
                            UpdatedBy = userInfo.UserId,
                            DocumentType = (int?)DocumentType.supplierLedgerRecord,
                            DocumentStatus = (int?)DocumentStatus.active,
                            Status = true,
                            BranchId = userInfo.BranchId,
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

                    #region PORTION FOR :: PREPARE InventoryLedger TVP (Stock IN)
                    var InventoryLedger = new List<AFInventoryLedger_TVP>();

                    if (postedData.PostedDataAFBillPPI != null && postedData.PostedDataAFBillPPI.Any())
                    {
                        foreach (var item in postedData.PostedDataAFBillPPI)
                        {
                            decimal billPPIDerivedUnitPurchasePrice = item.Quantity > 0 ? Math.Round(item.ChargedAmount / item.Quantity, 0, MidpointRounding.AwayFromZero) : 0;
                            Guid? vBillPPIGuID = item.GuID;
                            InventoryLedger.Add(new AFInventoryLedger_TVP
                            {
                                GuID = Guid.NewGuid(),
                                LocationId = postedData.LocationId,
                                TransactionDate = postedData.TransactionDate,
                                ProductId = item.ProductId,
                                ProductCombinationId = item.ProductCombinationId,
                                RefDocumentType = (int?)DocumentType.bill,
                                RefDocumentId = AFBill.insertedId,
                                RefDocumentDetailGuID = vBillPPIGuID,
                                Description = postedData.Description?.Trim(),
                                QuantityIn = item.Quantity,
                                QuantityOut = 0,
                                UnitPurchasePrice = billPPIDerivedUnitPurchasePrice,
                                UnitSalePrice = 0,
                                Debit = item.ChargedAmount,
                                Credit = 0,
                                Batch = item.Batch,
                                ExpiryDate = item.ExpiryDate,
                                ReceiptBalanceQuantity = item.Quantity,
                                ReconcillationStatus = (int)Default.reconcileStatus,
                                CreatedOn = DateTime.Now,
                                CreatedBy = userInfo.UserId,
                                UpdatedOn = DateTime.Now,
                                UpdatedBy = userInfo.UserId,
                                DocumentType = (int?)DocumentType.inventoryLedgerRecord,
                                DocumentStatus = (int?)DocumentStatus.active,
                                Status = true,
                                BranchId = userInfo.BranchId,
                                CompanyId = userInfo.CompanyId
                            });
                        }
                    }
                    #endregion

                    #region PORTION FOR :: UPSERT INTO dbo.AFInventoryLedger (Stock IN)
                    var AFInventoryLedger = await _repo.UpsertInto_AFInventoryLedger(
                                                    postedData.OperationType,
                                                    (int?)DocumentType.bill,
                                                    InventoryLedger,
                                                    con, transaction);
                    #endregion

                    #region PORTION FOR :: HANDLE TRANSACTION
                    switch (AFInventoryLedger.response)
                    {
                        case (int)Code.Created:
                        case (int)Code.Accepted:
                            await transaction.CommitAsync();
                            return ServiceResult.internalSuccess(Message.serverResponse(AFInventoryLedger.response), (int)AFInventoryLedger.response, AFInventoryLedger.insertedIn);
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
        public async Task<ServiceResult> updateInsertDataInto_AFBillReceipt(PostedData postedData)
        {
            var userInfo = _currentUser;

            if (!userInfo.IsAuthenticated)
                return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);

            #region PORTION FOR :: DOCUMENT SETTING ON BASIS OF OperationType
            Guid? billReceiptGuID = Guid.Empty;
            Guid? supplierLedgerGuID = Guid.Empty;
            if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB) || postedData.OperationType == nameof(OperationType.MPO_LIST))
            {
                billReceiptGuID = Guid.NewGuid();
                supplierLedgerGuID = Guid.NewGuid();
            }
            else
            {
                billReceiptGuID = postedData.GuID;
                supplierLedgerGuID = postedData.GuID;
            }
            bool? isOperationPermitted = true;
            //bool? isOperationPermitted = await _validationService.isAFPaymentReceiptValid(postedData.OperationType, paymentReceiptGuID);
            #endregion

            #region PORTION FOR :: VALIDATE BILL EXISTS BEFORE SAVING PAYMENT
            AFBill? AFBill = null;
            if (postedData.PaymentTypeId == (int)PaymentType.BillWise)
            {
                AFBill = await _eRPOSContext.AFBill
                    .Where(x => x.Id == postedData.BillId && x.Status == true)
                    .FirstOrDefaultAsync();

                if (AFBill == null)
                {
                    return ServiceResult.failure(Message.serverResponse((int?)Code.CEM_InvalidBill), (int)Code.CEM_InvalidBill);
                }
                if ((decimal)postedData.ReceiptAmount > AFBill.DueAmount)
                {
                    return ServiceResult.failure(Message.serverResponse((int?)Code.CEM_WrongBillAmount), (int)Code.CEM_WrongBillAmount);
                }
            }
            #endregion


            if (isOperationPermitted == true)
            {
                var con = (SqlConnection)_eRPOSContext.Database.GetDbConnection();
                await con.OpenAsync();
                using var transaction = con.BeginTransaction();
                try
                {
                    await _eRPOSContext.Database.UseTransactionAsync(transaction);
                    DateTime? transactionDate = postedData.TransactionDate;

                    #region PORTION FOR :: UPSERT INTO dbo.AFBillReceipt
                    var AFBillReceipt = await _repo.UpsertInto_AFBillReceipt(
                                                      postedData.OperationType,
                                                      billReceiptGuID,
                                                      postedData.LocationId,
                                                      transactionDate,
                                                      postedData.SupplierId,
                                                      postedData.BillId,
                                                      postedData.Description,
                                                      postedData.PaymentTypeId,
                                                      postedData.PaymentMethodId,
                                                      postedData.Reference,
                                                      postedData.ReceiptAmount,
                                                      (int?)Default.paymentStatus,
                                                      DateTime.Now,
                                                      userInfo.UserId,
                                                      DateTime.Now,
                                                      userInfo.UserId,
                                                      (int?)DocumentType.billReceipt,
                                                      (int?)DocumentStatus.active,
                                                      userInfo.BranchId,
                                                      userInfo.CompanyId,
                                                      con, transaction);
                    #endregion

                    #region PORTION FOR :: FILL & UPSERT SupplierLedger
                    string supplierLedgerDescription = postedData!.Description;
                    List<AFSupplierLedger_TVP> supplierLedger = new List<AFSupplierLedger_TVP>
                        {
                            new AFSupplierLedger_TVP
                            {
                                Id = 0,
                                GuID = supplierLedgerGuID,
                                Code= "",
                                LocationId = postedData.LocationId,
                                TransactionDate= transactionDate,
                                SupplierId = postedData.SupplierId,
                                RefDocumentType = (int?)DocumentType.billReceipt,
                                RefDocumentId=AFBillReceipt.insertedId,
                                Description= supplierLedgerDescription,
                                Debit= 0,
                                Credit =(decimal)postedData.ReceiptAmount,
                                ReconcillationStatus= (int?)Default.reconcileStatus,
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

                    switch (postedData.PaymentTypeId)
                    {
                        case (int)PaymentType.BillWise:
                            if (AFBill != null)
                            {
                                decimal oldDueAmount = AFBill.DueAmount ?? 0m;
                                decimal newDueAmount = Math.Max(0m, oldDueAmount - (decimal)postedData.ReceiptAmount);
                                AFBill.DueAmount = newDueAmount;

                                if (newDueAmount == 0)
                                    AFBill.BillStatus = (int?)InvoiceStatus.paid;
                                else if (newDueAmount < oldDueAmount)
                                    AFBill.BillStatus = (int?)InvoiceStatus.partialPaid;

                                AFBill.UpdatedBy = userInfo.UserId;
                                AFBill.UpdatedOn = DateTime.Now;

                                _eRPOSContext.Entry(AFBill).Property(x => x.DueAmount).IsModified = true;
                                _eRPOSContext.Entry(AFBill).Property(x => x.BillStatus).IsModified = true;
                                _eRPOSContext.Entry(AFBill).Property(x => x.UpdatedBy).IsModified = true;
                                _eRPOSContext.Entry(AFBill).Property(x => x.UpdatedOn).IsModified = true;

                                await _eRPOSContext.SaveChangesAsync();
                            }
                            break;

                        case (int)PaymentType.SupplierAccount:
                            break;
                    }
                    #region PORTION FOR :: HANDLE TRANSACTION
                    switch (AFBillReceipt.response)
                    {
                        case (int)Code.Created:
                        case (int)Code.Accepted:
                            await transaction.CommitAsync();
                            return ServiceResult.success(Message.serverResponse(AFBillReceipt.response), (int)AFBillReceipt.response);
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
                finally
                {
                    if (con.State == System.Data.ConnectionState.Open)
                        await con.CloseAsync();
                }
            }
            else
            {
                return ServiceResult.failure(Message.serverResponse((int?)Code.Conflict), (int)Code.Conflict);
            }
        }
        private async Task iProductCCE_SPR(int refDocumentType, List<AFBillPPI_TVP> afBill_items)
        {
            List<IProductCCE> existingCombination;
            List<IProductCCE> newCombination = new List<IProductCCE>();
            switch (refDocumentType)
            {
                case (int)DocumentType.inventoryAdjustment:
                    var productIds = afBill_items.Select(x => x.ProductId).Distinct().ToList();
                    var attributedProductIds = await _eRPOSContext.IProduct.Where(x => productIds.Contains(x.Id) && !string.IsNullOrWhiteSpace(x.AttributeIds)).Select(x => x.Id).ToListAsync();

                    foreach (var item in afBill_items.Where(x => !attributedProductIds.Contains(x.ProductId ?? 0)))
                    {
                        item.ProductCombinationId = null;
                    }
                    var attributedItems = afBill_items.Where(x => attributedProductIds.Contains(x.ProductId ?? 0)).ToList();
                    if(attributedItems.Count == 0)
                    {
                        break;
                    }

                    existingCombination = await _eRPOSContext.IProductCCE.Where(x => attributedProductIds.Contains(x.ProductId ?? 0)).ToListAsync();
                    foreach (var item in attributedItems)
                    {
                        var formattedDescription = item.Attribute?.Trim().ToLower();
                        var productCombination = existingCombination.FirstOrDefault(x => x.ProductId == item.ProductId && x.Description?.Trim().ToLower() == formattedDescription)
                                               ?? newCombination.FirstOrDefault(x => x.ProductId == item.ProductId && x.Description?.Trim().ToLower() == formattedDescription);
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
                    foreach (var item in attributedItems)
                    {
                        var cleanDesc = item.Attribute?.Trim().ToLower();
                        item.ProductCombinationId = existingCombination
                            .FirstOrDefault(x => x.ProductId == item.ProductId && x.Description?.Trim().ToLower() == cleanDesc)?.Id;

                        if (item.ProductCombinationId == null)
                            throw new InvalidOperationException($"Failed to resolve product combination for ProductId {item.ProductId}.");
                    }
                    break;
            }
        }
    }
}