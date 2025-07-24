using PlayFab.GroupsModels;

namespace Plugin.PlayFab;

internal partial class Group
{
    [HTTP("POST", "/Group/UnblockEntity")]
    [HTTP("POST", "/Group/UnblockEntity?{!args}")]
    public static bool UnblockEntity(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<UnblockEntityRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        return server.SendSuccess<EmptyResponse>();
    }
}
