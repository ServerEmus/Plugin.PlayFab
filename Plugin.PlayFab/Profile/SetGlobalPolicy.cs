using PlayFab.ProfilesModels;

namespace Plugin.PlayFab;

internal partial class Profile
{
    [HTTP("POST", "/Profile/SetGlobalPolicy")]
    [HTTP("POST", "/Profile/SetGlobalPolicy?{!args}")]
    public static bool SetGlobalPolicy(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<SetGlobalPolicyRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        return server.SendSuccess<SetGlobalPolicyResponse>();
    }
}
