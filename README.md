<div align="center">
  <img alt="Sandmod Permission" height="300px" src="https://files.facepunch.com/sbox/asset/sandmod.permission/logo.9529a05a.png">
</div>

![Discord](https://img.shields.io/discord/1018463122144636980?style=for-the-badge&label=Discord&color=3273EB)
![GitHub Repo stars](https://img.shields.io/github/stars/sandmod/permission?style=for-the-badge&logoColor=3273EB&color=3273EB)
![Asset.Party References](https://img.shields.io/badge/dynamic/xml?url=https%3A%2F%2Fasset.party%2Fsandmod%2Fpermission&query=%2F%2Fdiv%5Btext()%3D%22Referenced%22%5D%2Fparent%3A%3Adiv%2Fdiv%5Bcontains(%40class%2C%20'value')%5D&suffix=%20References&style=for-the-badge&label=asset.party&color=3273EB)![Asset.Party Collections](https://img.shields.io/badge/dynamic/xml?url=https%3A%2F%2Fasset.party%2Fsandmod%2Fpermission&query=%2F%2Fdiv%5Bcontains(%40class%2C%20'collections')%5D%2Fspan%5Bcontains(%40class%2C%20'count')%5D&suffix=%20Collections&style=for-the-badge&label=&color=3273EB)![Asset.Party Likes](https://img.shields.io/badge/dynamic/xml?url=https%3A%2F%2Fasset.party%2Fsandmod%2Fpermission&query=%2F%2Fdiv%5Bcontains(%40class%2C%20'rate_like')%5D%2Fspan&suffix=%20Likes&style=for-the-badge&label=&color=3273EB)![Asset.Party Favourites](https://img.shields.io/badge/dynamic/xml?url=https%3A%2F%2Fasset.party%2Fsandmod%2Fpermission&query=%2F%2Fdiv%5Bcontains(%40class%2C%20'favourite')%5D%2Fspan%5Bcontains(%40class%2C%20'count')%5D&suffix=%20Favourites&style=for-the-badge&label=&color=3273EB)

> An abstract and shared permission framework

> Sandmod provides all kinds of frameworks for developers.  
> This way we can all have on a shared experience where addon incompatibility is no issue anymore.

Sandmod Permission is an abstract permission framework based on providers and components.

You have the freedom to choose how you want to provide and check the permission.  
Here are some possible and funky examples how you could provide permissions:
* Retrieved from a database
* Based on ranks / groups / roles
* Based on inventory items
* Based on play time
* Based on suicide count
* Probably anything...

## The library

The library is split into 2 parts.
* Providing permissions and permission check logic
* Checking permissions

The permission checking part is probably the most used one as this is the main point of the library.  
If you are not sure how permissions are defined or you want get everything out of your permissions, check out the [Permission definition](#permission-definition) section.

### Checking permissions

If you want to check if a client is allowed to do something, you just want to know if they do or not (a simple true / false).  
You probably don't care about how it's defined that the client is allowed to, because others should take care of it.

For this case, all you need to do is use the `bool IClient.HasPermission(string permission)` or `bool IClient.HasPermission(string permission, IPermissionTarget target)` method.

### Providing permissions

This is the more technical and complex part of the library, but you only need it if you actually want to provide permissions yourself.

The main component are the [IPermissionProvider](code/Provider/IPermissionProvider.cs) and the [IPermissionComponent](code/Components/IPermissionComponent.cs).

The [IPermissionProvider](code/Provider/IPermissionProvider.cs) is used to provided the [IPermissionComponents](code/Components/IPermissionComponent.cs) to the client.  
Check out the [DefaultPermissionProvider](code/Provider/DefaultPermissionProvider.cs) for an basic example.

The [IPermissionComponent](code/Components/IPermissionComponent.cs) represents partial permissions of a client.
> It's partial because a client can have multiple components on them and they are all taken into account when checking if the client has a specific permission.


The [IPermissionComponent](code/Components/IPermissionComponent.cs) is easily implementable and allows you to create your own logic for providing and checking your own permissions.  
The permissions inside the [IPermissionComponent](code/Components/IPermissionComponent.cs) should be replicated to allow permission checks on the client side.  
Check out the [DefaultPermissionComponent](code/Components/DefaultPermissionComponent.cs) for an basic example.

### Permission definition

Permissions are mostly defined as lowercase string in a `.` notation.  
This allows for grouping of the permission sections.

| Example permission         | Included permissions                                            |
|:---------------------------|:----------------------------------------------------------------|
| `example`                  | `example`                                                       |
| `example.permission`       | `example`<br>`example.permission`                               |
| `example.permission.other` | `example`<br>`example.permission`<br>`example.permission.other` |   

**In general it's recommended to start permissions with a unique name, to prevent permission conflicts with other addons (e.g. `addon.example.permission`).**

By grouping permissions it's important to group them properly else unwanted behaviour might happen with wildcards.  
Here is a bad example:

| Permissions of the client | Checked permissions                              | Allowed permissions                             |
|:--------------------------|:-------------------------------------------------|:------------------------------------------------|
| `kick.*`                  | `kick.client1`<br>`kick.client2`<br>`kick.bot1`  | `kick.client1`<br>`kick.client2`<br>`kick.bot1` |

The above example doesn't allow you to have separate wildcards for the permissions for the clients and bots.  
If you wanted to do this properly, you can do it like this:

| Permissions of the client | Checked permissions                                               | Allowed permissions                                               |
|:--------------------------|:------------------------------------------------------------------|:------------------------------------------------------------------|
| `kick.client.*`           | `kick.client.client1`<br>`kick.client.client2`<br>`kick.bot.bot1` | `kick.client.client1`<br>`kick.client.client2`                    |
| `kick.bot.*`              | `kick.client.client1`<br>`kick.client.client2`<br>`kick.bot.bot1` | `kick.bot.bot1`                                                   |
| `kick.*`                  | `kick.client.client1`<br>`kick.client.client2`<br>`kick.bot.bot1` | `kick.client.client1`<br>`kick.client.client2`<br>`kick.bot.bot1` |

## Setup

To add this library to your s&box editor, simply clone the [Sandmod Permission](https://asset.party/sandmod/permission) project from [asset.party](https://asset.party/sandmod/permission).

## Examples

Check out the [DefaultPermissionProvider](code/Provider/DefaultPermissionProvider.cs) for an basic example.  
Check out the [DefaultPermissionComponent](code/Components/DefaultPermissionComponent.cs) for an basic example.

## Upcoming additions

Maybe a permission template registry will be added to allow permission configuration tools to list used permissions with their parameters.

## Contribution

If you think something is missing or can be optimized feel free to create an issue or pull request.

