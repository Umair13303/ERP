using Microsoft.EntityFrameworkCore;
using OrganisationSetup.Models.DAL;
using SharedUI.Models.Enums;

namespace OrganisationSetup.Services
{
    public interface IAFAtomicServices
    {
        Task<bool> generate_GeneralLedgerRecord(List<AFGeneralLedger> generalLedger, List<AFStockLedger> stockLedger, CancellationToken cancellationToken = default);
        Task<bool> generate_ReversalRecord(DocumentType documentType,int documentId,CancellationToken cancellationToken = default);
    }

    public class AFAtomicServices : IAFAtomicServices
    {
        private readonly ERPOrganisationSetupContext _eRPOSContext;

        public AFAtomicServices(ERPOrganisationSetupContext eRPOSContext)
        {
            _eRPOSContext = eRPOSContext;
        }

        public async Task<bool> generate_GeneralLedgerRecord(List<AFGeneralLedger> generalLedger, List<AFStockLedger> stockLedger, CancellationToken cancellationToken = default)
        {
            {
                if ((generalLedger == null || !generalLedger.Any()) && (stockLedger == null || !stockLedger.Any()))
                {
                    return true; 
                }
                var strategy = _eRPOSContext.Database.CreateExecutionStrategy();

                return await strategy.ExecuteAsync(async () =>
                {
                    using var transaction = await _eRPOSContext.Database.BeginTransactionAsync(cancellationToken);
                    try
                    {
                        if (generalLedger != null && generalLedger.Any())
                        {
                            await _eRPOSContext.Set<AFGeneralLedger>().AddRangeAsync(generalLedger, cancellationToken);
                        }
                        if (stockLedger != null && stockLedger.Any())
                        {
                            await _eRPOSContext.Set<AFStockLedger>().AddRangeAsync(stockLedger, cancellationToken);
                        }
                        await _eRPOSContext.SaveChangesAsync(cancellationToken);
                        await transaction.CommitAsync(cancellationToken);
                        return true;
                    }
                    catch
                    {
                        await transaction.RollbackAsync(cancellationToken);
                        throw;
                    }
                });
            }
        }
        public async Task<bool> generate_ReversalRecord(DocumentType documentType, int documentId, CancellationToken cancellationToken = default)
        {
            int docTypeId = (int)documentType;

            var strategy = _eRPOSContext.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _eRPOSContext.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    await _eRPOSContext.Set<AFGeneralLedger>()
                        .Where(g => g.RefDocumentType == docTypeId && g.RefDocumentId == documentId)
                        .ExecuteDeleteAsync(cancellationToken);

                    await _eRPOSContext.Set<AFStockLedger>()
                        .Where(s => s.RefDocumentType == docTypeId && s.RefDocumentId == documentId)
                        .ExecuteDeleteAsync(cancellationToken);

                    await transaction.CommitAsync(cancellationToken);
                    return true;
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            });
        }

    }
}
