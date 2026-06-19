using MiraAPI.Colors;
using MiraAPI.Translation;
using UnityEngine;

namespace MiraAPI.Example;

[RegisterCustomColors]
public static class ExampleColors
{
    public static CustomColor Cerulean { get; } = new("color.cerulean".Translate(), new Color(0.0f, 0.48f, 0.65f))
    {
        ColorBrightness = CustomColorBrightness.Lighter,
    };

    public static CustomColor Rose { get; } = new("color.rose".Translate(), new Color(0.98f, 0.26f, 0.62f));

    public static CustomColor Gold { get; } = new("color.gold".Translate(), new Color(1.0f, 0.84f, 0.0f));
}
