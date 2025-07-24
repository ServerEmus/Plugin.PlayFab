using PlayFab.ClientModels;

namespace Plugin.PlayFab;

internal partial class Client
{
    [HTTP("POST", "/Client/UpdateUserTitleDisplayName")]
    [HTTP("POST", "/Client/UpdateUserTitleDisplayName?{!args}")]
    public static bool UpdateUserTitleDisplayName(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<UpdateUserTitleDisplayNameRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        var fabUser = DBFabUser.GetOne(x => x.TitleAccountId == token.Value.TitleAccountId && x.TitleId == token.Value.TitleId);
        if (server.ReturnIfNull(fabUser))
            return true;
        fabUser.DisplayName = request.DisplayName;
        DBFabUser.Update(fabUser);
        return server.SendSuccess<UpdateUserTitleDisplayNameResult>(new()
        { 
            DisplayName = request.DisplayName,
        });
    }

    [HTTP("POST", "/Client/UpdateUserData?{!args}")]
    public static bool UpdateUserData(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<UpdateUserDataRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        var fabUser = DBFabUser.GetOne(x => x.TitleAccountId == token.Value.TitleAccountId && x.TitleId == token.Value.TitleId);
        if (server.ReturnIfNull(fabUser))
            return true;
        foreach (var item in request.Data)
        {
            fabUser.CustomData.Add(item.Key, new()
            { 
                LastUpdated = DateTime.Now,
                Permission = request.Permission,
                Value = item.Value
            });
        }
        foreach (var key in request.KeysToRemove)
        {
            fabUser.CustomData.Remove(key);
        }
        fabUser.DataVersion++;
        DBFabUser.Update(fabUser);
        return server.SendSuccess<UpdateUserDataResult>(new()
        {
            DataVersion = fabUser.DataVersion,
        });
    }
}
