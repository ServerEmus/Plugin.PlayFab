using PlayFab.ClientModels;

namespace Plugin.PlayFab;

internal partial class Client
{
    [HTTP("POST", "/Client/GetCatalogItems?{!args}")]
    public static bool GetCatalogItems(ServerSender server)
    {
        return server.SendSuccess<GetCatalogItemsResult>(new());
    }
}
