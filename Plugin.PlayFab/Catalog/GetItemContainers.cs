using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/GetItemContainers")]
    [HTTP("POST", "/Catalog/GetItemContainers?{!args}")]
    public static bool GetItemContainers(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<GetItemContainersRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<GetItemContainersResponse>(new());
    }
}
