using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OrganisationSetup.Models.DAL;
using OrganisationSetup.Models.DAL.StoredProcedure;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using SharedUI.Models.SQLParameters;
using System.Diagnostics;
using SharedUI.Models.Responses;
using System.Configuration;
using SharedUI.Models.TVP;


namespace OrganisationSetup.Areas.AccountNfinance.Services
{
    public interface IAccountNfinanceUpsert
    {
        Task<ServiceResult> updateInsertDataInto_AFChartOfAccount(PostedData postedData, bool? isCustomerAutoAccount);
        Task<ServiceResult> updateInsertDataInto_AFInvoice(PostedData postedData, List<AFInvoicePPI_TVP> invoicePPI);
        Task<ServiceResult> updateInsertDataInto_AFInvoiceReceipt(PostedData postedData);
        Task<ServiceResult> updateInsertDataInto_AFBill(PostedData postedData);
        Task<ServiceResult> updateInsertDataInto_AFBillReceipt(PostedData postedData);
    }
    public class AccountNfinanceUpsertService : IAccountNfinanceUpsert
    {
        private readonly IOSDataLayer _repo;
        private readonly string _connectionString;
        private readonly IAccountNfinanceValidation _validationService;
        private readonly TempUser _currentUser;
        private readonly ERPOrganisationSetupContext _eRPOSContext;

