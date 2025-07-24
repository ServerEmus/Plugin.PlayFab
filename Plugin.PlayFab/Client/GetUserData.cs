using PlayFab.ClientModels;

namespace Plugin.PlayFab;

internal partial class Client
{
    [HTTP("POST", "/Client/GetUserData?{!args}")]
    public static bool GetUserData(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<GetUserDataRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        var fabUser = DBFabUser.GetOne(x => x.TitleAccountId == token.Value.TitleAccountId && x.TitleId == token.Value.TitleId);
        if (server.ReturnIfNull(fabUser))
            return true;
        DBFabUser.Update(fabUser);
        Dictionary<string, UserDataRecord> Data = [];
        foreach (var item in fabUser.CustomData)
        {
            if (request.Keys.Contains(item.Key))
                Data.Add(item.Key, item.Value);
        }
        return server.SendSuccess<GetUserDataResult>(new()
        {
            DataVersion = fabUser.DataVersion,
            Data = Data
        });
    }
}
