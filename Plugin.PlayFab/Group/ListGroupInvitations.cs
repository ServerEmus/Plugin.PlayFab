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
        List<GroupInvitation> invitations = [];
        var group = DBFabGroup.GetOne(x => x.Name == request.Group.Id);
        if (group != null)
        {
            foreach (var item in group.Blocked)
            {
                invitations.Add(new()
                {
                    Group = request.Group,
                    Entity = new()
                    {
                        Key = new()
                        {
                            Id = item,
                            Type = "title_player_account"
                        },
                        Lineage = []
                    },
                });
            }
        }
        return server.SendSuccess<ListGroupInvitationsResponse>(new()
        {
            Invitations = invitations
        });
    }
}
