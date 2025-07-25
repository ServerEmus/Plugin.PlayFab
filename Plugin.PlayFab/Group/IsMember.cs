using PlayFab.GroupsModels;

namespace Plugin.PlayFab;

internal partial class Group
{
    [HTTP("POST", "/Group/IsMember")]
    [HTTP("POST", "/Group/IsMember?{!args}")]
    public static bool IsMember(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<IsMemberRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var contains = GroupManager.IsMember(request.Group.Id, request.Entity.Id, request.RoleId);
        return server.SendSuccess<IsMemberResponse>(new()
        { 
            IsMember = contains,
        });
    }
}
