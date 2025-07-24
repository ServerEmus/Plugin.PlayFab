using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/UpdateInventoryItems")]
    [HTTP("POST", "/Inventory/UpdateInventoryItems?{!args}")]
    public static bool UpdateInventoryItems(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<UpdateInventoryItemsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<UpdateInventoryItemsResponse>(new());
    }
}
