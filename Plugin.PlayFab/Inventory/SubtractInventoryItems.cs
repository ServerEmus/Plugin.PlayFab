using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/SubtractInventoryItems")]
    [HTTP("POST", "/Inventory/SubtractInventoryItems?{!args}")]
    public static bool SubtractInventoryItems(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<SubtractInventoryItemsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<SubtractInventoryItemsResponse>(new());
    }
}
