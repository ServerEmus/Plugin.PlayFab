using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/RedeemMicrosoftStoreInventoryItems")]
    [HTTP("POST", "/Inventory/RedeemMicrosoftStoreInventoryItems?{!args}")]
    public static bool RedeemMicrosoftStoreInventoryItems(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<RedeemMicrosoftStoreInventoryItemsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<RedeemMicrosoftStoreInventoryItemsResponse>(new());
    }
}
