using PlayFab.GroupsModels;

namespace Plugin.PlayFab;

internal partial class Group
{
    [HTTP("POST", "/Group/UpdateRole")]
    [HTTP("POST", "/Group/UpdateRole?{!args}")]
    public static bool UpdateRole(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<UpdateGroupRoleRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        return server.SendSuccess<UpdateGroupRoleResponse>();
    }
}
