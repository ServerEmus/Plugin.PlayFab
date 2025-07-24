using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/DeleteInventoryCollection")]
    [HTTP("POST", "/Inventory/DeleteInventoryCollection?{!args}")]
    public static bool DeleteInventoryCollection(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<DeleteInventoryCollectionRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<DeleteInventoryCollectionResponse>(new());
    }
}
