using PlayFab.DataModels;

namespace Plugin.PlayFab;

internal partial class File
{
    [HTTP("POST", "/File/DeleteFiles")]
    [HTTP("POST", "/File/DeleteFiles?{!args}")]
    public static bool DeleteFiles(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<DeleteFilesRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<DeleteFilesResponse>(new());
    }
}
