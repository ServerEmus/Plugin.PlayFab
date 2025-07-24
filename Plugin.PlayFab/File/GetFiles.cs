using PlayFab.DataModels;

namespace Plugin.PlayFab;

internal partial class File
{
    [HTTP("POST", "/File/GetFiles")]
    [HTTP("POST", "/File/GetFiles?{!args}")]
    public static bool GetFiles(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<GetFilesRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<GetFilesResponse>(new());
    }
}
