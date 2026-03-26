using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OrganisationSetup.Areas.AccountNfinance.Services;
using OrganisationSetup.Models.DAL;
using OrganisationSetup.Models.DAL.StoredProcedure;
using OrganisationSetup.Services;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using SharedUI.Models.Responses;
using SharedUI.Models.SQLParameters;
using SharedUI.Models.TVP;
using System.Data;
using System.Diagnostics;


namespace OrganisationSetup.Areas.SaleOperation.Services
{
    public interface ISaleOperationUpsert
    {
        Task<ServiceResult> updateInsertDataInto_SOCustomer(PostedData postedData);

    }
    public class SaleOperationUpsertService : ISaleOperationUpsert
    {
        private readonly IOSDataLayer _repo;
        private readonly string _connectionString;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISaleOperationValidation _validationService;
        private readonly ICommon _cService;
        private readonly ERPOrganisationSetupContext _eRPOSContext;
        public SaleOperationUpsertService(IOSDataLayer repo, ERPOrganisationSetupContext context, IHttpContextAccessor httpContextAccessor ,ISaleOperationValidation validationService, ERPOrganisationSetupContext eRPOSC, ICommon cService)
        {
            _repo = repo;
            _connectionString = context.Database.GetDbConnection().ConnectionString;
            _httpContextAccessor = httpContextAccessor;
            _validationService = validationService;
            _cService = cService;
            _eRPOSContext = eRPOSC;
        }

        public async Task<ServiceResult> updateInsertDataInto_SOCustomer(PostedData postedData)
        {
            var userInfo = TempUser.Fill(_httpContextAccessor);

            if (!userInfo.IsAuthenticated)
                return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);

            #region PORTION FOR :: DOCUMENT SETTING ON BASIS OF OperationType
            Guid? customerGuID = Guid.Empty;
            Guid? chartOfAccountGuID = Guid.Empty;
            Guid? invoiceGuID = Guid.Empty;
            Guid? customerLedgerGuID = Guid.Empty;
            Guid? journalVoucherCreditGuID = Guid.Empty;
            Guid? journalVoucherDebitGuID = Guid.Empty;
            if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
                customerGuID = Guid.NewGuid();
                chartOfAccountGuID = Guid.NewGuid();
                invoiceGuID = Guid.NewGuid();
                customerLedgerGuID = Guid.NewGuid();
                journalVoucherCreditGuID = Guid.NewGuid();
                journalVoucherDebitGuID = Guid.NewGuid();
            }
            else
            {
                customerGuID = postedData.GuID;
                chartOfAccountGuID = postedData.GuID;
                invoiceGuID = postedData.GuID;
                customerLedgerGuID = postedData.GuID;
                journalVoucherCreditGuID = postedData.GuID;
                journalVoucherDebitGuID = postedData.GuID;
            }
            bool? isOperationPermitted = await _validationService.isOSCustomerValid(postedData.OperationType, customerGuID, postedData.Description);
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
                                                      postedData.DefaultReceivableAccount?.Trim(),
                                                      (int?)AccountCategory.ACCOUNTS_RECEIVABLE,
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

