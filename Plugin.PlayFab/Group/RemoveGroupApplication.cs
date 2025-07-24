using PlayFab.GroupsModels;

namespace Plugin.PlayFab;

internal partial class Group
{
    [HTTP("POST", "/Group/RemoveGroupApplication")]
    [HTTP("POST", "/Group/RemoveGroupApplication?{!args}")]
    public static bool RemoveGroupApplication(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<RemoveGroupApplicationRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        return server.SendSuccess<EmptyResponse>();
    }
}
