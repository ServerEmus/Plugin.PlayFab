using PlayFab.DataModels;

namespace Plugin.PlayFab;

internal partial class Objects
{
    [HTTP("POST", "/Objects/SetObjects")]
    [HTTP("POST", "/Objects/SetObjects?{!args}")]
    public static bool SetObjects(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<SetObjectsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<SetObjectsResponse>(new());
    }
}
