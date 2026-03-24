using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OrganisationSetup.Models.DAL;
using OrganisationSetup.Models.DAL.StoredProcedure;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using SharedUI.Models.SQLParameters;
using System.Diagnostics;
using SharedUI.Models.Responses;


namespace OrganisationSetup.Areas.CompanySetup.Services
{
    public interface ICompanySetupUpsert
    {
        Task<ServiceResult> updateInsertDataInto_CSDepartment(PostedData postedData);
    }
    public class CompanySetupUpsertService : ICompanySetupUpsert
    {
        private readonly IOSDataLayer _repo;
        private readonly string _connectionString;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICompanySetupValidation _validationService;
        public CompanySetupUpsertService(IOSDataLayer repo, ERPOrganisationSetupContext context, IHttpContextAccessor httpContextAccessor ,ICompanySetupValidation validationService)
        {
            _repo = repo;
            _connectionString = context.Database.GetDbConnection().ConnectionString;
            _httpContextAccessor = httpContextAccessor;
            _validationService = validationService;
        }
        public async Task<ServiceResult> updateInsertDataInto_CSDepartment(PostedData postedData)
        {
            var userInfo = TempUser.Fill(_httpContextAccessor);

            if (!userInfo.IsAuthenticated)
                return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);

            #region PORTION FOR :: DOCUMENT SETTING ON BASIS OF OperationType
            Guid? departmentGuID = Guid.Empty;
            if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
                departmentGuID = Guid.NewGuid();
            }
            else
            {
                departmentGuID = postedData.GuID;
            }
            bool? isOperationPermitted = await _validationService.isCSDepartmentValid(postedData.OperationType, departmentGuID, postedData.Description);
            #endregion

            if (isOperationPermitted == true)
            {
                using var con = new SqlConnection(_connectionString);
                await con.OpenAsync();
                using var transaction = con.BeginTransaction();
                try
                {
                    #region PORTION FOR :: UPSERT INTO dbo.CSDepartment
                    var CSDepartment = await _repo.UpsertInto_CSDepartment(
                                                            postedData.OperationType,
                                                            departmentGuID,
                                                            postedData.Description?.Trim(),
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            (int?)DocumentType.department,
                                                            (int?)DocumentStatus.active,
                                                            userInfo.BranchId,
                                                            userInfo.CompanyId,
                                                            con, transaction);
                    #endregion

                    #region PORTION FOR :: HANLDE TRANSACTION
                    switch (CSDepartment.Value)
                    {
                        case (int)Code.Created:
                        case (int)Code.Accepted:
                            await transaction.CommitAsync();
                            return ServiceResult.success(Message.serverResponse(CSDepartment.Value), (int)CSDepartment.Value);
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