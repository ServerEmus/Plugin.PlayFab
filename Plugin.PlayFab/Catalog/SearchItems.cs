using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/SearchItems")]
    [HTTP("POST", "/Catalog/SearchItems?{!args}")]
    public static bool SearchItems(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<SearchItemsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<SearchItemsResponse>(new());
    }
}
