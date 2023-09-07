using Sandmod.Core.Client;

namespace Sandmod.Permission.Provider;

/// <summary>
/// Extends <see cref="IPermissionProvider"/>.<br/>
/// Allows for permission checks on <see cref="OfflineClient"/>s.<br/>
/// See <see cref="IPermissionProvider"/> for more information:
/// <br/><br/>
/// <inheritdoc cref="IPermissionProvider"/>
/// </summary>
public interface IOfflinePermissionProvider : IPermissionProvider
{
    /// <summary>
    /// Checks the permissions of the <b><param name="offlineClient"></param></b> for the <b><paramref name="permission"/></b>.
    /// </summary>
    /// <param name="offlineClient">Offline client to check</param>
    /// <param name="permission">Permission to check</param>
    /// <returns>If the <b><paramref name="permission"/></b> is granted, not granted or denied</returns>
    Permission.Result CheckPermission(OfflineClient offlineClient, string permission);
}