        public AccountNfinanceUpsertService(TempUser currentUser,IOSDataLayer repo, ERPOrganisationSetupContext eRPOSContext, IHttpContextAccessor httpContextAccessor, IAccountNfinanceValidation validationService, IAccountNfinanceRetriever retrieverService)
        {
            _currentUser = currentUser;
            _repo = repo;
            _eRPOSContext = eRPOSContext;
            _connectionString = _eRPOSContext.Database.GetDbConnection().ConnectionString;
            _validationService = validationService;
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
        public async Task<ServiceResult> updateInsertDataInto_AFInvoice(PostedData postedData, List<AFInvoicePPI_TVP> invoicePPI)
        {
            var userInfo = _currentUser;

            if (!userInfo.IsAuthenticated)
                return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);
            #region PORTION FOR :: DOCUMENT SETTING ON BASIS OF OperationType
            Guid? invoiceGuID = Guid.Empty;
            if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
                invoiceGuID = Guid.NewGuid();
            }
            else
            {
                invoiceGuID = postedData.GuID;
            }
            bool? isOperationPermitted = await _validationService.isAFInvoiceValid(postedData.OperationType, invoiceGuID, postedData.Description);
            #endregion
            if (isOperationPermitted == true)
            {
                using var con = new SqlConnection(_connectionString);
                await con.OpenAsync();
                using var transaction = con.BeginTransaction();
                try
                {
                    #region PORTION FOR :: UPSERT INTO dbo.AFInvoice
                    var AFInvoice = await _repo.UpsertInto_AFInvoice(
                                                  postedData.OperationType,
                                                  invoiceGuID,
                                                  postedData.LocationId,
                                                  postedData.TransactionDate,
                                                  postedData.CustomerId,
                                                  postedData.Description,
                                                  postedData.FBRStamp,
                                                  (decimal)postedData.DueAmount,
                                                  postedData.InvoiceTypeId,
                                                  postedData.InvoiceStatus,
                                                  DateTime.Now,
                                                  userInfo.UserId,
                                                  DateTime.Now,
                                                  userInfo.UserId,
                                                  (int?)DocumentType.invoice,
                                                  (int?)DocumentStatus.active,
                                                  userInfo.BranchId,
                                                  userInfo.CompanyId,
                                                  invoicePPI,
                                                  con, transaction
                                                  );
                    #endregion

                    #region PORTION FOR :: HANLDE TRANSACTION
                    switch (AFInvoice.response)
                    {
                        case (int)Code.Created:
                        case (int)Code.Accepted:
                            await transaction.CommitAsync();
                            return ServiceResult.success(Message.serverResponse(AFInvoice.response), (int)AFInvoice.response);
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
            }
            bool? isOperationPermitted = true;
            //bool? isOperationPermitted = await _validationService.isAFPaymentReceiptValid(postedData.OperationType, paymentReceiptGuID);
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
                                                      (int?)PaymentStatus.verified,
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
                                ReconcillationStatus= (int?)ReconcileStatus.reconciled,
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
                            #region PORTION FOR :: UPDATE OUTSTANDING DUE AMOUNT ON dbo.AFInvoice

                            var AFInvoice = await _eRPOSContext.AFInvoice
                                                               .Where(x => x.Id == postedData.InvoiceId && x.Status == true)
                                                               .FirstOrDefaultAsync();
                            if (AFInvoice != null)
                            {
                                AFInvoice.DueAmount = (decimal)AFInvoice.DueAmount - (decimal)postedData.ReceiptAmount;
                                AFInvoice.DueAmount = AFInvoice.DueAmount < 0 ? 0 : AFInvoice.DueAmount;
                                if (postedData.ReceiptAmount < AFInvoice.DueAmount)
                                {
                                    AFInvoice.InvoiceStatus = (int?)InvoiceStatus.partialPaid;
                                }
                                else if (postedData.ReceiptAmount == AFInvoice.DueAmount)
                                {
                                    AFInvoice.InvoiceStatus = (int?)InvoiceStatus.paid;
                                }
                                _eRPOSContext.Entry(AFInvoice).Property(x => x.DueAmount).IsModified = true;
                                await _eRPOSContext.SaveChangesAsync();
                            }
                            else
                            {
                                AFInvoiceReceipt.response = (int)Code.NotFound;
                            }
                            #endregion
                            break;
                        case (int)PaymentType.CustomerAccount:
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
                    var billPPI = new List<AFBillPPI_TVP>();

                    foreach (var item in postedData.PostedDataAFBillPPI)
                    {
                        billPPI.Add(new AFBillPPI_TVP
                        {
                            Id = 0,
                            GuID = Guid.NewGuid(),
                            BillId = 0,
                            ProductId = 0,
                            Attribute = item.Attribute,
                            Quantity = item.Quantity,
                            UnitPurchasePrice = (decimal)item.UnitPurchasePrice,
                            ActualAmount = (decimal)item.ActualAmount,
                            DiscountAmount = (decimal)item.DiscountAmount,
                            ChargedAmount = (decimal)item.ChargedAmount!,
                            Batch = item.Batch,
                            ExpiryDate = item.ExpiryDate!,
                            CreatedOn = DateTime.Now,
                            CreatedBy = userInfo.UserId,
                            UpdatedOn = DateTime.Now,
                            UpdatedBy = userInfo.UserId,
                            DocumentType = (int?)DocumentType.billProduct,
                            DocumentStatus = (int?)DocumentStatus.active,
                            Status = true
                        });
                    }
                    ;

                    #region PORTION FOR :: UPSERT INTO dbo.AFBill
                    var AFBill = await _repo.UpsertInto_AFBill(
                                                    postedData.OperationType,
                                                    billGuID,
                                                    userInfo.BranchId,
                                                    postedData.TransactionDate,
                                                    postedData.SupplierId,
                                                    postedData.Description,
                                                    billPPI.Sum(x => x.ChargedAmount),
                                                    (int?)BillType.PurchaseBILLDirect,
                                                    (int?)BillStatus.unPaid,
                                                    DateTime.Now,
                                                    userInfo.UserId,
                                                    DateTime.Now,
                                                    userInfo.UserId,
                                                    (int?)DocumentType.bill,
                                                    (int?)DocumentStatus.active,
                                                    userInfo.BranchId,
                                                    userInfo.CompanyId,
                                                    billPPI,
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
                            ReconcillationStatus = (int?)ReconcileStatus.reconciled,
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
                            InventoryLedger.Add(new AFInventoryLedger_TVP
                            {
                                GuID = Guid.NewGuid(),
                                LocationId = postedData.LocationId,
                                TransactionDate = postedData.TransactionDate,
                                ProductId = item.ProductId,
                                Attribute = item.Attribute,
                                RefDocumentType = (int?)DocumentType.bill,
                                RefDocumentId = AFBill.insertedId,
                                Description = postedData.Description?.Trim(),
                                QuantityIn = item.Quantity,
                                QuantityOut = 0,
                                UnitPurchasePrice = item.UnitPurchasePrice,
                                UnitSalePrice = 0,
                                Debit = 0,
                                Credit = item.ActualAmount,
                                Batch = item.Batch,
                                ExpiryDate = item.ExpiryDate,
                                ReconcillationStatus = (int)ReconcileStatus.reconciled,
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
            }
            bool? isOperationPermitted = true;
            //bool? isOperationPermitted = await _validationService.isAFPaymentReceiptValid(postedData.OperationType, paymentReceiptGuID);
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
                                                      (int?)PaymentStatus.verified,
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

                    switch (postedData.PaymentTypeId)
                    {
                        case (int)PaymentType.BillWise:
                            #region PORTION FOR :: UPDATE OUTSTANDING DUE AMOUNT ON dbo.AFBill

                            var AFBill = await _eRPOSContext.AFBill
                                                               .Where(x => x.Id == postedData.BillId && x.Status == true)
                                                               .FirstOrDefaultAsync();
                            if (AFBill != null)
                            {
                                AFBill.DueAmount = (decimal)AFBill.DueAmount - (decimal)postedData.ReceiptAmount;
                                AFBill.DueAmount = AFBill.DueAmount < 0 ? 0 : AFBill.DueAmount;
                                if (postedData.ReceiptAmount < AFBill.DueAmount)
                                {
                                    AFBill.BillStatus = (int?)InvoiceStatus.partialPaid;
                                }
                                else if (postedData.ReceiptAmount == AFBill.DueAmount)
                                {
                                    AFBill.BillStatus = (int?)InvoiceStatus.paid;
                                }
                                _eRPOSContext.Entry(AFBill).Property(x => x.DueAmount).IsModified = true;
                                await _eRPOSContext.SaveChangesAsync();
                            }
                            else
                            {
                                AFBillReceipt.response = (int)Code.NotFound;
                            }
                            #endregion
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


    }
}