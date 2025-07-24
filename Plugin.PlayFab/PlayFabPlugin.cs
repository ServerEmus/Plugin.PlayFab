using ServerShared.Controllers;
using ServerShared.Interfaces;
using System.Reflection;

namespace Plugin.PlayFab;

public class PlayFabPlugin : IPlugin
{
    public uint Priority => 0;

    public string Name => "PlayFab";

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public void Initialize()
    {
        var server = ServerController.Servers.FirstOrDefault(static x => x.Port == 443);
        if (server == null)
            return;
        server.Server!.HTTPAttributeToMethods.Merge(Assembly.GetAssembly(typeof(PlayFabPlugin)));
    }

    public void ShutDown()
    {
        
    }
}
