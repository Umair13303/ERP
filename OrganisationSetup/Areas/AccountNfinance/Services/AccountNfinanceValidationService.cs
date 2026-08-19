using Humanizer;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using OrganisationSetup.Areas.Inventory.Services;
using OrganisationSetup.Models.DAL;
using OrganisationSetup.Services;
using SharedUI.Models.Enums;
using SharedUI.Models.Responses;
using SharedUI.Models.SQLParameters; // Required for async methods

namespace OrganisationSetup.Areas.AccountNfinance.Services
{
    public interface IAccountNfinanceValidation
    {
        Task<bool> isAFChartOfAccountValid(string? operationType, Guid? guID, string? description);
        Task<ServiceResult> isAFInvoiceValid(string? operationType, Guid? guID, PostedData postedData);
    }

    public class AccountNfinanceValidationService : IAccountNfinanceValidation
    {
        private readonly ERPOrganisationSetupContext _eRPOSContext;
        private readonly ICommon _commonServices;
        private readonly IInventoryRetriever _irServices;
        


        public AccountNfinanceValidationService(ERPOrganisationSetupContext eRPOSC, ICommon commonServices, IInventoryRetriever irServices)
        {
            _eRPOSContext = eRPOSC;
            _commonServices = commonServices;
            _irServices = irServices;
        }

        public async Task<bool> isAFChartOfAccountValid(string? operationType, Guid? guID, string? description)
        {
            if (string.IsNullOrEmpty(operationType)) return false;
            switch (operationType)
            {
                case nameof(OperationType.INSERT_DATA_INTO_DB):
                    return !await _eRPOSContext.AFChartOfAccount
                        .AnyAsync(x => x.Description!.Trim().ToLower() == description!.Trim().ToLower());

                case nameof(OperationType.UPDATE_DATA_INTO_DB):
                    bool exists = await _eRPOSContext.AFChartOfAccount.AnyAsync(x => x.GuID == guID);

                    return exists;

                default:
                    return false;
            }
        }

        public async Task<ServiceResult> isAFInvoiceValid(string? operationType, Guid? guID, PostedData postedData)
        {
            switch (operationType)
            {
                case nameof(OperationType.INSERT_DATA_INTO_DB):
                    var errorMessage = new List<string>();
                    if (postedData?.PostedDataAFInvoicePPI == null || !postedData.PostedDataAFInvoicePPI.Any())
                    {
                        return ServiceResult.failure("No record found in invoice detail!", (int)Code.BadRequest);
                    }

                    var uniqueProductIdList = postedData.PostedDataAFInvoicePPI.Where(x => x.ProductId.HasValue).Select(x => x.ProductId!.Value).Distinct().ToList();
                    var mappedProductATIIdList = await _commonServices.get_ActiveATIByParam(uniqueProductIdList);
                    var uniqueMappedProductATIIdList = mappedProductATIIdList.Values.Distinct().ToList();
                    var mappedATICostingModeList = await _eRPOSContext.IProductATI.Where(x => uniqueMappedProductATIIdList.Contains(x.Id)).ToDictionaryAsync(x => x.Id, x => x.CostingModeId);

                    foreach (var item in postedData.PostedDataAFInvoicePPI)
                    {
                        decimal availableStock = await _irServices.get_iProductCurrentStockByParam(item.ProductId,item.ProductCombinationId,postedData.LocationId);

                        if(item.Quantity > availableStock)
                        {
                            errorMessage.Add($"Product {item.ProductId}: availability is deffered by {item.Quantity - availableStock} no of units.");
                        }

                        if (!item.ProductId.HasValue || !mappedProductATIIdList.TryGetValue(item.ProductId.Value, out var atiId))
                        {
                            errorMessage.Add($"Product {item.ProductId}: no active ATI mapping found, cannot determine costing mode.");
                            continue;
                        }
                        if (!mappedATICostingModeList.TryGetValue(atiId, out var costingModeId) || !costingModeId.HasValue)
                        {
                            errorMessage.Add($"Product {item.ProductId}: ATI {atiId} has no CostingModeId configured.");
                            continue;
                        }
                        item.ProductATIId = atiId;
                        item.vCostingModeId = costingModeId;
                    }
                    if (errorMessage.Any())
                    {
                        return ServiceResult.failure(string.Join(" ", errorMessage), (int)Code.BadRequest);
                    }
                    return ServiceResult.success("Invoice record has been validated!", (int)Code.OK);
               
            }
            return ServiceResult.failure(string.Join(" "), (int)Code.BadRequest);
        }


    }
}