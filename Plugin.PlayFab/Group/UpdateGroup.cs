using PlayFab.GroupsModels;

namespace Plugin.PlayFab;

internal partial class Group
{
    [HTTP("POST", "/Group/UpdateGroup")]
    [HTTP("POST", "/Group/UpdateGroup?{!args}")]
    public static bool UpdateGroup(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<UpdateGroupRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        return server.SendSuccess<UpdateGroupResponse>();
    }
}
