using System.Linq;
using Sandbox;
using Sandmod.Permission.Target;

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
    /// Checks if the <see cref="IClient"/> has the <b><paramref name="permission"/></b> for the <b><paramref name="target"/></b>.
    /// </summary>
    /// <param name="permission">Permission to check</param>
    /// <param name="target">Target to check the permissions for</param>
    /// <returns>If the <see cref="IClient"/> has the <b><paramref name="permission"/></b></returns>
    public static bool HasPermission(this IClient self, string permission, IPermissionTarget target)
    {
        return Permission.Allows(self.Components.GetAll<IPermissionComponent>()
            .Select(component => component.CheckPermission(permission, target)).ToList());
    }

    /// <inheritdoc cref="HasPermission(Sandbox.IClient,string,IPermissionTarget)"/>
    public static bool HasPermission(this IClient self, string permission, IClient target)
    {
        return HasPermission(self, permission, target);
    }

    /// <inheritdoc cref="HasPermission(Sandbox.IClient,string,IPermissionTarget)"/>
    public static bool HasPermission(this IClient self, string permission, IEntity target)
    {
        return HasPermission(self, permission, EntityPermissionTarget.From(target));
    }

    /// <summary>
    /// Transforms a "granted" boolean to a <see cref="Permission.Result"/>.
    /// </summary>
    public static Permission.Result ToResult(this bool granted)
    {
        return granted ? Permission.Result.Grant : Permission.Result.NoGrant;
    }
}