                    #region PORTION FOR :: UPSERT INTO dbo.SOCustomer
                    var SOCustomer = await _repo.UpsertInto_SOCustomer(
                                                    postedData.OperationType,
                                                    customerGuID,
                                                    postedData.Description?.Trim(),
                                                    postedData.Contact?.Trim(),
                                                    postedData.Email?.Trim(),
                                                    postedData.CNICNumber?.Trim(),
                                                    postedData.Address?.Trim(),
                                                    postedData.AdditionalDetail?.Trim(),
                                                    postedData.ReceivableAccountId = AFChartOfAccount.insertedId,
                                                    postedData.OpeningBalance,
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
                    if(postedData.OpeningBalance > 0)
                    {
                        #region PRE-PARE DOCUMENTS IN CASE OPENING BALANCE > 0

                        #region PORTION FOR :: FILL & UPSERT Invoice
                        string InvoiceDescription = "Opening Balance Till: " + DateTime.Now.ToString("dd-MMM-yyyy") + " . ";
                        List<AFInvoiceProduct_TVP> invoicePI = new List<AFInvoiceProduct_TVP>
                        {
                            new AFInvoiceProduct_TVP
                            {
                                Id = 0,
                                GuID = Guid.NewGuid(),
                                ProductId = 0,
                                InvoiceId = 0,
                                Quantity = 0,
                                ActualAmount = (decimal)postedData.OpeningBalance!,
                                DiscountAmount = 0,
                                ChargedAmount =  (decimal)postedData.OpeningBalance!,
                                CreatedOn = DateTime.Now,
                                CreatedBy = userInfo.UserId,
                                UpdatedOn = DateTime.Now,
                                UpdatedBy = userInfo.UserId,
                                DocumentType = (int?)DocumentType.invoiceProductCustomerOB,
                                DocumentStatus = (int?)DocumentStatus.active,
                                Status = true
                            }
                        };

                        #region PORTION FOR :: UPSERT INTO dbo.AFInvoice
                        var AFInvoice = await _repo.UpsertInto_AFInvoice(
                                                        postedData.OperationType,
                                                        invoiceGuID,
                                                        postedData.LocationId = userInfo.BranchId,
                                                        postedData.TransactionDate = DateTime.Now,
                                                        postedData.CustomerId = SOCustomer.insertedId,
                                                        InvoiceDescription,
                                                        postedData.FBRStamp?.Trim(),
                                                        (int?)InvoiceType.CustomerOpeningBalanceInvoice,
                                                        (int?)PostingStatus.InvoiceStatus.unPaid,
                                                        DateTime.Now,
                                                        userInfo.UserId,
                                                        DateTime.Now,
                                                        userInfo.UserId,
                                                        (int?)DocumentType.invoice,
                                                        (int?)DocumentStatus.active,
                                                        userInfo.BranchId,
                                                        userInfo.CompanyId,
                                                        invoicePI,
                                                        con, transaction);

                        SOCustomer.response = AFInvoice.response;
                        #endregion
                        #endregion

                        #region PORTION FOR :: FILL & UPSERT CustomerLedger
                        string customerLedgerDescription = InvoiceDescription + "Recorded as OB invoice having document control code: " + AFInvoice.documentCode + " .";
                        List<AFCustomerLedger_TVP> customerLedger = new List<AFCustomerLedger_TVP>
                        {
                            new AFCustomerLedger_TVP
                            {
                                Id = 0,
                                GuID = customerLedgerGuID,
                                Code= "",
                                LocationId = userInfo.BranchId,
                                RefDocumentType = (int?)DocumentType.invoice,
                                RefDocumentGuID=invoiceGuID,
                                Description= customerLedgerDescription,
                                Debit= (decimal)postedData.OpeningBalance,
                                Credit =0,
                                PostingStatus= (int?)PostingStatus.LedgerStatus.unreconciled,
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

                        #region PORTION FOR :: UPSERT INTO dbo.AFInvoice
                        var AFCustomerLedger = await _repo.UpsertInto_AFCustomerLedger(
                                                    postedData.OperationType,
                                                    userInfo.CompanyId,
                                                    customerLedger,
                                                    con,transaction);

                        #endregion

                        #endregion

                        #region PORTION FOR :: FILL & UPSERT JournalVoucher
                        var osvChartOfAccountInfo = await _cService.populateOSvChartOfAccountByParam(postedData.OperationType,(int?)FilterConditions.osvChartOfAccount_Operation_ByDefaultSetting,(int?)AccountCategory.EQUITY_RETAINED_EARNINGS);
                        List<AFJournalVoucher_TVP> journalVoucher = new List<AFJournalVoucher_TVP>
                        {
                            new AFJournalVoucher_TVP
                            {
                                Id = 0,
                                GuID = journalVoucherCreditGuID,
                                Code="",
                                LocationId = userInfo.BranchId,
                                RefDocumentType=(int?)DocumentType.customerLedgerRecord,
                                RefDocumentGuID = customerLedgerGuID,
                                Description = "Opening Balance Debit for " + postedData.Description,
                                ChartOfAccountId= AFChartOfAccount.insertedId,
                                Credit= 0,
                                Debit= (decimal)postedData.OpeningBalance,
                                PostingStatus=(int)PostingStatus.VoucherStatus.pending,
                                CreatedOn = DateTime.Now,
                                CreatedBy = userInfo.UserId,
                                UpdatedOn = DateTime.Now,
                                UpdatedBy = userInfo.UserId,
                                DocumentType = (int?)DocumentType.journalVoucher,
                                DocumentStatus = (int?)DocumentStatus.active,
                                Status = true,
                                BranchId= userInfo.BranchId,
                                CompanyId = userInfo.CompanyId
                            },
                            new AFJournalVoucher_TVP
                            {
                                Id = 0,
                                GuID = journalVoucherDebitGuID,
                                Code="",
                                LocationId = userInfo.BranchId,
                                RefDocumentType=(int?)DocumentType.customerLedgerRecord,
                                RefDocumentGuID = customerLedgerGuID,
                                Description = "Equity Offset for " + postedData.Description + " Opening Balance",
                                ChartOfAccountId= (int?)osvChartOfAccountInfo?.FirstOrDefault()?.Id,
                                Credit= (decimal)postedData.OpeningBalance,
                                Debit= 0,
                                PostingStatus=(int)PostingStatus.VoucherStatus.pending,
                                CreatedOn = DateTime.Now,
                                CreatedBy = userInfo.UserId,
                                UpdatedOn = DateTime.Now,
                                UpdatedBy = userInfo.UserId,
                                DocumentType = (int?)DocumentType.journalVoucher,
                                DocumentStatus = (int?)DocumentStatus.active,
                                Status = true,
                                BranchId= userInfo.BranchId,
                                CompanyId = userInfo.CompanyId
                            },
                        };

                        #region PORTION FOR :: UPSERT INTO dbo.AFJournalVoucher
                        var AFJournalVoucher = await _repo.UpsertInto_AFJournalVoucher(
                                                    postedData.OperationType,
                                                    userInfo.CompanyId,
                                                    journalVoucher,
                                                    con, transaction);

                        #endregion
                        #endregion

                        SOCustomer.response = AFJournalVoucher.Value;

                        #endregion

                    }

                    #region PORTION FOR :: HANLDE TRANSACTION

                    switch (SOCustomer.response)
                    {
                        case (int)Code.Created:
                        case (int)Code.Accepted:
                            await transaction.CommitAsync();
                            return ServiceResult.success(Message.serverResponse(SOCustomer.response), (int)SOCustomer.response);
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