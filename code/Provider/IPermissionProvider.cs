using System.Collections.Generic;
using Sandbox;
using Sandmod.Core.Provider;

namespace Sandmod.Permission.Provider;

/// <summary>
/// Implementable permission provider.
/// <br />
/// Provides <see cref="IPermissionComponent" />s for <see cref="IClient" />s.
/// <br /><br />
/// <b>IMPORTANT!</b>
/// <br />
/// Use the <see cref="ProviderAttribute" /> on your implementation to allow for auto-injection.
/// <br />
/// See <see cref="ProviderAttribute" /> for more information.
/// </summary>
/// <remarks>The <see cref="IPermissionProvider"/> is only used on the server.</remarks>
public interface IPermissionProvider
{
    /// <summary>
    /// Provides IPermissionComponents for a <see cref="IClient" />.
    /// </summary>
    /// <param name="client">Client to provide components for</param>
    /// <returns>Provided components</returns>
    IReadOnlyCollection<IPermissionComponent> Provide(IClient client);
}