using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Graph.ServicePrincipals;
using Microsoft.Kiota.Abstractions;

namespace stid_management.Services;

public interface IAppRegistrationService
{
    Task<List<Application>> GetApplicationsAsync();
    Task<Application> GetApplicationAsync(string objectId);
    
    Task<Application> AddAppRoleToApplicationAsync(string objectId, AppRole appRole);
}

public class AppRegistrationService : IAppRegistrationService
{
    private readonly GraphServiceClient _graphServiceClient;

    public AppRegistrationService(GraphServiceClient graphServiceClient)
    {
        _graphServiceClient = graphServiceClient;
    }
    
    public async Task<ServicePrincipal> GetServicePrincipalFromClientIdAsync(string clientId)
    {
        var servicePrincipals = await _graphServiceClient.ServicePrincipals.GetAsync(requestConfig =>
        {
            requestConfig.QueryParameters.Filter = $"appId eq '{clientId}'";
            requestConfig.QueryParameters.Select = new[] { "id", "appId", "appRoles" };
        });
        return servicePrincipals.Value.First(x => x.AppId == clientId);
    }

    public async Task<List<Application>> GetApplicationsAsync()
    {
        // var apps = await _graphServiceClient.Applications.GetAsync(requestConfig =>
        // {
        //     requestConfig.QueryParameters.Select = new[] { "id", "displayName", "appId", "appRoles" };
        // });
        // var appsInfo = apps.Value.
        var applications = await _graphServiceClient.Applications.GetAsync();
        return applications.Value;
    }
    
    public async Task<Application> GetApplicationAsync(string objectId)
    {
        return await _graphServiceClient.Applications[objectId].GetAsync();
    }
    
    public async Task<Application> AddAppRoleToApplicationAsync(string objectId, AppRole appRole)
    {
        var patchingApp = new Application
        {
            AppRoles = new List<AppRole> { appRole }
        };
        return await _graphServiceClient.Applications[objectId].PatchAsync(patchingApp);
    }
}

public class ServicePrincipalInfo
{
    public string Id { get; set; }
    public List<AppRole> AppRoles { get; set; }
}