using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/GetDraftItem")]
    [HTTP("POST", "/Catalog/GetDraftItem?{!args}")]
    public static bool GetDraftItem(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<GetDraftItemRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<GetDraftItemResponse>(new());
    }
}
