using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/GetEntityDraftItems")]
    [HTTP("POST", "/Catalog/GetEntityDraftItems?{!args}")]
    public static bool GetEntityDraftItems(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<GetEntityDraftItemsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<GetEntityDraftItemsResponse>(new());
    }
}
