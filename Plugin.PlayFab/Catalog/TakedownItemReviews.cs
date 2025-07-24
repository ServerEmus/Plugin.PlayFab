using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/TakedownItemReviews")]
    [HTTP("POST", "/Catalog/TakedownItemReviews?{!args}")]
    public static bool TakedownItemReviews(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<TakedownItemReviewsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<TakedownItemReviewsResponse>(new());
    }
}
