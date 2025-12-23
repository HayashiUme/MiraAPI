using BepInEx.Configuration;
using MiraAPI.LocalSettings;
using MiraAPI.LocalSettings.Attributes;
using MiraAPI.Utilities.Assets;

namespace MiraAPI;

/// <summary>
/// Mira API Config File Handler.
/// </summary>
public class MiraApiSettings(ConfigFile config) : LocalSettingsTab(config)
{
    /// <inheritdoc />
    public override string TabName => "Mira API";

    /// <inheritdoc />
    public override LocalSettingTabAppearance TabAppearance => new()
    {
        TabButtonHoverColor = MiraApiPlugin.MiraColor,
        TabIcon = MiraAssets.SettingsIcon,
        HideIconOnHover = false,
    };

    /// <summary>
    /// Gets whether the modifiers hud should be on the left side of the screen (under roles/task tab). Recommended for streamers.
    /// </summary>
    [LocalToggleSetting]
    public ConfigEntry<bool> ModifiersHudLeftSide { get; private set; } = config.Bind("Displays", "Show Modifiers HUD on Left Side", false);

    /// <summary>
    /// Gets whether to show keybinds on buttons.
    /// </summary>
    [LocalToggleSetting]
    public ConfigEntry<bool> ShowKeybinds { get; private set; } = config.Bind("Keybinds", "Show Keybinds on Buttons", true);
    // This would be placed in the keybinds menu, but it crashes for Epic Games users. - Atony

    /// <summary>
    /// Gets whether to apply cosmetic changes to the TaskAdder.
    /// </summary>
    [LocalToggleSetting]
    public ConfigEntry<bool> PrettyTaskAdder { get; private set; } = config.Bind("Freeplay", "Pretty Task Laptop", true);

    /// <summary>
    /// Gets whether to show the red flash from sabotages.
    /// </summary>
    [LocalToggleSetting]
    public ConfigEntry<bool> EnableSabotageFlashes { get; private set; } = config.Bind("Accessibility", "Enable Sabotage Flashes", true);

    /// <summary>
    /// Gets whether to enable the sabotage sound effects or not.
    /// </summary>
    [LocalToggleSetting]
    public ConfigEntry<bool> EnableSabotageBlares { get; private set; } = config.Bind("Accessibility", "Enable Sabotage Blare", true);
}
