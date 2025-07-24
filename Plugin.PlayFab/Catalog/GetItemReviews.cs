using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/GetItemReviews")]
    [HTTP("POST", "/Catalog/GetItemReviews?{!args}")]
    public static bool GetItemReviews(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<GetItemReviewsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<GetItemReviewsResponse>(new());
    }
}
