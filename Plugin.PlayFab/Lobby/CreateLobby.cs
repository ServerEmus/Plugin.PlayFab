using PlayFab.MultiplayerModels;


namespace Plugin.PlayFab;

internal partial class Lobby
{
    [HTTP("POST", "/Lobby/CreateLobby")]
    [HTTP("POST", "/Lobby/CreateLobby?{!args}")]
    public static bool CreateLobby(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<CreateLobbyRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        string LobbyId = Guid.NewGuid().ToString();
        DBFabLobby.Create(new()
        { 
            Lobby = new()
            { 
                AccessPolicy = request.AccessPolicy ??= AccessPolicy.Public,
                SearchData = request.SearchData,
                ChangeNumber = 1,
                LobbyData = request.LobbyData,
                MaxPlayers = request.MaxPlayers,
                Owner = request.Owner,
                Members = request.Members,
                OwnerMigrationPolicy = request.OwnerMigrationPolicy,
                UseConnections = request.UseConnections,
                MembershipLock = MembershipLock.Unlocked,
                RestrictInvitesToLobbyOwner = request.RestrictInvitesToLobbyOwner,
                LobbyId = LobbyId,
                ConnectionString = LobbyId,
            }
        });

        return server.SendSuccess<CreateLobbyResult>(new()
        {
            LobbyId = LobbyId,
            ConnectionString = LobbyId
        });
    }
}
