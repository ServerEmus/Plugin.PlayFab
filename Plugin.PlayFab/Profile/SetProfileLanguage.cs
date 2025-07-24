using PlayFab.ProfilesModels;

namespace Plugin.PlayFab;

internal partial class Profile
{
    [HTTP("POST", "/Profile/SetProfileLanguage")]
    [HTTP("POST", "/Profile/SetProfileLanguage?{!args}")]
    public static bool SetProfileLanguage(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<SetProfileLanguageRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        return server.SendSuccess<SetProfileLanguageResponse>();
    }
}
