using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OrganisationSetup.Models.DAL;
using OrganisationSetup.Models.DAL.StoredProcedure;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using SharedUI.Models.SQLParameters;
using System.Diagnostics;
using SharedUI.Models.Responses;


namespace OrganisationSetup.Areas.ApplicationConfiguration.Services
{
    public interface IApplicationConfigurationUpsert
    {
        Task<ServiceResult> updateInsertDataInto_ACCompany(PostedData postedData);
        Task<ServiceResult> updateInsertDataInto_ACBranch(PostedData postedData);
        Task<ServiceResult> updateInsertDataInto_ACUser(PostedData postedData);
        

    }
    public class ApplicationConfigurationUpsertService : IApplicationConfigurationUpsert
    {
        private readonly TempUser _currentUser;
        private readonly IOSDataLayer _repo;
        private readonly string _connectionString;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IApplicationConfigurationValidation _validationService;
        public ApplicationConfigurationUpsertService(TempUser currentUser,IOSDataLayer repo, ERPOrganisationSetupContext context, IHttpContextAccessor httpContextAccessor ,IApplicationConfigurationValidation validationService)
        {
            _currentUser=  currentUser;
            _repo = repo;
            _connectionString = context.Database.GetDbConnection().ConnectionString;
            _httpContextAccessor = httpContextAccessor;
            _validationService = validationService;
        }
        public async Task<ServiceResult> updateInsertDataInto_ACCompany(PostedData postedData)
        {
            var userInfo = _currentUser;

            if (!userInfo.IsAuthenticated)
                return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);

            #region PORTION FOR :: DOCUMENT SETTING ON BASIS OF OperationType
            Guid? companyGuID = Guid.Empty;
            if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
               companyGuID = Guid.NewGuid();
            }
            else
            {
               companyGuID = postedData.GuID;
            }
            bool? isOperationPermitted = await _validationService.isACCompanyValid(postedData.OperationType, companyGuID, postedData.Description);
            #endregion

            if (isOperationPermitted == true)
            {
                using var con = new SqlConnection(_connectionString);
                await con.OpenAsync();
                using var transaction = con.BeginTransaction();
                try
                {
                    #region PORTION FOR :: UPSERT INTO dbo.ACCompany
                    var ACCompany = await _repo.UpsertInto_ACCompany(
                                             postedData.OperationType,
                                             companyGuID,
                                             postedData.Description?.Trim(),
                                             postedData.CountryId,
                                             postedData.CityId,
                                             postedData.Contact?.Trim(),
                                             postedData.Email?.Trim(),
                                             postedData.Address?.Trim(),
                                             postedData.Website?.Trim(),
                                             null,
                                             DateTime.Now,
                                             userInfo.UserId,
                                             DateTime.Now,
                                             userInfo.UserId,
                                             (int?)DocumentType.company,
                                             (int?)DocumentStatus.active,
                                             userInfo.BranchId,
                                             userInfo.CompanyId,
                                             con, transaction);

                    #endregion


                    #region PORTION FOR :: HANLDE TRANSACTION
                    switch (ACCompany.Value)
                    {
                        case (int)Code.Created:
                        case (int)Code.Accepted:
                            await transaction.CommitAsync();
                            return ServiceResult.success(Message.serverResponse(ACCompany.Value), (int)ACCompany.Value);
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
        public async Task<ServiceResult> updateInsertDataInto_ACBranch(PostedData postedData)
        {
            var userInfo = _currentUser;

            if (!userInfo.IsAuthenticated)
                return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);

            #region PORTION FOR :: DOCUMENT SETTING ON BASIS OF OperationType
            Guid? branchGuID = Guid.Empty;
            if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
                branchGuID = Guid.NewGuid();
            }
            else
            {
                branchGuID = postedData.GuID;
            }
            bool? isOperationPermitted = await _validationService.isACBranchValid(postedData.OperationType, branchGuID, postedData.Description);
            #endregion

            if (isOperationPermitted == true)
            {
                using var con = new SqlConnection(_connectionString);
                await con.OpenAsync();
                using var transaction = con.BeginTransaction();
                try
                {
                    #region PORTION FOR :: UPSERT INTO dbo.ACBranch
                    var ACBranch = await _repo.UpsertInto_ACBranch(
                                                            postedData.OperationType,
                                                            branchGuID,
                                                            postedData.Description?.Trim(),
                                                            postedData.OrganisationTypeId,
                                                            postedData.CountryId,
                                                            postedData.CityId,
                                                            postedData.Contact?.Trim(),
                                                            postedData.Email?.Trim(),
                                                            postedData.Address?.Trim(),
                                                            postedData.NTNNumber?.Trim(),
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            (int?)DocumentType.branch,
                                                            (int?)DocumentStatus.active,
                                                            userInfo.BranchId,
                                                            userInfo.CompanyId,
                                                            con, transaction);
                    #endregion

                    #region PORTION FOR :: HANLDE TRANSACTION
                    switch (ACBranch.Value)
                    {
                        case (int)Code.Created:
                        case (int)Code.Accepted:
                            await transaction.CommitAsync();
                            return ServiceResult.success(Message.serverResponse(ACBranch.Value), (int)ACBranch.Value);
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
        public async Task<ServiceResult> updateInsertDataInto_ACUser(PostedData postedData)
        {
            var userInfo = _currentUser;

            if (!userInfo.IsAuthenticated)
                return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);

            #region PORTION FOR :: DOCUMENT SETTING ON BASIS OF OperationType
            Guid? userGuID = Guid.Empty;
            if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
                userGuID = Guid.NewGuid();
            }
            else
            {
                userGuID = postedData.GuID;
            }
            bool? isOperationPermitted = await _validationService.isACUserValid(postedData.OperationType, userGuID, postedData.Description);
            #endregion
            
            if (isOperationPermitted == true)
            {
                using var con = new SqlConnection(_connectionString);
                await con.OpenAsync();
                using var transaction = con.BeginTransaction();
                try
                {
                    #region PORTION FOR :: UPSERT INTO dbo.ACUser
                    var ACUser = await _repo.UpsertInto_ACUser(
                                                            postedData.OperationType,
                                                            userGuID,
                                                            postedData.Description?.Trim(),
                                                            postedData.Password?.Trim(),
                                                            postedData.Contact?.Trim(),
                                                            postedData.Email?.Trim(),
                                                            postedData.EmployeeId,
                                                            postedData.RoleId,
                                                            postedData.AllowedBranchIds?.Trim(),
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            (int?)DocumentType.user,
                                                            (int?)DocumentStatus.active,
                                                            postedData.BranchId,
                                                            postedData.CompanyId,
                                                            con, transaction);
                    #endregion

                    #region PORTION FOR :: HANLDE TRANSACTION
                    switch (ACUser.Value)
                    {
                        case (int)Code.Created:
                        case (int)Code.Accepted:
                            await transaction.CommitAsync();
                            return ServiceResult.success(Message.serverResponse(ACUser.Value), (int)ACUser.Value);
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