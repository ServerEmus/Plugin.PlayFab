using PlayFab.GroupsModels;

namespace Plugin.PlayFab;

internal partial class Group
{
    [HTTP("POST", "/Group/CreateRole")]
    [HTTP("POST", "/Group/CreateRole?{!args}")]
    public static bool CreateRole(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<CreateGroupRoleRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var ret = GroupManager.CreateRole(request.Group.Id, request.RoleId, request.RoleName);
        if (ret == 1)
            return server.SendError(new()
            {
                Error = PF.PlayFabErrorCode.RoleNameNotAvailable,
                ErrorMessage = "RoleNameNotAvailable"
            });
        if (ret == 2)
            return server.SendError(new()
            {
                Error = PF.PlayFabErrorCode.DuplicateRoleId,
                ErrorMessage = "DuplicateRoleId"
            });
        return server.SendSuccess<CreateGroupRoleResponse>(new()
        {
            ProfileVersion = 0,
            RoleId = request.RoleId,
            RoleName = request.RoleName,
        });
    }
}
