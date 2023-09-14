using System.Collections.Generic;
using Sandbox;
using Sandmod.Core.Provider;

namespace Sandmod.Permission.Provider;

/// <summary>
/// Default implementation of the <see cref="IPermissionProvider" />.
/// <br />
/// See <see cref="IPermissionProvider" /> for more information:
/// <br /><br />
/// <inheritdoc cref="IPermissionProvider" />
/// </summary>
[DefaultProvider(ProviderPriority.Internal)]
internal sealed class DefaultPermissionProvider : IPermissionProvider
{
    public IReadOnlyCollection<IPermissionComponent> Provide(IClient client)
    {
        // TODO maybe replace with an actually useful default implementation
        return new List<IPermissionComponent> {new DefaultPermissionComponent(new List<string> {"*"})};
    }
}