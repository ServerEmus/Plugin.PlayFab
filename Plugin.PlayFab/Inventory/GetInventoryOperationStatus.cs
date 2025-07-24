using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/GetInventoryOperationStatus")]
    [HTTP("POST", "/Inventory/GetInventoryOperationStatus?{!args}")]
    public static bool GetInventoryOperationStatus(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<GetInventoryOperationStatusRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<GetInventoryOperationStatusResponse>(new());
    }
}
