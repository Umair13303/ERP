using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUI.Models.Configurations
{
    public class UISetting
    {
        public string? DisplayName { get; set; }
        public string? OperationType { get; set; }

    }
    public class FieldConfig
    {
        public string? Display { get; set; }
        public object? DefaultValue { get; set; }
        public object? Label { get; set; }
    }
}
