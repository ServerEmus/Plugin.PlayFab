using PlayFab.EconomyModels;

namespace Plugin.PlayFab;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/GetTransactionHistory")]
    [HTTP("POST", "/Inventory/GetTransactionHistory?{!args}")]
    public static bool GetTransactionHistory(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<GetTransactionHistoryRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        var token = server.GetSessionInfoFromServer();
        if (server.ReturnIfNull(token))
            return true;
        return server.SendSuccess<GetTransactionHistoryResponse>(new());
    }
}
