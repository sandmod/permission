using System.Collections.Generic;
using Sandbox;

namespace Sandmod.Permission;

/// <summary>
/// Implementable permission component representing partial permissions of an <see cref="IClient"/>,
/// checked via the <see cref="IClient"/>'s <see cref="PermissionExtensions.HasPermission"/> method.
/// <br/><br/>
/// <b>IMPORTANT!</b>
/// <br/>
/// For this component to be taken into account on the <see cref="IClient"/>'s <see cref="PermissionExtensions.HasPermission"/> check,
/// the component needs to be added to the <see cref="IClient"/>'s <see cref="IClient.Components"/>.
/// <br/><br/>
/// It can be used somewhere else as well, for your own purposes but it won't be taken into account on the <see cref="IClient"/>'s <see cref="PermissionExtensions.HasPermission"/> check.
/// </summary>
public interface IPermissionComponent : IComponent
{
    /// <summary>
    /// <see cref="IClient"/>'s permissions of this <see cref="IPermissionComponent"/>.
    /// <br/>
    /// Should be network replicated to the owning client [<see cref="LocalAttribute"/>].
    /// </summary>
    public abstract IReadOnlyCollection<string> Permissions { get; }

    /// <summary>
    /// Checks if the <see cref="IPermissionComponent"/>'s permissions for the owning <see cref="IClient"/> allow the <b><paramref name="permission"/></b>.
    /// </summary>
    /// <param name="permission">Permission to check</param>
    /// <returns>If the <b><paramref name="permission"/></b> is allowed</returns>
    public virtual bool HasPermission(string permission)
    {
        return Permission.Allows(Permissions, permission);
    }
}