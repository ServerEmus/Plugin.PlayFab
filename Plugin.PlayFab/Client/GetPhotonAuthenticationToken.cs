using PlayFab.ClientModels;
using System.Security.Cryptography;

namespace Plugin.PlayFab;

internal partial class Client
{
    [HTTP("POST", "/Client/GetPhotonAuthenticationToken?{!args}")]
    public static bool GetPhotonAuthenticationToken(ServerSender server)
    {
        return server.SendSuccess<GetPhotonAuthenticationTokenResult>(new()
        { 
            PhotonCustomAuthenticationToken = RandomNumberGenerator.GetString("0123456789abcdef", 32)
        });
    }
}
