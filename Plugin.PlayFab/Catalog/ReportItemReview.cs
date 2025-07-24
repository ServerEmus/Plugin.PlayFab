using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/ReportItemReview")]
    [HTTP("POST", "/Catalog/ReportItemReview?{!args}")]
    public static bool ReportItemReview(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<ReportItemReviewRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<ReportItemReviewResponse>(new());
    }
}
