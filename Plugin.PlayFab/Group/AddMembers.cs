using PlayFab.GroupsModels;
using Plugin.PlayFab.Models;

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
        if (!GroupManager.AddMembers(request.Group.Id, request.Members.Select(x=>(FabId)x), request.RoleId))
            return server.SendError(new()
            {
                Error = PF.PlayFabErrorCode.RoleDoesNotExist,
                ErrorMessage = "RoleDoesNotExist"
            });
        return server.SendSuccess<EmptyResponse>();
    }
}
