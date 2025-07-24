using PlayFab.MultiplayerModels;


namespace Plugin.PlayFab;

internal partial class Lobby
{
    [HTTP("POST", "/Lobby/FindLobbies")]
    [HTTP("POST", "/Lobby/FindLobbies?{!args}")]
    public static bool FindLobbies(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<FindLobbiesRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        Console.WriteLine("Filter for Lobby: " + request.Filter);
        return server.SendSuccess<FindLobbiesResult>(new()
        {
            Lobbies = [],
            Pagination =
            { 
                ContinuationToken = string.Empty,
                TotalMatchedLobbyCount = 0,
            }
        });
    }

    [HTTP("POST", "/Lobby/FindFriendLobbies")]
    [HTTP("POST", "/Lobby/FindFriendLobbies?{!args}")]
    public static bool FindFriendLobbies(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<FindFriendLobbiesRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        Console.WriteLine("Filter for Lobby: " + request.Filter);
        return server.SendSuccess<FindLobbiesResult>(new()
        {
            Lobbies = [],
            Pagination =
            {
                ContinuationToken = string.Empty,
                TotalMatchedLobbyCount = 0,
            }
        });
    }
}
