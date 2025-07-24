using PlayFab.DataModels;

namespace Plugin.PlayFab;

internal partial class Objects
{
    [HTTP("POST", "/Objects/GetObjects")]
    [HTTP("POST", "/Objects/GetObjects?{!args}")]
    public static bool GetObjects(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<GetObjectsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<GetObjectsResponse>(new());
    }
}
