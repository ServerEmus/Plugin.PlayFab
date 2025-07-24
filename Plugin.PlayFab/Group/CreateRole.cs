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
        var group = DBFabGroup.GetOne(x => x.Name == request.Group.Id);
        if (group == null)
            return server.SendError(new()
            {
                Error = PF.PlayFabErrorCode.RoleNameNotAvailable,
                ErrorMessage = "RoleNameNotAvailable"
            });
        if (group.Roles.ContainsKey(request.RoleId))
            return server.SendError(new()
            {
                Error = PF.PlayFabErrorCode.DuplicateRoleId,
                ErrorMessage = "DuplicateRoleId"
            });
        if (group.Roles.ContainsValue(request.RoleName))
            return server.SendError(new()
            {
                Error = PF.PlayFabErrorCode.RoleNameNotAvailable,
                ErrorMessage = "RoleNameNotAvailable"
            });
        if (request.RoleName.Length is <1 or >100)
            return server.SendError(new()
            {
                Error = PF.PlayFabErrorCode.RoleNameNotAvailable,
                ErrorMessage = "RoleNameNotAvailable"
            });
        // TODO: Additional checks.
        group.Roles.Add(request.RoleId, request.RoleName);
        DBFabGroup.Update(group);
        return server.SendSuccess<CreateGroupRoleResponse>(new()
        {
            ProfileVersion = 0,
            RoleId = request.RoleId,
            RoleName = request.RoleName,
        });
    }
}
