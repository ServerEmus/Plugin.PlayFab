using PlayFab.GroupsModels;


namespace Plugin.PlayFab;

internal partial class Group
{
    [HTTP("POST", "/Group/BlockEntity")]
    [HTTP("POST", "/Group/BlockEntity?{!args}")]
    public static bool BlockEntity(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<BlockEntityRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var group = DBFabGroup.GetOne(x => x.Name == request.Group.Id);
        if (group == null)
            return server.SendError(new()
            {
                Error = PF.PlayFabErrorCode.RoleNameNotAvailable,
                ErrorMessage = "RoleNameNotAvailable"
            });
        group.Blocked.Add(request.Entity.Id);
        DBFabGroup.Update(group);
        return server.SendSuccess<EmptyResponse>();
    }
}
