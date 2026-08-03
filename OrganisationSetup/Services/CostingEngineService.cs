//using Microsoft.Data.SqlClient;

//namespace OrganisationSetup.Services
//{
//    public interface ICostingEngineService
//    {
//        Task<CostPreview> GetSuggestedUnitCost(int productId, int? productCombinationId, int locationId, decimal quantity);

//        Task<decimal> ConsumeCost(int productId, int? productCombinationId, int locationId, decimal quantity, int refDocumentType, int refDocumentId, SqlConnection con, SqlTransaction trans);
//        Task AddCostLayer(int productId, int? productCombinationId, int locationId, decimal quantity, decimal unitCost,int refDocumentType, int refDocumentId, string? batch, DateTime? expiryDate,SqlConnection con, SqlTransaction trans);
//        Task<decimal> ReverseConsumption(int refDocumentType, int refDocumentId, int productId, decimal quantity,SqlConnection con, SqlTransaction trans);

//    }
//    public class CostPreview
//    {
//        public decimal SuggestedUnitCost { get; set; }
//        public List<LayerBreakdown> Layers { get; set; } = new();
//    }
//}
