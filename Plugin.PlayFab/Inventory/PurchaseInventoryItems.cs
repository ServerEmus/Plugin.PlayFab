using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/PurchaseInventoryItems")]
    [HTTP("POST", "/Inventory/PurchaseInventoryItems?{!args}")]
    public static bool PurchaseInventoryItems(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<PurchaseInventoryItemsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<PurchaseInventoryItemsResponse>(new());
    }
}
