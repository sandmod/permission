using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sandbox;

namespace Sandmod.Permission;

/// <summary>
/// Extendable permission component representing partial permissions of an <see cref="IClient"/>,
/// checked via the <see cref="IClient"/>'s <see cref="PermissionExtensions.HasPermission"/> method.
/// <br/><br/>
/// <b>IMPORTANT!</b>
/// <br/>
/// For this component to be taken into account on the <see cref="IClient"/>'s <see cref="PermissionExtensions.HasPermission"/> check,
/// the component needs to be added to the <see cref="IClient"/>'s <see cref="IClient.Components"/>.
/// <br/><br/>
/// It can be used somewhere else as well, for your own purposes but it won't be taken into account on the <see cref="IClient"/>'s <see cref="PermissionExtensions.HasPermission"/> check.
/// </summary>
public abstract partial class PermissionComponent : EntityComponent, IEnumerable<string>
{
    /// <summary>
    /// Default property for the <see cref="IClient"/>'s permissions.
    /// <br/>
    /// It's networked to allow for replication on the client for e.g. UI checks.
    /// </summary>
    [Net] protected List<string> InternalPermissions { get; set; } = new ();
    
    /// <summary>
    /// <see cref="IClient"/>'s permissions of this <see cref="PermissionComponent"/>.
    /// </summary>
    public virtual IReadOnlyCollection<string> Permissions => InternalPermissions.AsReadOnly();

    /// <summary>
    /// Checks if the <see cref="PermissionComponent"/>'s permissions for the owning <see cref="IClient"/> allow the <b><paramref name="permission"/></b>.
    /// </summary>
    /// <param name="permission">Permission to check</param>
    /// <returns>If the <b><paramref name="permission"/></b> is allowed</returns>
    public virtual bool HasPermission(string permission)
    {
        return Permission.Allows(Permissions, permission);
    }
    
    /// <summary>
    /// Enumerator of the <see cref="PermissionComponent"/>'s permissions.
    /// </summary>
    public IEnumerator<string> GetEnumerator()
    {
        return Permissions.GetEnumerator();
    }

    /// <summary>
    /// Enumerator of the <see cref="PermissionComponent"/>'s permissions.
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return Permissions.GetEnumerator();
    }
}