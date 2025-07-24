namespace Plugin.PlayFab.Models;

public class FabLobby
{
    [LiteDB.BsonId]
    public int DataBaseId { get; set; }
    public global::PlayFab.MultiplayerModels.Lobby Lobby { get; set; } = new();
}
