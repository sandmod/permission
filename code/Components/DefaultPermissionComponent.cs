using System.Collections.Generic;
using Sandbox;

namespace Sandmod.Permission;

/// <summary>
/// Default implementation of the <see cref="IPermissionComponent"/>.
/// <br/>
/// See <see cref="IPermissionComponent"/> for more information:
/// <br/><br/>
/// <inheritdoc cref="IPermissionComponent"/>
/// </summary>
public sealed partial class DefaultPermissionComponent : EntityComponent, IPermissionComponent
{
    /// <summary>
    /// Default property for <see cref="IClient"/>'s permissions.
    /// <br/>
    /// It's networked to allow for replication on the client for e.g. UI checks.
    /// </summary>
    [Net, Local] private List<string> Permissions { get; set; } = new List<string>();
    
    public DefaultPermissionComponent() {}

    public DefaultPermissionComponent(IReadOnlyCollection<string> permissions)
    {
        Permissions = new List<string>(permissions);
    }

    public Permission.Result CheckPermission(string permission)
    {
        return Permission.Grants(Permissions.AsReadOnly(), permission).ToResult();
    }
}