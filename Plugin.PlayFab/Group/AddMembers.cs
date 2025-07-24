using PlayFab.GroupsModels;


namespace Plugin.PlayFab;

internal partial class Group
{
    [HTTP("POST", "/Group/AddMembers")]
    [HTTP("POST", "/Group/AddMembers?{!args}")]
    public static bool AddMembers(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<AddMembersRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var group = DBFabGroup.GetOne(x => x.Name == request.Group.Id);
        if (group == null)
            return server.SendError(new()
            {
                Error = PF.PlayFabErrorCode.RoleDoesNotExist,
                ErrorMessage = "RoleDoesNotExist"
            });
        if (!group.Roles.ContainsKey(request.RoleId))
            return server.SendError(new()
            {
                Error = PF.PlayFabErrorCode.RoleDoesNotExist,
                ErrorMessage = "RoleDoesNotExist"
            });
        foreach (var item in request.Members)
            group.MembersAndRoles.Add(item.Id, request.RoleId);
        DBFabGroup.Update(group);
        return server.SendSuccess<EmptyResponse>();
    }
}
