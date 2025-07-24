using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/GetInventoryCollectionIds")]
    [HTTP("POST", "/Inventory/GetInventoryCollectionIds?{!args}")]
    public static bool GetInventoryCollectionIds(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<GetInventoryCollectionIdsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<GetInventoryCollectionIdsResponse>(new());
    }
}
