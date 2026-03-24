namespace SharedUI.Models.Enums
{
    public class SetupRoute
    {
        public enum Area
        {
            ApplicationConfiguration=1,
            CompanySetup=2,
            AccountNfinance = 3,
            Inventory = 4,
            AIMSCHConfiguration = 5,
            SaleOperation = 6,
        }
        public enum Controller
        {
            COMDashboard = 1,
            COMAuthentication = 2,
        }
        public enum Action
        {
            Login = 1,
            DashboardDefault = 1,
        }
        public enum Api
        {
            COMInternalAPI=1
        }
    }
}
