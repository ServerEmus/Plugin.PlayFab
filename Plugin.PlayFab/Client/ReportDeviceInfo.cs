using PlayFab.ClientModels;

namespace Plugin.PlayFab;

internal partial class Client
{
    [HTTP("POST", "/Client/ReportDeviceInfo?{!args}")]
    public static bool ReportDeviceInfo(ServerSender server)
    {
        return server.SendSuccess<EmptyResponse>(new());
    }
}
