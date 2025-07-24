using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/ExecuteInventoryOperations")]
    [HTTP("POST", "/Inventory/ExecuteInventoryOperations?{!args}")]
    public static bool ExecuteInventoryOperations(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<ExecuteInventoryOperationsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<ExecuteInventoryOperationsResponse>(new());
    }
}
