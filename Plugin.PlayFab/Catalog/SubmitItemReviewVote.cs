using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/SubmitItemReviewVote")]
    [HTTP("POST", "/Catalog/SubmitItemReviewVote?{!args}")]
    public static bool SubmitItemReviewVote(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<SubmitItemReviewVoteRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<SubmitItemReviewVoteResponse>(new());
    }
}
