using PlayFab.MultiplayerModels;

namespace Plugin.PlayFab.MultiplayerServer;

internal partial class MultiplayerServer
{
    [HTTP("POST", "/MultiplayerServer/ListPartyQosServers?{!args}")]
    public static bool ListPartyQosServers(ServerSender server)
    {
        return server.SendSuccess<ListPartyQosServersResponse>(new()
        {
            PageSize = 1,
            QosServers =
            [
                new()
                {
                    ServerUrl = "127.0.0.1:6666",
                    Region = "WestEurope"
                }
            ]
        });
    }
}
