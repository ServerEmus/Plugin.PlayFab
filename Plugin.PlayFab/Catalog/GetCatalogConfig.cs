using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/GetCatalogConfig")]
    [HTTP("POST", "/Catalog/GetCatalogConfig?{!args}")]
    public static bool GetCatalogConfig(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<GetCatalogConfigRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<GetCatalogConfigResponse>(new());
    }
}
