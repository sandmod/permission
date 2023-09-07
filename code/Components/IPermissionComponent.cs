using Sandbox;
using Sandmod.Core.Client;
using Sandmod.Permission.Provider;

namespace Sandmod.Permission;

/// <summary>
/// Implementable permission component representing partial permissions of an <see cref="IClient"/>,
/// checked via the <see cref="IClient"/>'s <see cref="PermissionExtensions.HasPermission"/> method.
/// <br/><br/>
/// <b>IMPORTANT!</b>
/// <br/>
/// For this component to be taken into account on the <see cref="IClient"/>'s <see cref="PermissionExtensions.HasPermission"/> check,
/// the component needs to be added to the <see cref="IClient"/>'s <see cref="IClient.Components"/>.
/// <br/>
/// Create a <see cref="IPermissionProvider"/> to provide your own implementations.
/// <br/><br/>
/// It can be used somewhere else as well, for your own purposes but it won't be taken into account on the <see cref="IClient"/>'s <see cref="PermissionExtensions.HasPermission"/> check.
/// </summary>
public interface IPermissionComponent : IComponent
{
    /// <summary>
    /// Checks the <see cref="IPermissionComponent"/>'s permissions of the owning <see cref="IClient"/> for the <b><paramref name="permission"/></b>.
    /// </summary>
    /// <param name="permission">Permission to check</param>
    /// <returns>If the <b><paramref name="permission"/></b> is granted, not granted or denied</returns>
    public Permission.Result CheckPermission(string permission);
}