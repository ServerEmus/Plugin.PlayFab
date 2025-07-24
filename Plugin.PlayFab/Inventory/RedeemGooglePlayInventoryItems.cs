using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/RedeemGooglePlayInventoryItems")]
    [HTTP("POST", "/Inventory/RedeemGooglePlayInventoryItems?{!args}")]
    public static bool RedeemGooglePlayInventoryItems(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<RedeemGooglePlayInventoryItemsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<RedeemGooglePlayInventoryItemsResponse>(new());
    }
}
