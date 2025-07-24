using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/DeleteInventoryItems")]
    [HTTP("POST", "/Inventory/DeleteInventoryItems?{!args}")]
    public static bool DeleteInventoryItems(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<DeleteInventoryItemsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<DeleteInventoryItemsResponse>(new());
    }
}
