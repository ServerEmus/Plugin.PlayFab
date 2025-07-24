using PlayFab.ProfilesModels;

namespace Plugin.PlayFab;

internal partial class Profile
{
    [HTTP("POST", "/Profile/GetTitlePlayersFromMasterPlayerAccountIds")]
    [HTTP("POST", "/Profile/GetTitlePlayersFromMasterPlayerAccountIds?{!args}")]
    public static bool GetTitlePlayersFromMasterPlayerAccountIds(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<GetTitlePlayersFromMasterPlayerAccountIdsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        return server.SendSuccess<GetTitlePlayersFromMasterPlayerAccountIdsResponse>();
    }
}
