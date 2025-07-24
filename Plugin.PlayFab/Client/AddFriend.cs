using PlayFab.ClientModels;

namespace Plugin.PlayFab;

internal partial class Client
{
    [HTTP("POST", "/Client/AddFriend?{!args}")]
    public static bool AddFriend(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<AddFriendRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        return server.SendSuccess<AddFriendResult>(new()
        {
        });
    }
}
