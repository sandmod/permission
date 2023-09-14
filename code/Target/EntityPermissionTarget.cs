using Sandbox;

namespace Sandmod.Permission.Target;

/// <summary>
/// Default implementation of the <see cref="IPermissionTarget"/> for <see cref="IEntity"/>.<br/>
/// </summary>
/// <remarks>
/// This is a wrapper around the <see cref="IEntity"/>.
/// </remarks>
public sealed class EntityPermissionTarget : IPermissionTarget
{
    private readonly IEntity Entity;

    private EntityPermissionTarget(IEntity entity)
    {
        Entity = entity;
    }

    public object TargetObject => Entity;

    public string PermissionString => Entity is IClient client
        ? client.SteamId.ToString()
        : (Entity as Entity)?.Name ?? Entity.Id.ToString();

    public static implicit operator EntityPermissionTarget(Entity entity)
    {
        return new EntityPermissionTarget(entity);
    }

    /// <summary>
    /// Creates a <see cref="EntityPermissionTarget"/> for the <see cref="IEntity"/>.
    /// </summary>
    /// <param name="entity"><see cref="IEntity"/> to create a <see cref="EntityPermissionTarget"/> for</param>
    public static EntityPermissionTarget From(IEntity entity)
    {
        return new EntityPermissionTarget(entity);
    }
}