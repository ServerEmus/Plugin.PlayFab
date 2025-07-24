using PlayFab;
using PlayFab.Internal;
using PlayFab.Json;
using Plugin.PlayFab.Models;
using System.Diagnostics.CodeAnalysis;

namespace Plugin.PlayFab.Extensions;

public static class ServerStructExt
{
    public static (FabId PlayFabId, FabId GameId, FabId TitleAccountId, string TitleId)? GetSessionInfoFromServer(this ServerSender sender)
    {
        string cred = string.Empty;
        var ftoken = GetPlayFabToken(sender);
        if (ftoken != null)
            cred = ftoken.EntityCredentials.Replace("title_player_account!", string.Empty);
        var etoken = GetEntityToken(sender);
        if (etoken != null)
            cred = etoken;
        if (string.IsNullOrEmpty(cred))
            return null;
        return FabUserExt.GetSessionInfo(cred);
    }

    public static FabEntityToken? GetPlayFabToken(this ServerSender sender)
    {
        if (!sender.Headers.TryGetValue("x-authorization", out string? auth))
            return null;
        if (string.IsNullOrEmpty(auth))
            return null;
        return auth.GetFabEntityToken();
    }

    public static string? GetEntityToken(this ServerSender sender)
    {
        if (!sender.Headers.TryGetValue("x-entitytoken", out string? entitytoken))
            return null;
        if (string.IsNullOrEmpty(entitytoken))
            return null;
        return entitytoken;
    }

    public static bool ReturnIfNull<T>(this ServerSender sender, [NotNullWhen(false)] T? typeObject)
    {
        if (typeObject != null)
            return false;
        sender.Response.MakeGetResponse(PlayFabSimpleJson.SerializeObject(new PlayFabError()
        {
            HttpCode = 400,
            Error = PlayFabErrorCode.JsonParseError,
            ErrorMessage = "Json Parse Error",
            HttpStatus = "BadRequest",
        }));
        sender.SendResponse();
        return true;
    }

    public static bool SendSuccess<T>(this ServerSender sender, T data) where T : PlayFabResultCommon
    {
        sender.Response.MakeGetResponse(PlayFabSimpleJson.SerializeObject(new PlayFabJsonSuccess<T>()
        {
            code = 200,
            status = "OK",
            data = data
        }), "application/json");
        sender.SendResponse();
        return true;
    }

    public static bool SendSuccess<T>(this ServerSender sender) where T : PlayFabResultCommon, new()
    {
        sender.Response.MakeGetResponse(PlayFabSimpleJson.SerializeObject(new PlayFabJsonSuccess<T>()
        {
            code = 200,
            status = "OK",
            data = new()
        }), "application/json");
        sender.SendResponse();
        return true;
    }

    public static bool SendError(this ServerSender sender, PlayFabError error)
    {
        sender.Response.MakeGetResponse(PlayFabSimpleJson.SerializeObject(error), "application/json");
        sender.SendResponse();
        return true;
    }
}
