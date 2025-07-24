using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/ExecuteTransferOperations")]
    [HTTP("POST", "/Inventory/ExecuteTransferOperations?{!args}")]
    public static bool ExecuteTransferOperations(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<ExecuteTransferOperationsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<ExecuteTransferOperationsResponse>(new());
    }
}
