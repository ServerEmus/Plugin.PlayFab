using PlayFab.DataModels;

namespace Plugin.PlayFab;

internal partial class File
{
    [HTTP("POST", "/File/AbortFileUploads")]
    [HTTP("POST", "/File/AbortFileUploads?{!args}")]
    public static bool AbortFileUploads(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<AbortFileUploadsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<AbortFileUploadsResponse>(new());
    }
}
