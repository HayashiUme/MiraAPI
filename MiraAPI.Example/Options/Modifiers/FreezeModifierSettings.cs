using MiraAPI.Example.Modifiers.Freezer;
using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.Translation;

namespace MiraAPI.Example.Options.Modifiers;

public class FreezeModifierSettings : AbstractOptionGroup<FreezeModifier>
{
    public override string GroupName => "options.freezeModifierSettings.name".Translate();

    [ModdedToggleOption("Use Color")]
    public bool UseColor { get; set; } = true;
}
