using Microsoft.Data.SqlClient;
using SharedUI.Models.ViewModels;
using Dapper;
using System.Data;

namespace OrganisationSetup.Models.DAL.StoredProcedure
{
    public interface IDapper
    {
        Task<IEnumerable<DTObject.Invoice_List>> populateInvoiceListByParam(Guid? guid, string operationType, int?[] documentStatusIds, int?[] invoiceStatusIds, int? customerId);
    }
    public class DapperService : IDapper
    {
        private readonly IConfiguration _config;

        public DapperService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<DTObject.Invoice_List>> populateInvoiceListByParam(Guid? guid, string operationType, int?[] documentStatusIds, int?[] invoiceStatusIds, int? customerId)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString("ERPOrganisationSetup"));
            var parameters = new
            {
                GuID = guid,
                CustomerId = customerId,
                DocumentStatusIds = documentStatusIds != null ? string.Join(",", documentStatusIds) : null,
                InvoiceStatusIds = invoiceStatusIds != null ? string.Join(",", invoiceStatusIds) : null,
            };
            var result = await db.QueryAsync<DTObject.Invoice_List>("[dbo].[AFInvoice_GetListByParam_purp]", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
