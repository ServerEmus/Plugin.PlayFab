namespace Plugin.PlayFab;

internal partial class Party
{
    [HTTP("POST", "/Party/RequestParty")]
    [HTTP("POST", "/Party/RequestParty?{!args}")]
    public static bool RequestParty(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<RequestPartyRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<RequestPartyResponse>(new()
        {
            PartyId = request.PartyId,
        });
    }
}
