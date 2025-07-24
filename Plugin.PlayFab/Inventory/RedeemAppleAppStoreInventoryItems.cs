using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/RedeemAppleAppStoreInventoryItems")]
    [HTTP("POST", "/Inventory/RedeemAppleAppStoreInventoryItems?{!args}")]
    public static bool RedeemAppleAppStoreInventoryItems(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<RedeemAppleAppStoreInventoryItemsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<RedeemAppleAppStoreInventoryItemsResponse>(new());
    }
}
