using System.Collections.Generic;
using Sandbox;
using Sandmod.Core.Client;
using Sandmod.Core.Provider;

namespace Sandmod.Permission.Provider;

/// <summary>
/// Default implementation of the <see cref="IPermissionProvider"/>.
/// <br/>
/// See <see cref="IPermissionProvider"/> for more information:
/// <br/><br/>
/// <inheritdoc cref="IPermissionProvider"/>
/// </summary>
[DefaultProvider(ProviderPriority.Internal)]
public sealed class DefaultPermissionProvider : IOfflinePermissionProvider
{
    public IReadOnlyCollection<IPermissionComponent> ProvideComponents(IClient client)
    {
        // TODO maybe replace with an actually useful default implementation
        return new List<IPermissionComponent>{ new DefaultPermissionComponent(new List<string>() { "*" }) };
    }

    public Permission.Result CheckPermission(OfflineClient client, string permission)
    {
        throw new System.NotImplementedException();
    }
}