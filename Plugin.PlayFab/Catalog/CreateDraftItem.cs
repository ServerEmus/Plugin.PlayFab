using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/CreateDraftItem")]
    [HTTP("POST", "/Catalog/CreateDraftItem?{!args}")]
    public static bool CreateDraftItem(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<CreateDraftItemRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<CreateDraftItemResponse>(new());
    }
}
