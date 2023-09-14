using System.Collections.Generic;
using System.Linq;

namespace Sandmod.Permission;

/// <summary>
/// Permission constants
/// </summary>
public static class Permission
{
    /// <summary>
    /// Separator splitting permissions into sections.
    /// <code>
    /// example.permission => [example, permission]
    /// </code>
    /// </summary>
    public const string Separator = ".";

    /// <summary>
    /// Inverter inverting grant permissions to be excluded.
    /// <code>
    /// example.permission.* == example.permission.something
    /// example.permission.* + !example.permission.something != example.permission.something
    /// </code>
    /// </summary>
    public const string Inverter = "!";

    /// <summary>
    /// Permission wildcards
    /// </summary>
    public static class WildCard
    {
        /// <summary>
        /// Wildcard matching a single permission section.
        /// <code>
        /// example._.permission == example.something.permission
        /// example._.permission != example.something.else.permission
        /// </code>
        /// </summary>
        public const string Single = "_";

        /// <summary>
        /// Wildcard matching a permission section and all it's sub-sections.
        /// <code>
        /// example.permission.* == example.permission.something
        /// example.permission.* == example.permission.something.else
        /// </code>
        /// </summary>
        public const string Any = "*";

        /// <summary>
        /// Wildcard matching the first permission section which it typically a unique addon identifer.
        /// <code>
        /// ?.permission == addon.permission
        /// ?.permission == permission
        /// ?.permission != something.else.permission
        /// </code>
        /// </summary>
        public const string Addon = "?";
    }

    public static bool IsInverted(string permission)
    {
        return permission.StartsWith(Inverter);
    }

    /// <summary>
    /// Checks if the <b><paramref name="permission"/></b> includes the <b><paramref name="checkPermission"/></b>.<br/>
    /// This check is valid for normal and inverted permissions.
    /// </summary>
    /// <param name="permission">Permission e.g. granted on a client</param>
    /// <param name="checkPermission">Permission to check if it's included in the <b><paramref name="permission"/></b></param>
    /// <returns>If the <b><paramref name="permission"/></b> includes the <b><paramref name="checkPermission"/></b></returns>
    public static bool Includes(string permission, string checkPermission)
    {
        var inverted = false;
        if (permission.StartsWith(Inverter))
        {
            // Remove the inverter at the start to allow for checking matches
            permission = permission[1..];
            inverted = true;
        }

        // Grant permission matches exactly with the check permission
        if (permission == checkPermission) return true;

        // Check sections split by the Permission.Separator
        var checkSections = checkPermission.Split(Separator).Where(section => section != "").ToArray();
        var grantSections = permission.Split(Separator).Where(section => section != "").ToArray();
        for (var i = 0; i < checkSections.Length; i++)
        {
            // Check permission sections are longer
            // The last inverted grant permission section includes itself and all sub-sections (similar as the Wildcard.ANY)
            // For none inverted grant permissions this is not the case
            if (grantSections.Length <= i) return inverted;

            var grantSection = grantSections[i];
            var checkSection = checkSections[i];

            // Grant permission includes any / no value for the first section
            if (i == 0 && grantSections.Length >= 2 && grantSection == WildCard.Addon)
            {
                var nextGrantSection = grantSections[i + 1];
                if (nextGrantSection != checkSection)
                {
                    checkSections = checkSections.Skip(1).ToArray();
                }

                grantSections = grantSections.Skip(1).ToArray();
                i--;
                continue;
            }

            // Grant permission includes this section and all sub-sections
            // For inverted grant permissions is kind of useless as it's the same as ending an inverted permission with a "._" but we check it anyway
            if (grantSection == WildCard.Any) return true;
            // Grant permission includes any value for this section
            if (grantSection == WildCard.Single) continue;

            // Grant permission section is the same as the check permission section
            if (grantSection == checkSection) continue;

            // Grant permission section is different than the check permission section
            return false;
        }

        // Logically unreachable as the exact match is already covered on top but we have to return something
        return false;
    }

    /// <summary>
    /// Checks if the <b><paramref name="permissions"/></b> allow the <b><paramref name="checkPermission"/></b>.<br/>
    /// Inverted permissions act as an exclude and therefore have higher priority.
    /// </summary>
    /// <param name="permissions">Permissions e.g. granted on a client</param>
    /// <param name="checkPermission">Permission to check if it's granted by the <b><paramref name="permissions"/></b></param>
    /// <returns>If the <b><paramref name="permissions"/></b> grant the <b><paramref name="checkPermission"/></b></returns>
    public static bool Grants(IReadOnlyCollection<string> permissions, string checkPermission)
    {
        var excluded = permissions.Where(IsInverted).Any(p => Includes(p, checkPermission));
        return !excluded && permissions.Where(p => !IsInverted(p)).Any(p => Includes(p, checkPermission));
    }

    /// <summary>
    /// Checks if the permission check <b><paramref name="results"/></b> allow the permission or not.
    /// <br/>
    /// </summary>
    /// <param name="results">Permission check results</param>
    /// <returns>
    /// <b>False</b> if there is an explicit <see cref="Result.Deny"/> or no <see cref="Result.Grant"/>.<br/>
    /// <b>True</b> if there is an <see cref="Result.Grant"/> and no explicit <see cref="Result.Deny"/>.
    /// </returns>
    public static bool Allows(IReadOnlyCollection<Result> results)
    {
        return results.All(result => result != Result.Deny) && results.Any(result => result == Result.Grant);
    }

    /// <summary>
    /// Permission check result.
    /// </summary>
    public enum Result
    {
        /// <summary>
        /// Permission is not granted.
        /// </summary>
        NoGrant,

        /// <summary>
        /// Permission is granted.
        /// </summary>
        Grant,

        /// <summary>
        /// Permission is explicitly denied.
        /// </summary>
        Deny
    }
}