using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/ReviewItem")]
    [HTTP("POST", "/Catalog/ReviewItem?{!args}")]
    public static bool ReviewItem(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<ReviewItemRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<ReviewItemResponse>(new());
    }
}
