using PlayFab.ProfilesModels;

namespace Plugin.PlayFab;

internal partial class Profile
{
    [HTTP("POST", "/Profile/GetGlobalPolicy")]
    [HTTP("POST", "/Profile/GetGlobalPolicy?{!args}")]
    public static bool GetGlobalPolicy(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<GetGlobalPolicyRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        return server.SendSuccess<GetGlobalPolicyResponse>();
    }
}
