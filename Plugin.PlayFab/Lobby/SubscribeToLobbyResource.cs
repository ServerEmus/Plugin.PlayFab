using PlayFab.MultiplayerModels;

namespace Plugin.PlayFab;

internal partial class Lobby
{
    [HTTP("POST", "/Lobby/SubscribeToLobbyResource")]
    [HTTP("POST", "/Lobby/SubscribeToLobbyResource?{!args}")]
    public static bool SubscribeToLobbyResource(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<SubscribeToLobbyResourceRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;;
        return server.SendSuccess<SubscribeToLobbyResourceResult>(new()
        {
            Topic = $"{request.SubscriptionVersion}~lobby~{request.Type}-{request.ResourceId}",
        });
    }
}
