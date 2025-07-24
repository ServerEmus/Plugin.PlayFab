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
        var group = DBFabGroup.GetOne(x => x.Name == request.Group.Id);
        if (group == null)
            return server.SendError(new()
            {
                Error = PF.PlayFabErrorCode.EntityBlockedByGroup,
                ErrorMessage = "EntityBlockedByGroup"
            });
        var id = request.Entity.Id;
        DateTime time = DateTime.UtcNow;
        if (group.Applications.TryGetValue(id, out time) && time < DateTime.UtcNow && request.AutoAcceptOutstandingInvite.HasValue && request.AutoAcceptOutstandingInvite.Value)
        {
            group.MembersAndRoles.Add(id, group.MemberId);
            group.Applications.Remove(id);
            DBFabGroup.Update(group);
            return server.SendError(new()
            {
                Error = PF.PlayFabErrorCode.OutstandingInvitationAcceptedInstead,
                ErrorMessage = "OutstandingInvitationAcceptedInstead"
            });
        }
        time = DateTime.UtcNow.AddDays(7);
        group.Applications.Add(id, time);
        DBFabGroup.Update(group);
        return server.SendSuccess<ApplyToGroupResponse>(new()
        { 
            Entity = new()
            {
                Key = new()
                {
                    Id = id,
                    Type = "title_player_account"
                },
                Lineage = [],
            },
            Expires = time,
            Group = request.Group
        });
    }
}
