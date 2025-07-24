using PlayFab.ProfilesModels;

namespace Plugin.PlayFab;

internal partial class Profile
{
    [HTTP("POST", "/Profile/GetProfile")]
    [HTTP("POST", "/Profile/GetProfile?{!args}")]
    public static bool GetProfile(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<GetEntityProfileRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        return server.SendSuccess<GetEntityProfileResponse>();
    }
}
