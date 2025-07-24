using PlayFab.GroupsModels;


namespace Plugin.PlayFab;

internal partial class Group
{
    [HTTP("POST", "/Group/AcceptGroupApplication")]
    [HTTP("POST", "/Group/AcceptGroupApplication?{!args}")]
    public static bool AcceptGroupApplication(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<AcceptGroupApplicationRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var group = DBFabGroup.GetOne(x => x.Name == request.Group.Id);
        if (group == null)
            return server.SendError(new()
            {
                Error = PF.PlayFabErrorCode.EntityBlockedByGroup,
                ErrorMessage = "EntityBlockedByGroup"
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
        if (!group.Applications.TryGetValue(request.Entity.Id, out DateTime time) || time < DateTime.Now)
            return server.SendError(new()
            {
                Error = PF.PlayFabErrorCode.GroupApplicationNotFound,
                ErrorMessage = "GroupApplicationNotFound"
            });
        group.MembersAndRoles.Add(request.Entity.Id, group.MemberId);
        group.Applications.Remove(request.Entity.Id);
        DBFabGroup.Update(group);
        return server.SendSuccess<EmptyResponse>();
    }
}
