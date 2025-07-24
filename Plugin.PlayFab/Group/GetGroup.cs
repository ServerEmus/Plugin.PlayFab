using PlayFab.GroupsModels;


namespace Plugin.PlayFab;

internal partial class Group
{
    [HTTP("POST", "/Group/GetGroup")]
    [HTTP("POST", "/Group/GetGroup?{!args}")]
    public static bool GetGroup(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<GetGroupRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var group = DBFabGroup.GetOne(x=>x.Id == request.Group.Id && x.Name == request.GroupName);
        if (group == null)
            return server.SendError(new()
            { 
                Error = PF.PlayFabErrorCode.GroupNameNotAvailable,
                ErrorMessage = "GroupNameNotAvailable"
            });
        return server.SendSuccess<GetGroupResponse>(new()
        { 
            AdminRoleId = group.AdminId,
            Created = group.CreatedAt,
            Group = new()
            { 
                Id = group.Id,
                Type = "group"
            },
            GroupName = group.Name,
            MemberRoleId = group.MemberId,
            ProfileVersion = 0,
            Roles = group.Roles
        });
    }
}
