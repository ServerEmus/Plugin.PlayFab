using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/SetItemModerationState")]
    [HTTP("POST", "/Catalog/SetItemModerationState?{!args}")]
    public static bool SetItemModerationState(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<SetItemModerationStateRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<SetItemModerationStateResponse>(new());
    }
}
