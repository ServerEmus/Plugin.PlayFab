using PlayFab.LocalizationModels;

namespace Plugin.PlayFab;

internal partial class Locale
{
    [HTTP("POST", "/Profile/GetLanguageList")]
    [HTTP("POST", "/Profile/GetLanguageList?{!args}")]
    public static bool GetLanguageList(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<GetLanguageListRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        return server.SendSuccess<GetLanguageListResponse>();
    }
}
