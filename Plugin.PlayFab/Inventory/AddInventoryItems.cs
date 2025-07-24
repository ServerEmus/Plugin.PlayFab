using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/AddInventoryItems")]
    [HTTP("POST", "/Inventory/AddInventoryItems?{!args}")]
    public static bool AddInventoryItems(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<AddInventoryItemsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<AddInventoryItemsResponse>(new());
    }
}
