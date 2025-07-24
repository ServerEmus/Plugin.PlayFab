using PlayFab.ClientModels;

namespace Plugin.PlayFab;

internal partial class Client
{
    [HTTP("POST", "/Client/GetPlayFabIDsFromSteamIDs?{!args}")]
    public static bool GetPlayFabIDsFromSteamIDs(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<GetPlayFabIDsFromSteamIDsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        if (request.SteamStringIDs.Count == 0)
            return server.SendError(new()
            { 
                HttpCode = 400,
                HttpStatus = "BadRequest",
                Error = PF.PlayFabErrorCode.InvalidParams,
                ErrorMessage = "Invalid input paramters",
                ErrorDetails =
                {
                    { "", ["One of the following properties must be defined: SteamIDs, SteamStringIDs"] }
                }
            });
        return server.SendSuccess<GetPlayFabIDsFromSteamIDsResult>(new()
        {
            Data = []
        });
    }
}
