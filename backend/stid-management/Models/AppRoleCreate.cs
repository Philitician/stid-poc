using Microsoft.Graph.Models;

namespace stid_management.Models;

public record AppRoleCreate(
    string DisplayName,
    string Description,
    string Value,
    List<string>? AllowedMemberTypes,
    bool? IsEnabled,
    string? OdataType,
    string? Origin);

public static class AppRoleExtensions
{
    public static AppRole ToAppRole(this AppRoleCreate appRoleCreate) => new()
    {
        Id = Guid.NewGuid().ToString(),
        DisplayName = appRoleCreate.DisplayName,
        Description = appRoleCreate.Description,
        Value = appRoleCreate.Value,
        AllowedMemberTypes = appRoleCreate.AllowedMemberTypes ?? new List<string>{ "User" },
        IsEnabled = appRoleCreate.IsEnabled ?? true,
        OdataType = appRoleCreate.OdataType ?? "#microsoft.graph.appRole",
        Origin = appRoleCreate.Origin ?? "Application"
    };
}