using Sandbox;

namespace Sandmod.Permission.Target;

/// <summary>
/// Implementable permission target.<br/>
/// Used in permission checks as target for the permissions.
/// <br/><br/>
/// E.g. if you want to check if someone is allowed to kick an <see cref="IClient"/>,
/// the "kick" permission would be checked the the <see cref="IClient"/> as <see cref="IPermissionTarget"/>.
/// </summary>
public interface IPermissionTarget
{
    /// <summary>
    /// Target object of the <see cref="IPermissionTarget"/>.
    /// </summary>
    /// <remarks>
    /// Might be the object itself or a sub-object if this is a wrapper.
    /// </remarks>
    public object TargetObject => this;

    /// <summary>
    /// String representation of the <see cref="IPermissionTarget"/>
    /// </summary>
    public string PermissionString { get; }
}