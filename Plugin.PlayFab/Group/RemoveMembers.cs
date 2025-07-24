using PlayFab.GroupsModels;

namespace Plugin.PlayFab;

internal partial class Group
{
    [HTTP("POST", "/Group/RemoveMembers")]
    [HTTP("POST", "/Group/RemoveMembers?{!args}")]
    public static bool RemoveMembers(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<RemoveMembersRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        return server.SendSuccess<EmptyResponse>();
    }
}
