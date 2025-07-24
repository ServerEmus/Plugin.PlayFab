using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/RedeemPlayStationStoreInventoryItems")]
    [HTTP("POST", "/Inventory/RedeemPlayStationStoreInventoryItems?{!args}")]
    public static bool RedeemPlayStationStoreInventoryItems(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<RedeemPlayStationStoreInventoryItemsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<RedeemPlayStationStoreInventoryItemsResponse>(new());
    }
}
