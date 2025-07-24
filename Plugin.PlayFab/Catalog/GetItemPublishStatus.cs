using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/GetItemPublishStatus")]
    [HTTP("POST", "/Catalog/GetItemPublishStatus?{!args}")]
    public static bool GetItemPublishStatus(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<GetItemPublishStatusRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<GetItemPublishStatusResponse>(new());
    }
}
