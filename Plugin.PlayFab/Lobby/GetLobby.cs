using PlayFab.MultiplayerModels;


namespace Plugin.PlayFab;

internal partial class Lobby
{
    [HTTP("POST", "/Lobby/GetLobby")]
    [HTTP("POST", "/Lobby/GetLobby?{!args}")]
    public static bool GetLobby(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<GetLobbyRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;;
        var fabLobby = DBFabLobby.GetOne(x=>x.Lobby.LobbyId == request.LobbyId);
        if (server.ReturnIfNull(fabLobby))
            return true; ;
        return server.SendSuccess<GetLobbyResult>(new()
        {
            Lobby = fabLobby.Lobby,
        });
    }
}
