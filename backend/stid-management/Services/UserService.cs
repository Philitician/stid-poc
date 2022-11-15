using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Identity.Web;

namespace stid_management.Services;

public interface IUserService
{
    Task<List<User>> GetUsersAsync();
    Task<AppRoleAssignment> AddAppRoleToUser(string userId, string appRoleId, string? appId);
}

public class UserService : IUserService
{
    private readonly GraphServiceClient _graphServiceClient;

    public UserService(GraphServiceClient graphServiceClient)
    {
        _graphServiceClient = graphServiceClient;
    }
    
    public Task<List<User>> GetUsersAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<AppRoleAssignment> AddAppRoleToUser(string userId, string appRoleId, string? appId)
    {
        var appRoleAssignment = new AppRoleAssignment
        {
            PrincipalId = userId,
            AppRoleId = appRoleId,
            ResourceId = appId
        };
        var res = await _graphServiceClient.Users[userId].AppRoleAssignments.PostAsync(appRoleAssignment);
        return res;
    }
}