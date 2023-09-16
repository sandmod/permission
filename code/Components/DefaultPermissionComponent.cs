using System.Collections.Generic;
using Sandbox;
using Sandmod.Permission.Target;

namespace Sandmod.Permission;

/// <summary>
/// Default implementation of the <see cref="IPermissionComponent" />.<br />
/// See <see cref="IPermissionComponent" /> for more information:
/// <br /><br />
/// <inheritdoc cref="IPermissionComponent" />
/// </summary>
internal sealed partial class DefaultPermissionComponent : EntityComponent, IPermissionComponent
{
    public DefaultPermissionComponent()
    {
    }

    public DefaultPermissionComponent(IReadOnlyCollection<string> permissions)
    {
        Permissions = new List<string>(permissions);
    }

    /// <summary>
    /// Default property for <see cref="IClient" />'s permissions.<br />
    /// It's networked to allow for replication on the client for e.g. UI checks.
    /// </summary>
    [Net]
    [Local]
    private List<string> Permissions { get; set; } = new();

    public Permission.Result CheckPermission(string permission)
    {
        return Permission.Grants(Permissions.AsReadOnly(), permission).ToResult();
    }

    public Permission.Result CheckPermission(string permission, IPermissionTarget target)
    {
        return CheckPermission(permission + Permission.Separator + target.PermissionString);
    }
}