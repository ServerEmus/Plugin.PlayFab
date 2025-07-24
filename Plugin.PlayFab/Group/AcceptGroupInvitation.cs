using PlayFab.GroupsModels;
using System.Text.Json;


namespace Plugin.PlayFab;

internal partial class Group
{
    [HTTP("POST", "/Group/AcceptGroupInvitation")]
    [HTTP("POST", "/Group/AcceptGroupInvitation?{!args}")]
    public static bool AcceptGroupInvitation(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<AcceptGroupInvitationRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var group = DBFabGroup.GetOne(x => x.Name == request.Group.Id);
        if (group == null)
            return server.SendError(new()
            {
                Error = PF.PlayFabErrorCode.GroupInvitationNotFound,
                ErrorMessage = "GroupInvitationNotFound"
            });
        if (group.Blocked.Contains(request.Entity.Id))
            return server.SendError(new()
            {
                Error = PF.PlayFabErrorCode.EntityBlockedByGroup,
                ErrorMessage = "EntityBlockedByGroup"
            });
        if (group.MembersAndRoles.ContainsKey(request.Entity.Id))
            return server.SendError(new()
            {
                Error = PF.PlayFabErrorCode.EntityIsAlreadyMember,
                ErrorMessage = "EntityIsAlreadyMember"
            });
        if (group.Invitations.TryGetValue(request.Entity.Id, out string? role))
            return server.SendError(new()
            {
                Error = PF.PlayFabErrorCode.GroupInvitationNotFound,
                ErrorMessage = "GroupInvitationNotFound"
            });
        if (string.IsNullOrEmpty(role) || group.Roles.ContainsKey(role))
            return server.SendError(new()
            {
                Error = PF.PlayFabErrorCode.RoleDoesNotExist,
                ErrorMessage = "RoleDoesNotExist"
            });
        group.MembersAndRoles.Add(request.Entity.Id, role);
        DBFabGroup.Update(group);
        return server.SendSuccess<EmptyResponse>();
    }
}
