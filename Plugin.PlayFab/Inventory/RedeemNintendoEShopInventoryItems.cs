using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/RedeemNintendoEShopInventoryItems")]
    [HTTP("POST", "/Inventory/RedeemNintendoEShopInventoryItems?{!args}")]
    public static bool RedeemNintendoEShopInventoryItems(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<RedeemNintendoEShopInventoryItemsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<RedeemNintendoEShopInventoryItemsResponse>(new());
    }
}
