﻿using System.Linq;
using Sandbox;

namespace Sandmod.Permission;

public static class PermissionExtensions
{
    /// <summary>
    /// Get the <see cref="IPermissionComponent"/> of the <see cref="IClient"/>.
    /// </summary>
    /// <typeparam name="T">The <see cref="IPermissionComponent"/> type</typeparam>
    /// <returns>The <see cref="IPermissionComponent"/> of the <see cref="IClient"/></returns>
    public static T GetPermissionComponent<T>(this IClient self) where T : IPermissionComponent
    {
        return self.Components.Get<T>();
    }

    /// <summary>
    /// Checks if the <see cref="IClient"/> has the <b><paramref name="permission"/></b>.
    /// </summary>
    /// <param name="permission">Permission to check</param>
    /// <returns>If the <see cref="IClient"/> has the <b><paramref name="permission"/></b></returns>
    public static bool HasPermission(this IClient self, string permission)
    {
        return self.Components.GetAll<IPermissionComponent>().Any(component => component.HasPermission(permission));
    }
}