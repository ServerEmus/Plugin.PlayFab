using PlayFab.DataModels;

namespace Plugin.PlayFab;

internal partial class File
{
    [HTTP("POST", "/File/FinalizeFileUploads")]
    [HTTP("POST", "/File/FinalizeFileUploads?{!args}")]
    public static bool FinalizeFileUploads(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<FinalizeFileUploadsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<FinalizeFileUploadsResponse>(new());
    }
}
