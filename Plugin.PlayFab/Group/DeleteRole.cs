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
        {
            return server.SendError(new()
            { 
                Error = PF.PlayFabErrorCode.RoleIsGroupDefaultMember,
                ErrorMessage = "RoleIsGroupDefaultMember"
            });
        }
        var group = DBFabGroup.GetOne(x=>x.Id == request.Group.Id);
        if (group == null)
            return server.SendSuccess<EmptyResponse>();
        if (!group.Roles.ContainsKey(request.RoleId))
            return server.SendError(new()
            {
                Error = PF.PlayFabErrorCode.RoleDoesNotExist,
                ErrorMessage = "RoleDoesNotExist"
            });
        group.Roles.Remove(request.RoleId);
        for (int i = 0; i < group.MembersAndRoles.Count; i++)
        {
            var member = group.MembersAndRoles.ElementAt(i);
            group.MembersAndRoles[member.Key] = group.MemberId;
        }
        DBFabGroup.Update(group);
        return server.SendSuccess<EmptyResponse>();
    }
}
