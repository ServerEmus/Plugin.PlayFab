using PlayFab.ProfilesModels;

namespace Plugin.PlayFab;

internal partial class Profile
{
    [HTTP("POST", "/Profile/SetProfilePolicy")]
    [HTTP("POST", "/Profile/SetProfilePolicy?{!args}")]
    public static bool SetProfilePolicy(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<SetEntityProfilePolicyRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        return server.SendSuccess<SetEntityProfilePolicyResponse>();
    }
}
