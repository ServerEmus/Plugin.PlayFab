using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/DeleteItem")]
    [HTTP("POST", "/Catalog/DeleteItem?{!args}")]
    public static bool DeleteItem(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<DeleteItemRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<DeleteItemResponse>(new());
    }
}
