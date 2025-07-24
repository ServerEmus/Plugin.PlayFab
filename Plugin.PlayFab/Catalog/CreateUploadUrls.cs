using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/CreateUploadUrls")]
    [HTTP("POST", "/Catalog/CreateUploadUrls?{!args}")]
    public static bool CreateUploadUrls(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<CreateUploadUrlsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<CreateUploadUrlsResponse>(new());
    }
}
