using System.Collections.Generic;
using System.Linq;
using Sandbox;
using Sandmod.Core.Client;
using Sandmod.Core.Provider;
using Sandmod.Permission.Provider;

namespace Sandmod.Permission;

/// <summary>
/// 
/// </summary>
public static class PermissionSystem
{
    private static List<IPermissionProvider> _providers;

    [GameEvent.Entity.PostSpawn]
    [Event.Hotload]
    private static void Init()
    {
        if (!Game.IsServer) return;
        _providers = new List<IPermissionProvider>(ProviderFactory.Provide<IPermissionProvider>());
    }

    [GameEvent.Server.ClientJoined]
    private static void SetupClient(ClientJoinedEvent joinedEvent)
    {
        var client = joinedEvent.Client;
        var providerComponents = _providers.Select(provider => provider.ProvideComponents(client));
        foreach (var components in providerComponents)
        {
            foreach (var component in components)
            {
                client.Components.Add(component);
            }
        }
    }

    /// <summary>
    /// Checks if the <b><paramref name="client"/></b> has the <b><paramref name="permission"/></b>.
    /// </summary>
    /// <param name="client">Client to check</param>
    /// <param name="permission">Permission to check</param>
    /// <returns>If the <b><paramref name="client"/></b> has the <b><paramref name="permission"/></b></returns>
    public static bool HasPermission(IClient client, string permission)
    {
        return client.HasPermission(permission);
    }

    /// <summary>
    /// Checks if the <b><paramref name="offlineClient"/></b> has the <b><paramref name="permission"/></b>.
    /// <br/><br/>
    /// <b>IMPORTANT!</b><br/>
    /// Not all permission providers support offline permissions. Therefore the result might not be the same as if the client was online.
    /// <br/><br/>
    /// </summary>
    /// <param name="offlineClient">Offline client to check</param>
    /// <param name="permission">Permission to check</param>
    /// <returns>If the <b><paramref name="offlineClient"/></b> has the <b><paramref name="permission"/></b></returns>
    public static bool HasPermission(OfflineClient offlineClient, string permission)
    {
        return Permission.Allows(_providers.Where(provider => provider is IOfflinePermissionProvider)
            .Select(provider => ((IOfflinePermissionProvider) provider).CheckPermission(offlineClient, permission))
            .ToList());
    }
}