using PlayFab.GroupsModels;

namespace Plugin.PlayFab;

internal partial class Group
{
    [HTTP("POST", "/Group/ApplyToGroup")]
    [HTTP("POST", "/Group/ApplyToGroup?{!args}")]
    public static bool ApplyToGroup(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<ApplyToGroupRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        bool canAccept = request.AutoAcceptOutstandingInvite.HasValue && request.AutoAcceptOutstandingInvite.Value;
        int ret = GroupManager.ApplyToGroup(request.Group.Id, request.Entity.Id, canAccept, out var time);
        if (ret == 1)
            return server.SendError(new()
            {
                Error = PF.PlayFabErrorCode.EntityBlockedByGroup,
                ErrorMessage = "EntityBlockedByGroup"
            });
        if (ret == 2)
            return server.SendError(new()
            {
                Error = PF.PlayFabErrorCode.EntityIsAlreadyMember,
                ErrorMessage = "EntityIsAlreadyMember"
            });
        if (ret == 3)
            return server.SendError(new()
            {
                Error = PF.PlayFabErrorCode.RoleDoesNotExist,
                ErrorMessage = "RoleDoesNotExist"
            });
        if (ret == 4)
            return server.SendError(new()
            {
                Error = PF.PlayFabErrorCode.OutstandingInvitationAcceptedInstead,
                ErrorMessage = "OutstandingInvitationAcceptedInstead"
            });
        return server.SendSuccess<ApplyToGroupResponse>(new()
        { 
            Entity = new()
            {
                Key = new()
                {
                    Id = request.Entity.Id,
                    Type = "title_player_account"
                },
                Lineage = [],
            },
            Expires = time,
            Group = request.Group
        });
    }
}
