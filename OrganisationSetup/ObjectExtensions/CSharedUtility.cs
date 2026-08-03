using SharedUI.Models.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
namespace OrganisationSetup.Areas.Inventory.Services
{
    public static class CSharedUtility
    {
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public static string? attributeKeyBuilder(string? attributeJson)
        {
            if (string.IsNullOrWhiteSpace(attributeJson))
                return null;

            try
            {
                var attributes = JsonSerializer.Deserialize<List<AttributeItem>>(attributeJson, _jsonOptions);

                if (attributes == null || attributes.Count == 0)
                    return null;
                var orderedAttributes = attributes
                    .Where(a => a != null)
                    .OrderBy(a => a.Id, StringComparer.Ordinal)
                    .ToList();

                if (orderedAttributes.Count == 0)
                    return null;

                return JsonSerializer.Serialize(orderedAttributes);
            }
            catch (JsonException)
            {
                return attributeJson;
            }
        }
    }
}
