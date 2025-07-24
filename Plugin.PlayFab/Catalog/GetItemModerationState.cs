using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/GetItemModerationState")]
    [HTTP("POST", "/Catalog/GetItemModerationState?{!args}")]
    public static bool GetItemModerationState(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<GetItemModerationStateRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<GetItemModerationStateResponse>(new());
    }
}
