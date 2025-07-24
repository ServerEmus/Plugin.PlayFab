using PlayFab.ProfilesModels;

namespace Plugin.PlayFab;

internal partial class Profile
{
    [HTTP("POST", "/Profile/GetProfiles")]
    [HTTP("POST", "/Profile/GetProfiles?{!args}")]
    public static bool GetProfiles(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<GetEntityProfilesRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        return server.SendSuccess<GetEntityProfilesResponse>();
    }
}
