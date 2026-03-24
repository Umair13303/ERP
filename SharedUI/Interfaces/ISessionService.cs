using SharedUI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUI.Interfaces
{
    public interface ISessionService
    {
        int UserId { get; }
        string UserName { get; }
        string CompanyName { get; }
        List<VMMenu> UserMenu { get; set; }

    }
}
