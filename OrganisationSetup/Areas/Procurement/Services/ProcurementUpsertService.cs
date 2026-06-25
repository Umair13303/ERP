using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
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
using System.Transactions;


namespace OrganisationSetup.Areas.Procurement.Services
{
    public interface IProcurementUpsert
    {
        Task<ServiceResult> updateInsertDataInto_PSupplier(PostedData postedData);

    }
    public class ProcurementUpsertService : IProcurementUpsert
    {
        private readonly TempUser _currentUser;
        private readonly IOSDataLayer _repo;
        private readonly string _connectionString;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProcurementValidation _validationService;
        private readonly ERPOrganisationSetupContext _eRPOSContext;
        public ProcurementUpsertService(TempUser currentUser, IOSDataLayer repo, ERPOrganisationSetupContext context, IHttpContextAccessor httpContextAccessor , IProcurementValidation validationService, ERPOrganisationSetupContext eRPOSC)
        {
            _currentUser = currentUser;
            _repo = repo;
            _connectionString = context.Database.GetDbConnection().ConnectionString;
            _httpContextAccessor = httpContextAccessor;
            _validationService = validationService;
            _eRPOSContext = eRPOSC;
        }
        
        public async Task<ServiceResult> updateInsertDataInto_PSupplier(PostedData postedData)
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
                                                      (int?)FinancialStatement.BALANCE_SHEET,
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
                    var ISupplier = await _repo.UpsertInto_PSupplier(
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
                                UnitPurchasePrice = 0,
                                ActualAmount = (decimal)postedData.OpeningBalance!,
                                DiscountAmount = 0,
                                ChargedAmount =  (decimal)postedData.OpeningBalance!,
                                Batch =  null,
                                ExpiryDate =  null,
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
                                Debit= 0,
                                Credit =(decimal)AFBill.totalBillAmount,
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
    }
}