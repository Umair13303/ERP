using OrganisationSetup.Models.DAL;
using OrganisationSetup.Models.DAL.StoredProcedure;
using SharedUI.Models.Custom;
using SharedUI.Models.Enums;
using SharedUI.Models.SQLParameters;
using SharedUI.Models.TVP;
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
        public static decimal calculateDefaultPriceByMargin(decimal minimumPrice, decimal profitMargin)
        {
            decimal defaultPrice = minimumPrice * profitMargin;
            defaultPrice = defaultPrice / 100;
            defaultPrice = minimumPrice + defaultPrice;

            return defaultPrice;
        }
    }



}
