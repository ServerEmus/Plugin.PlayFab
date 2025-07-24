using PlayFab.ProfilesModels;

namespace Plugin.PlayFab;

internal partial class Profile
{
    [HTTP("POST", "/Profile/GetTitlePlayersFromXboxLiveIDs")]
    [HTTP("POST", "/Profile/GetTitlePlayersFromXboxLiveIDs?{!args}")]
    public static bool GetTitlePlayersFromXboxLiveIDs(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<GetTitlePlayersFromXboxLiveIDsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        return server.SendSuccess<GetTitlePlayersFromProviderIDsResponse>();
    }
}
