using PlayFab.DataModels;

namespace Plugin.PlayFab;

internal partial class File
{
    [HTTP("POST", "/File/InitiateFileUploads")]
    [HTTP("POST", "/File/InitiateFileUploads?{!args}")]
    public static bool InitiateFileUploads(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<InitiateFileUploadsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<InitiateFileUploadsResponse>(new());
    }
}
