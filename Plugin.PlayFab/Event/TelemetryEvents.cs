using PlayFab.EventsModels;
using System.Security.Cryptography;

namespace Plugin.PlayFab;

internal partial class Event
{
    [HTTP("POST", "/Event/WriteTelemetryEvents")]
    [HTTP("POST", "/Event/WriteTelemetryEvents?{!args}")]
    public static bool WriteTelemetryEvents(ServerSender server)
    {
        var request = JsonSerializer.Deserialize<WriteEventsRequest>(server.Request.Body);
        if (server.ReturnIfNull(request))
            return true;
        List<string> ids = [];
        for (int i = 0; i < request.Events.Count; i++)
        {
            ids.Add(RandomNumberGenerator.GetString("0123456789abcdef", 32));
        }

        return server.SendSuccess<WriteEventsResponse>(new()
        {
            AssignedEventIds = ids
        });
    }
}
