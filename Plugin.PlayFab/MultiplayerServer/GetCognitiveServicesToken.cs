namespace Plugin.PlayFab.MultiplayerServer;

internal partial class MultiplayerServer
{
    [HTTP("POST", "/MultiplayerServer/GetCognitiveServicesToken?{!args}")]
    public static bool GetCognitiveServicesToken(ServerSender server)
    {
        return server.SendError(new()
        {
            Error = PF.PlayFabErrorCode.MultiplayerServerUnavailable,
            HttpCode = 404,
            HttpStatus = "NotFound",
            ErrorMessage = "Nah"
        });
    }
}
