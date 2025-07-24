using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/PublishDraftItem")]
    [HTTP("POST", "/Catalog/PublishDraftItem?{!args}")]
    public static bool PublishDraftItem(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<PublishDraftItemRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<PublishDraftItemResponse>(new());
    }
}
