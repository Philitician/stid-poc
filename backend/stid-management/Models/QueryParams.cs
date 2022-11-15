using System.Text.Json.Serialization;

namespace stid_management.Models;

public class QueryParams
{
    [JsonPropertyName("userId")]
    public string UserId { get; set; }
    
    [JsonPropertyName("appRoleId")]
    public string AppRoleId { get; set; }
    
    [JsonPropertyName("appId")]
    public string AppId { get; set; }
    
    public static bool TryParse(string query, out QueryParams result)
    {
        result = null;
        if (string.IsNullOrEmpty(query))
            return false;
        var parts = query.Split('&');
        if (parts.Length != 3)
            return false;
        var userId = parts[0].Split('=');
        var appRoleId = parts[1].Split('=');
        var appId = parts[2].Split('=');
        if (userId.Length != 2 || appRoleId.Length != 2 || appId.Length != 2)
            return false;
        result = new QueryParams
        {
            UserId = userId[1],
            AppRoleId = appRoleId[1],
            AppId = appId[1]
        };
        return true;
    }
}