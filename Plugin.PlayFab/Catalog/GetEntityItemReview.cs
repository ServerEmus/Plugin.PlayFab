using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/GetEntityItemReview")]
    [HTTP("POST", "/Catalog/GetEntityItemReview?{!args}")]
    public static bool GetEntityItemReview(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<GetEntityItemReviewRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<GetEntityItemReviewResponse>(new());
    }
}
