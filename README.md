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

The main parts of the library are the [IPermissionProvider](code/Provider/IPermissionProvider.cs) and the [IPermissionComponent](code/Components/IPermissionComponent.cs).

The [IPermissionProvider](code/Provider/IPermissionProvider.cs) is used to provided the [IPermissionComponents](code/Components/IPermissionComponent.cs) to the client.  
Check out the [DefaultPermissionProvider](code/Provider/DefaultPermissionProvider.cs) for an basic example.

The [IPermissionComponent](code/Components/IPermissionComponent.cs) represents partial permissions of a client.
> It's partial because a client can have multiple components on them and they are all taken into account when checking if the client has a specific permission.

The [IPermissionComponent](code/Components/IPermissionComponent.cs) is easily implementable and allows you to create your own logic for providing and checking your own permissions.  
Check out the [DefaultPermissionComponent](code/Components/DefaultPermissionComponent.cs) for an basic example.

Use the `bool IClient.HasPermission(string permission)` method to check if the client has the passed permission.

## Setup

To add this library to your s&box editor, simply clone the [Sandmod Permission](https://asset.party/sandmod/permission) project from [asset.party](https://asset.party/sandmod/permission).

## Examples

Check out the [DefaultPermissionProvider](code/Provider/DefaultPermissionProvider.cs) for an basic example.  
Check out the [DefaultPermissionComponent](code/Components/DefaultPermissionComponent.cs) for an basic example.

