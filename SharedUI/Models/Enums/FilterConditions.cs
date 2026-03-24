using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUI.Models.Enums
{
    public enum FilterConditions
    {
        acCompany_ApplicationConfiguration_SolutionSetup=1,
        acBranch_ApplicationConfiguration_SolutionSetup = 2,
        acSaleUnit_ApplicationConfiguration_SolutionSetup = 3,
        acSaleUnit_Operation_ByCompany = 4,
        acBranch_Operation_ByAllowedBranches = 5,
        afChartOfAccount_Operation_ByCompanyId = 6,
        CSDepartment_Operation_ByCompany = 7,
        ISection_Operation_ByDepartment = 8,
        ICategory_Operation_BySection = 9,
        ISubCategory_Operation_ByCategory = 10,
        IBrand_Operation_ByCompany = 11,
        osvChartOfAccount_Operation_ByDefaultSetting = 12,
        ISubCategory_Operation_GroupByCategory=13





    }
}
