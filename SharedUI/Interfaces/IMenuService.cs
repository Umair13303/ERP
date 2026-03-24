using SharedUI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUI.Interfaces
{
    public interface IMenuService
    {
        Task<List<VMMenu>> getMenuForUserRole();
    }
}
