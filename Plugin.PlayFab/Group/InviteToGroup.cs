using PlayFab.GroupsModels;


namespace Plugin.PlayFab;

internal partial class Group
{
    [HTTP("POST", "/Group/InviteToGroup")]
    [HTTP("POST", "/Group/InviteToGroup?{!args}")]
    public static bool InviteToGroup(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<InviteToGroupRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var sessionInfo  = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(sessionInfo))
            return true;
        var inviter = sessionInfo.Value.TitleAccountId;
        // need to get the user here.
        var group = DBFabGroup.GetOne(x => x.Name == request.Group.Id);
        if (group == null)
            return server.SendError(new()
            {
                Error = PF.PlayFabErrorCode.EntityBlockedByGroup,
                ErrorMessage = "EntityBlockedByGroup"
            });
        var roleid = string.IsNullOrEmpty(request.RoleId) ? group.MemberId : request.RoleId;
        var id = request.Entity.Id;
        DateTime time = DateTime.UtcNow;
        if (group.Applications.TryGetValue(id, out time) && time < DateTime.UtcNow && request.AutoAcceptOutstandingApplication.HasValue && request.AutoAcceptOutstandingApplication.Value)
        {
            group.MembersAndRoles.Add(id, roleid);
            group.Applications.Remove(id);
            group.Invitations.Remove(id);
            DBFabGroup.Update(group);
            return server.SendError(new()
            {
                Error = PF.PlayFabErrorCode.OutstandingApplicationAcceptedInstead,
                ErrorMessage = "OutstandingApplicationAcceptedInstead"
            });
        }
        time = DateTime.UtcNow.AddDays(7);
        group.Applications.Add(id, time);
        group.Invitations.Add(id, roleid);
        DBFabGroup.Update(group);
        return server.SendSuccess<InviteToGroupResponse>(new()
        { 
            Expires = time,
            Group = request.Group,
            RoleId = roleid,
            InvitedByEntity = new()
            { 
                Key = new()
                { 
                    Id = inviter,
                    Type = "title_player_account"
                },
                Lineage = []
            },
            InvitedEntity = new()
            {
                Key = new()
                {
                    Id = id,
                    Type = "title_player_account"
                },
                Lineage = []
            }
        });
    }
}
