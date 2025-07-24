using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/TransferInventoryItems")]
    [HTTP("POST", "/Inventory/TransferInventoryItems?{!args}")]
    public static bool TransferInventoryItems(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<TransferInventoryItemsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<TransferInventoryItemsResponse>(new());
    }
}
