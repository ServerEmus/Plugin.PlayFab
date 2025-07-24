using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/GetInventoryItems")]
    [HTTP("POST", "/Inventory/GetInventoryItems?{!args}")]
    public static bool GetInventoryItems(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<GetInventoryItemsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<GetInventoryItemsResponse>(new());
    }
}
