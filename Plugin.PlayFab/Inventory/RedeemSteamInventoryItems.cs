using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/RedeemSteamInventoryItems")]
    [HTTP("POST", "/Inventory/RedeemSteamInventoryItems?{!args}")]
    public static bool RedeemSteamInventoryItems(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<RedeemSteamInventoryItemsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<RedeemSteamInventoryItemsResponse>(new());
    }
}
