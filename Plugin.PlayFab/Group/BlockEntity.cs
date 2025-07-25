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
        if (GroupManager.BlockEntity(request.Group.Id, request.Entity.Id))
            return server.SendError(new()
            {
                Error = PF.PlayFabErrorCode.RoleNameNotAvailable,
                ErrorMessage = "RoleNameNotAvailable"
            });
        return server.SendSuccess<EmptyResponse>();
    }
}
