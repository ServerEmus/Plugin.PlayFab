using PlayFab.GroupsModels;

namespace Plugin.PlayFab;

internal partial class Group
{
    [HTTP("POST", "/Group/RemoveGroupInvitation")]
    [HTTP("POST", "/Group/RemoveGroupInvitation?{!args}")]
    public static bool RemoveGroupInvitation(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<RemoveGroupInvitationRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        return server.SendSuccess<EmptyResponse>();
    }
}
