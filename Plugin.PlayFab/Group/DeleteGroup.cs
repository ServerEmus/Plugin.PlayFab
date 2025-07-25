using PlayFab.GroupsModels;


namespace Plugin.PlayFab;

internal partial class Group
{
    [HTTP("POST", "/Group/DeleteGroup")]
    [HTTP("POST", "/Group/DeleteGroup?{!args}")]
    public static bool DeleteGroup(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<DeleteGroupRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        GroupManager.DeleteGroup(request.Group.Id);
        return server.SendSuccess<EmptyResponse>();
    }
}
