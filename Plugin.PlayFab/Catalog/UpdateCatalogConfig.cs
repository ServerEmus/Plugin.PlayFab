using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/UpdateCatalogConfig")]
    [HTTP("POST", "/Catalog/UpdateCatalogConfig?{!args}")]
    public static bool UpdateCatalogConfig(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<UpdateCatalogConfigRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<UpdateCatalogConfigResponse>(new());
    }
}
