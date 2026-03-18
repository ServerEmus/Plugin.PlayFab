using ModdableWebServer.Interfaces;
using ServerShared.Controllers;
using ServerShared.Plugins;
using System.Reflection;

namespace Plugin.PlayFab;

public class PlayFabPlugin : ServerPlugin
{
    public override uint Priority => 0;

    public override string Name => "PlayFab";

    public override void Start()
    {
        var server = ServerController.Servers.FirstOrDefault(static x => x.Port == 443);
        if (server == null)
            return;

        if (server.Server is not IHttpServer http)
            return;

        http.HTTPAttributeToMethods.Merge(Assembly.GetAssembly(typeof(PlayFabPlugin)));
    }

    public override void Stop()
    {
        
    }
}
