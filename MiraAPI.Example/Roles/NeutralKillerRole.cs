using Il2CppInterop.Runtime.Attributes;
using MiraAPI.Example.GameOver;
using MiraAPI.GameEnd;
using MiraAPI.Roles;
using MiraAPI.Translation;
using UnityEngine;

namespace MiraAPI.Example.Roles;

public class NeutralKillerRole : ImpostorRole, ICustomRole
{
    public string RoleName => "outcastKiller.name".Translate();
    public string RoleDescription => "outcastKiller.description".Translate();
    public string RoleLongDescription => RoleDescription;
    public Color RoleColor => Color.magenta;
    public ModdedRoleTeams Team => ModdedRoleTeams.Custom;

    public CustomRoleConfiguration Configuration => new(this)
    {
        UseVanillaKillButton = true,
        CanGetKilled = true,
        CanUseVent = true,
    };

    public RoleOptionsGroup RoleOptionsGroup { get; } = new("outcast.name".Translate(), Color.gray);

    [HideFromIl2Cpp]
    public TeamIntroConfiguration? IntroConfiguration { get; } = new(
        Color.gray,
        "outcast.roleTitle".Translate(),
        "outcast.teamIntroDescription".Translate());

    public override void SpawnTaskHeader(PlayerControl playerControl)
    {
        // remove existing task header.
    }

    public override bool DidWin(GameOverReason gameOverReason)
    {
        return gameOverReason == CustomGameOver.GameOverReason<NeutralKillerGameOver>();
    }
}
