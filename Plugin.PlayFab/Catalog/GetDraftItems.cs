using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/GetDraftItems")]
    [HTTP("POST", "/Catalog/GetDraftItems?{!args}")]
    public static bool GetDraftItems(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<GetDraftItemsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<GetDraftItemsResponse>(new());
    }
}
