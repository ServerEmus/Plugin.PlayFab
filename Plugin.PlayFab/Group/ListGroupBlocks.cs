using PlayFab.GroupsModels;


namespace Plugin.PlayFab;

internal partial class Group
{
    [HTTP("POST", "/Group/ListGroupBlocks")]
    [HTTP("POST", "/Group/ListGroupBlocks?{!args}")]
    public static bool ListGroupBlocks(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<ListGroupBlocksRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        List<GroupBlock> groupBlocks = [];
        var group = DBFabGroup.GetOne(x => x.Name == request.Group.Id);
        if (group != null)
        {
            foreach (var item in group.Blocked)
            {
                groupBlocks.Add(new()
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
        return server.SendSuccess<ListGroupBlocksResponse>(new()
        { 
            BlockedEntities = groupBlocks
        });
    }
}
