using PlayFab.GroupsModels;

namespace Plugin.PlayFab;

internal partial class Group
{
    [HTTP("POST", "/Group/ListMembershipOpportunities")]
    [HTTP("POST", "/Group/ListMembershipOpportunities?{!args}")]
    public static bool ListMembershipOpportunities(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<ListMembershipOpportunitiesRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        return server.SendSuccess<ListMembershipOpportunitiesResponse>(new());
    }
}
