using Microsoft.Graph;
using Microsoft.Graph.Models;
using stid_management.Data;

namespace stid_management.Services;

public interface IGroupService
{
    Task<Group> CreateGroup(string groupName, string groupDescription);
    Task DeleteGroup(string groupId);
    Task<List<Microsoft.Graph.Models.Group>> GetGroupMemberships(string userId);
    Task AddGroupMember(string groupId, string userId);
    Task RemoveGroupMember(string groupId, string userId);
}

public class GroupService : IGroupService
{
    private readonly GraphServiceClient _graphServiceClient;
    private readonly StidContext _db;

    public GroupService(GraphServiceClient graphServiceClient, StidContext db)
    {
        _graphServiceClient = graphServiceClient;
        _db = db;
    }

    public async Task<Group> CreateGroup(string groupName, string groupDescription)
    { 
        var group = await _graphServiceClient.Groups.PostAsync(new Group
        {
            DisplayName = groupName,
            Description = groupDescription,
            MailEnabled = false,
            MailNickname = groupName,
            SecurityEnabled = true
        });
        
        return group;
    }

    public Task DeleteGroup(string groupId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Group>> GetGroupMemberships(string userId)
    {
        var groups = await _graphServiceClient.Users[userId].MemberOf.Group.GetAsync();
        return groups.Value;
    }

    public Task AddGroupMember(string groupId, string userId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveGroupMember(string groupId, string userId)
    {
        throw new NotImplementedException();
    }
}