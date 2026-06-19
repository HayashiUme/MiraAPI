using System.Linq;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;

using MiraAPI.PluginLoading;
using MiraAPI.Translation;
using Reactor;
using Reactor.Networking;
using Reactor.Networking.Attributes;

namespace MiraAPI.Example;

[BepInAutoPlugin("mira.example", "MiraExampleMod")]
[BepInProcess("Among Us.exe")]
[BepInDependency(ReactorPlugin.Id)]
[BepInDependency(MiraApiPlugin.Id)]
[ReactorModFlags(ModFlags.RequireOnAllClients)]
public partial class ExamplePlugin : BasePlugin, IMiraPlugin
{
    public Harmony Harmony { get; } = new(Id);
    public string OptionsTitleText => "Mira API\nExample Mod";
    public ConfigFile GetConfigFile() => Config;
    public override void Load()
    {
        TranslationManager.Register("mira.example", "MiraAPI.Example.Resources.Translations.English.json", MiraLanguage.English);
        TranslationManager.Register("mira.example", "MiraAPI.Example.Resources.Translations.SChinese.json", MiraLanguage.SChinese);

        ExampleEventHandlers.Initialize();
        Harmony.PatchAll();
    }
}
