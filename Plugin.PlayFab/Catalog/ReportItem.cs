using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/ReportItem")]
    [HTTP("POST", "/Catalog/ReportItem?{!args}")]
    public static bool ReportItem(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<ReportItemRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<ReportItemResponse>(new());
    }
}
