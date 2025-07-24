using PlayFab.GroupsModels;


namespace Plugin.PlayFab;

internal partial class Group
{
    [HTTP("POST", "/Group/ListGroupApplications")]
    [HTTP("POST", "/Group/ListGroupApplications?{!args}")]
    public static bool ListGroupApplications(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<ListGroupApplicationsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        List<GroupApplication> groupApplications = [];
        var group = DBFabGroup.GetOne(x => x.Name == request.Group.Id);
        if (group != null)
        {
            foreach (var item in group.Applications)
            {
                groupApplications.Add(new()
                {
                    Group = request.Group,
                    Entity = new()
                    { 
                        Key = new()
                        { 
                            Id = item.Key,
                            Type = "title_player_account"
                        },
                        Lineage = []
                    },
                    Expires = item.Value
                });
            }
        }
        return server.SendSuccess<ListGroupApplicationsResponse>(new()
        { 
            Applications = groupApplications
        });
    }
}
