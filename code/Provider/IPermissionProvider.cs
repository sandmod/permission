using Sandbox;
using Sandmod.Core.Provider;

namespace Sandmod.Permission.Provider;

/// <summary>
/// Implementable permission provider.
/// <br/>
/// Provides <see cref="IPermissionComponent"/>s for <see cref="IClient"/>s.
/// <br/><br/>
/// <b>IMPORTANT!</b>
/// <br/>
/// Use the <see cref="ProviderAttribute"/> on your implementation to allow for auto-injection.
/// <br/>
/// See <see cref="ProviderAttribute"/> for more information.
/// </summary>
public interface IPermissionProvider : IComponentProvider<IPermissionComponent>
{
}