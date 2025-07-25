using PlayFab.GroupsModels;

namespace Plugin.PlayFab;

internal partial class Group
{
    [HTTP("POST", "/Group/ListGroupInvitations")]
    [HTTP("POST", "/Group/ListGroupInvitations?{!args}")]
    public static bool ListGroupInvitations(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<ListGroupInvitationsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        return server.SendSuccess<ListGroupInvitationsResponse>(new()
        {
            Invitations = []
        });
    }
}
