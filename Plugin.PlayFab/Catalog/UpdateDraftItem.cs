using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/UpdateDraftItem")]
    [HTTP("POST", "/Catalog/UpdateDraftItem?{!args}")]
    public static bool UpdateDraftItem(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<UpdateDraftItemRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<UpdateDraftItemResponse>(new());
    }
}
