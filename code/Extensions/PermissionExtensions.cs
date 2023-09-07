using System.Linq;
using Sandbox;

namespace Sandmod.Permission;

public static class PermissionExtensions
{
    /// <summary>
    /// Checks if the <see cref="IClient"/> has the <b><paramref name="permission"/></b>.
    /// </summary>
    /// <param name="permission">Permission to check</param>
    /// <returns>If the <see cref="IClient"/> has the <b><paramref name="permission"/></b></returns>
    public static bool HasPermission(this IClient self, string permission)
    {
        return Permission.Allows(self.Components.GetAll<IPermissionComponent>()
            .Select(component => component.CheckPermission(permission)).ToList());
    }

    /// <summary>
    /// Transforms a "granted" boolean to a <see cref="Permission.Result"/>.
    /// </summary>
    public static Permission.Result ToResult(this bool granted)
    {
        return granted ? Permission.Result.Grant : Permission.Result.NoGrant;
    }
}