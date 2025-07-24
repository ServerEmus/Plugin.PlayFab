using PlayFab.GroupsModels;

namespace Plugin.PlayFab;

internal partial class Group
{
    [HTTP("POST", "/Group/ListMembership")]
    [HTTP("POST", "/Group/ListMembership?{!args}")]
    public static bool ListMembership(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<ListMembershipRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        return server.SendSuccess<ListMembershipResponse>(new());
    }
}
