
using System.Text.Json;


namespace Core.Utils
{ 
    public static class ObjectExtensions
    {
        public static string ToJsonString(this object obj)
        {
            return JsonSerializer.Serialize(obj, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
        }
    }
}
