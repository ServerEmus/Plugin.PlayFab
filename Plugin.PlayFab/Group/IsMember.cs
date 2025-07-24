using PlayFab.GroupsModels;


namespace Plugin.PlayFab;

internal partial class Group
{
    [HTTP("POST", "/Group/IsMember")]
    [HTTP("POST", "/Group/IsMember?{!args}")]
    public static bool IsMember(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<IsMemberRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var group = DBFabGroup.GetOne(x => x.Name == request.Group.Id);
        if (group == null)
            return server.SendError(new()
            {
                Error = PF.PlayFabErrorCode.RoleNameNotAvailable,
                ErrorMessage = "RoleNameNotAvailable"
            });
        bool contains = group.MembersAndRoles.ContainsKey(request.Entity.Id);
        if (contains && !string.IsNullOrEmpty(request.RoleId))
            contains = group.MembersAndRoles.TryGetValue(request.RoleId, out var role) && role == request.RoleId;
        return server.SendSuccess<IsMemberResponse>(new()
        { 
            IsMember = contains,
        });
    }
}
