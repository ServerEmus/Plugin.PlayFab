using PlayFab.ClientModels;

namespace Plugin.PlayFab;

internal partial class Client
{
    [HTTP("POST", "/Client/AcceptTrade?{!args}")]
    public static bool AcceptTrade(ServerSender server)
    {
        foreach (var item in server.Parameters)
        {
            Console.WriteLine(item.Key + " " + item.Value);
        }
        _ = JsonSerializer.Deserialize<AcceptTradeRequest>(server.Request.Body);
        server.Response.MakeOkResponse(204);
        server.SendResponse();
        return true;
    }
}
