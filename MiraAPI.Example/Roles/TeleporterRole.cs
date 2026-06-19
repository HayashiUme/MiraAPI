using MiraAPI.Roles;
using MiraAPI.Translation;
using UnityEngine;

namespace MiraAPI.Example.Roles;

public class TeleporterRole : CrewmateRole, ICustomRole
{
    public string RoleName => "teleporter.name".Translate();
    public string RoleLongDescription => "teleporter.description".Translate();
    public string RoleDescription => RoleLongDescription;
    public Color RoleColor => new Color32(221, 176, 152, 255);
    public ModdedRoleTeams Team => ModdedRoleTeams.Crewmate;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        OptionsScreenshot = ExampleAssets.Banner,
        CanModifyChance = false,
        DefaultChance = 73,
        DefaultRoleCount = 4,
    };

    public bool CanLocalPlayerSeeRole(PlayerControl player)
    {
        return true;
    }
}
