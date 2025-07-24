using PlayFab.Internal;
using PlayFab;
using PlayFab.ClientModels;

namespace Plugin.PlayFab;

internal partial class Client
{
    [HTTP("POST", "/Client/LoginWithCustomID?{!args}")]
    public static bool LoginWithCustomID(ServerSender server)
    {
        foreach (var item in server.Parameters)
        {
            Console.WriteLine(item.Key + " " + item.Value);
        }
        Console.WriteLine(server.Request.Body);
        //_ = JsonSerializer.Deserialize<LoginWithCustomIDRequest>(server.Request.Body);
        /*
        var ret = new PlayFabJsonSuccess<LoginResult>()
        {
            data = new()
            {
                EntityToken = new()
                { 
                    Entity = new()
                    { 
                        Id = "CUSTOMID",
                        Type = "title_player_account"
                    }
                }
            },
            code = 200,
            status = "OK"
        };
        /*
        var error = new PlayFabError()
        {
            HttpCode = 400,
            Error = PlayFabErrorCode.AccountNotFound,
            ErrorMessage = "User not found",
            HttpStatus = "BadRequest",
        };
        */
        server.Response.MakeOkResponse(204);
        server.SendResponse();
        return true;
    }
}
