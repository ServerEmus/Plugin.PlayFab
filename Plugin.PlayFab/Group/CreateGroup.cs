using PlayFab.GroupsModels;
using Plugin.PlayFab.Models;

namespace Plugin.PlayFab;

internal partial class Group
{
    [HTTP("POST", "/Group/CreateGroup")]
    [HTTP("POST", "/Group/CreateGroup?{!args}")]
    public static bool CreateGroup(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<CreateGroupRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        var fabGroup = GroupManager.CreateGroup(request.GroupName);
        if (fabGroup == null)
            return server.SendError(new()
            { 
                Error = PF.PlayFabErrorCode.GroupNameNotAvailable,
                ErrorMessage = "GroupNameNotAvailable"
            });
        return server.SendSuccess<CreateGroupResponse>(new()
        { 
            AdminRoleId = fabGroup.AdminId,
            Created = fabGroup.CreatedAt,
            Group = new()
            { 
                Id = fabGroup.Id,
                Type = "group",
            },
            GroupName = request.GroupName,
            MemberRoleId = fabGroup.MemberId,
            ProfileVersion = 0,
            Roles = fabGroup.Roles,
        });
    }
}
