using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sandbox;

namespace Sandmod.Permission;

/// <summary>
/// Basic implementation of the <see cref="IPermissionComponent"/>.
/// <br/>
/// See <see cref="IPermissionComponent"/> for more information:
/// <br/><br/>
/// <inheritdoc cref="IPermissionComponent"/>
/// </summary>
public abstract partial class PermissionComponent : EntityComponent, IPermissionComponent
{
    /// <summary>
    /// Default property for the <see cref="IClient"/>'s permissions.
    /// <br/>
    /// It's networked to allow for replication on the client for e.g. UI checks.
    /// </summary>
    [Net, Local] protected List<string> InternalPermissions { get; set; } = new ();
    
    public virtual IReadOnlyCollection<string> Permissions => InternalPermissions.AsReadOnly();

}