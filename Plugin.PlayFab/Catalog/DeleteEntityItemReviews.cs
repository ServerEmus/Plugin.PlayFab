using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/DeleteEntityItemReviews")]
    [HTTP("POST", "/Catalog/DeleteEntityItemReviews?{!args}")]
    public static bool DeleteEntityItemReviews(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<DeleteEntityItemReviewsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<DeleteEntityItemReviewsResponse>(new());
    }
}
