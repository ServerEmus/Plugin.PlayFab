using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/GetItemReviewSummary")]
    [HTTP("POST", "/Catalog/GetItemReviewSummary?{!args}")]
    public static bool GetItemReviewSummary(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<GetItemReviewSummaryRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<GetItemReviewSummaryResponse>(new());
    }
}
