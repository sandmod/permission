using System.Collections.Generic;
using System.Linq;
using Sandbox;
using Sandmod.Core.Client;
using Sandmod.Core.Provider;
using Sandmod.Permission.Provider;
using Sandmod.Permission.Target;

namespace Sandmod.Permission;

/// <summary>
/// Global permission system.
/// </summary>
public static class PermissionSystem
{
    private static List<IPermissionProvider> _providers;

    [GameEvent.Entity.PostSpawn]
    private static void Init()
    {
        if (!Game.IsServer) return;
        _providers = new List<IPermissionProvider>(ProviderFactory.Provide<IPermissionProvider>());
    }

    [Event.Hotload]
    private static void HotLoad()
    {
        if (!Game.IsServer) return;
        Init();
        foreach (var client in Game.Clients)
        {
            client.Components.RemoveAny<IPermissionComponent>();
            SetupClient(client);
        }
    }

    [GameEvent.Server.ClientJoined]
    private static void SetupClient(ClientJoinedEvent joinedEvent)
    {
        SetupClient(joinedEvent.Client);
    }

    private static void SetupClient(IClient client)
    {
        var providerComponents = _providers.Select(provider => provider.Provide(client));
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
    /// Checks if the <b><paramref name="client"/></b> has the <b><paramref name="permission"/></b> for the <b><paramref name="target"/></b>.
    /// </summary>
    /// <param name="client">Client to check</param>
    /// <param name="permission">Permission to check</param>
    /// <param name="target">Target to check the permissions for</param>
    /// <returns>If the <b><paramref name="client"/></b> has the <b><paramref name="permission"/></b></returns>
    public static bool HasPermission(IClient client, string permission, IPermissionTarget target)
    {
        return client.HasPermission(permission, target);
    }

    /// <summary>
    /// Checks if the <b><paramref name="offlineClient"/></b> has the <b><paramref name="permission"/></b>.
    /// <br/><br/>
    /// <b>IMPORTANT!</b><br/>
    /// Not all permission providers support offline permissions. Therefore the result might not be the same as if the client was online.
    /// <br/><br/>
    /// </summary>
    /// <remarks>Only available on the server</remarks>
    /// <param name="offlineClient">Offline client to check</param>
    /// <param name="permission">Permission to check</param>
    /// <returns>If the <b><paramref name="offlineClient"/></b> has the <b><paramref name="permission"/></b></returns>
    public static bool HasPermission(IOfflineClient offlineClient, string permission)
    {
        Game.AssertServer();
        return Permission.Allows(_providers.Where(provider => provider is IOfflinePermissionProvider)
            .Select(provider => ((IOfflinePermissionProvider) provider).CheckPermission(offlineClient, permission))
            .ToList());
    }

    /// <summary>
    /// Checks if the <b><paramref name="offlineClient"/></b> has the <b><paramref name="permission"/></b> for the <b><paramref name="target"/></b>.
    /// <br/><br/>
    /// <b>IMPORTANT!</b><br/>
    /// Not all permission providers support offline permissions. Therefore the result might not be the same as if the client was online.
    /// <br/><br/>
    /// </summary>
    /// <remarks>Only available on the server</remarks>
    /// <param name="offlineClient">Offline client to check</param>
    /// <param name="permission">Permission to check</param>
    /// <param name="target">Target to check the permissions for</param>
    /// <returns>If the <b><paramref name="offlineClient"/></b> has the <b><paramref name="permission"/></b></returns>
    public static bool HasPermission(IOfflineClient offlineClient, string permission, IPermissionTarget target)
    {
        Game.AssertServer();
        return Permission.Allows(_providers.Where(provider => provider is IOfflinePermissionProvider)
            .Select(provider =>
                ((IOfflinePermissionProvider) provider).CheckPermission(offlineClient, permission, target))
            .ToList());
    }
}