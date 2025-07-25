using PlayFab.GroupsModels;

using Plugin.PlayFab.Models;

namespace Plugin.PlayFab;

internal partial class Group
{
    [HTTP("POST", "/Group/DeleteRole")]
    [HTTP("POST", "/Group/DeleteRole?{!args}")]
    public static bool DeleteRole(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<DeleteRoleRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        if (FabGroup.MainRoles.ContainsKey(request.RoleId))
            return server.SendError(new()
            { 
                Error = PF.PlayFabErrorCode.RoleIsGroupDefaultMember,
                ErrorMessage = "RoleIsGroupDefaultMember"
            });
        var ret = DBFabGroup.DeleteRole(request.Group.Id, request.RoleId);
        if (ret == 1)
            return server.SendError(new()
            {
                Error = PF.PlayFabErrorCode.RoleDoesNotExist,
                ErrorMessage = "RoleDoesNotExist"
            });
        return server.SendSuccess<EmptyResponse>();
    }
}
