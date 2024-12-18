using Azure.Core;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientManagement.BusinessLogicLayer
{
    public static class ObjectExtensions
    {
        public static string ToJsonString(this object obj)
        {
            return JsonSerializer.Serialize(obj, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
        }
    }
}
