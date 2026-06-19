using MiraAPI.Roles;
using MiraAPI.Translation;
using UnityEngine;

namespace MiraAPI.Example.Roles;

public class FreezerRole : ImpostorRole, ICustomRole
{
    public string RoleName => "freezer.name".Translate();
    public string RoleLongDescription => "freezer.description".Translate();
    public string RoleDescription => RoleLongDescription;
    public Color RoleColor => Palette.Blue;
    public ModdedRoleTeams Team => ModdedRoleTeams.Impostor;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        OptionsScreenshot = ExampleAssets.Banner,
        MaxRoleCount = 2,
    };
}
