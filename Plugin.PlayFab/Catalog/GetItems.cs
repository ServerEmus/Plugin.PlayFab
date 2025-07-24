using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/GetItems")]
    [HTTP("POST", "/Catalog/GetItems?{!args}")]
    public static bool GetItems(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<GetItemsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<GetItemsResponse>(new());
    }
}
