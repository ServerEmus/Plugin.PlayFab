using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/GetMicrosoftStoreAccessTokens")]
    [HTTP("POST", "/Inventory/GetMicrosoftStoreAccessTokens?{!args}")]
    public static bool GetMicrosoftStoreAccessTokens(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<GetMicrosoftStoreAccessTokensRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<GetMicrosoftStoreAccessTokensResponse>(new());
    }
}
