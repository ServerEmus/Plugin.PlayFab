using PlayFab.ClientModels;

namespace Plugin.PlayFab;

internal partial class Client
{

    [HTTP("POST", "/Client/GetFriendsList?{!args}")]
    public static bool GetFriendsList(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<GetFriendsListRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        return server.SendSuccess<GetFriendsListResult>(new()
        {
            Friends = []
        });
    }
}
