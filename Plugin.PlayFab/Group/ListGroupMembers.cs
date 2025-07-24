using PlayFab.GroupsModels;


namespace Plugin.PlayFab;

internal partial class Group
{
    [HTTP("POST", "/Group/ListGroupMembers")]
    [HTTP("POST", "/Group/ListGroupMembers?{!args}")]
    public static bool ListGroupMembers(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<ListGroupMembersRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        List<EntityMemberRole> entityMemberRoles = [];
        var group = DBFabGroup.GetOne(x => x.Name == request.Group.Id);
        if (group != null)
        {
            foreach (var grouped in group.MembersAndRoles.GroupBy(x => x.Value))
            {
                EntityMemberRole entityMemberRole = new()
                {
                    Members = [],
                    RoleId = grouped.Key,
                    RoleName = group.Roles[grouped.Key]
                };

                foreach (var kv in grouped)
                {
                    entityMemberRole.Members.Add(new()
                    {
                        Key = new()
                        {
                            Id = kv.Key,
                            Type = "title_player_account"
                        },
                        Lineage = []
                    });
                }
                entityMemberRoles.Add(entityMemberRole);
            }
        }
        return server.SendSuccess<ListGroupMembersResponse>(new()
        { 
            Members = entityMemberRoles
        });
    }
}
