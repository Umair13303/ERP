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
        Task<ServiceResult> updateInsertDataInto_AFInvoice(PostedData postedData, List<AFInvoiceProductPricing_TVP> invoicePPI);



    }
    public class AccountNfinanceUpsertService : IAccountNfinanceUpsert
    {
        private readonly IOSDataLayer _repo;
        private readonly string _connectionString;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountNfinanceValidation _validationService;
        private readonly IAccountNfinanceRetriever _retrieverService;
        private readonly TempUser _currentUser;
        public AccountNfinanceUpsertService(TempUser currentUser,IOSDataLayer repo, ERPOrganisationSetupContext context, IHttpContextAccessor httpContextAccessor, IAccountNfinanceValidation validationService, IAccountNfinanceRetriever retrieverService)
        {
            _currentUser = currentUser;
            _repo = repo;
            _connectionString = context.Database.GetDbConnection().ConnectionString;
            _httpContextAccessor = httpContextAccessor;
            _validationService = validationService;
            _retrieverService = retrieverService;
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

        public async Task<ServiceResult> updateInsertDataInto_AFInvoice(PostedData postedData, List<AFInvoiceProductPricing_TVP> invoicePPI)
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
                                                  postedData.DueAmount,
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
    }
}