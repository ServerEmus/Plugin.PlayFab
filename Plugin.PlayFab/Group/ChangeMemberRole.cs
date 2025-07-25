using PlayFab.GroupsModels;

namespace Plugin.PlayFab;

internal partial class Group
{
    [HTTP("POST", "/Group/ChangeMemberRole")]
    [HTTP("POST", "/Group/ChangeMemberRole?{!args}")]
    public static bool ChangeMemberRole(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<ChangeMemberRoleRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        if (!GroupManager.TryGetGroup(request.Group.Id, out var group) || group == null)
            return server.SendError(new()
            {
                Error = PF.PlayFabErrorCode.RoleNameNotAvailable,
                ErrorMessage = "RoleNameNotAvailable"
            });
        foreach (var item in group.MembersAndRoles.Where(x => x.Value == request.OriginRoleId && request.Members.Any(y => y.Id == x.Key)).Select(x => x.Key).ToList())
            GroupManager.ChangeMemberRole(group.Id, item, request.DestinationRoleId);
        return server.SendSuccess<EmptyResponse>();
    }
}
