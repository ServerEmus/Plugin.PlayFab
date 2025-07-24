using Plugin.PlayFab.Models;
using ServerShared.Database;

namespace Plugin.PlayFab.Managers;

internal static class DataBaseManager
{
    public static DataBaseConnection<FabUser> DBFabUser;
    public static DataBaseConnection<FabLobby> DBFabLobby;
    public static DataBaseConnection<FabTitle> DBFabTitle;
    public static DataBaseConnection<FabGroup> DBFabGroup;

    static DataBaseManager()
    {
        DBFabUser = new("Users");
        DBFabLobby = new("Lobbies");
        DBFabTitle = new("Titels");
        DBFabGroup = new("Groups");
    }
}
