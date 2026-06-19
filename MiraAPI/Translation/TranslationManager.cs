using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Reactor.Utilities;
using UnityEngine;

namespace MiraAPI.Translation;

public static class TranslationManager
{
    private const string CacheDirectory = "mira_languages";

    private static readonly Dictionary<string, Dictionary<MiraLanguage, Dictionary<string, string>>> Translations = [];

    private static MiraLanguage _currentLanguage = MiraLanguage.English;

    /// <summary>
    /// Gets or sets the current language.
    /// </summary>
    public static MiraLanguage CurrentLanguage
    {
        get => _currentLanguage;
        set => _currentLanguage = value;
    }

    /// <summary>
    /// Registers a translation file from embedded resources.
    /// </summary>
    /// <param name="modGuid">The mod guid.</param>
    /// <param name="resourcePath">The embedded resource path.</param>
    /// <param name="lang">The language this file provides translations for.</param>
    public static void Register(string modGuid, string resourcePath, MiraLanguage lang)
    {
        var assembly = System.Reflection.Assembly.GetCallingAssembly();

        using var stream = assembly.GetManifestResourceStream(resourcePath);
        if (stream == null)
        {
            Error($"Translation resource not found: {resourcePath} in {assembly.GetName().Name}");
            return;
        }

        using var reader = new StreamReader(stream);
        var json = reader.ReadToEnd();

        Dictionary<string, string>? translationDict;
        try
        {
            translationDict = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        }
        catch (Exception e)
        {
            Error($"Failed to parse translation file {resourcePath}: {e.Message}");
            return;
        }

        if (translationDict == null)
        {
            Error($"Translation file {resourcePath} is empty or invalid.");
            return;
        }

        if (!Translations.ContainsKey(modGuid))
        {
            Translations[modGuid] = [];
        }

        Translations[modGuid][lang] = translationDict;

        CacheToDisk(modGuid, lang, json);

        Info($"Registered {lang} translation for mod {modGuid} ({translationDict.Count} keys)");
    }

    /// <summary>
    /// Translates a key into the current language.
    /// Searches all mods, with reverse lookup fallback.
    /// </summary>
    /// <param name="key">The translation key (structured or display text).</param>
    /// <returns>The translated string.</returns>
    public static string Translate(string key)
    {
        var currentLang = CurrentLanguage;

        foreach (var (_, modTranslations) in Translations)
        {
            if (modTranslations.TryGetValue(currentLang, out var langDict) &&
                langDict.TryGetValue(key, out var translated))
            {
                return translated;
            }
        }

        foreach (var (_, modTranslations) in Translations)
        {
            if (modTranslations.TryGetValue(MiraLanguage.English, out var engDict) &&
                engDict.TryGetValue(key, out var english))
            {
                return english;
            }
        }

        string? bestStructuredKey = null;
        foreach (var (_, modTranslations) in Translations)
        {
            if (modTranslations.TryGetValue(MiraLanguage.English, out var engDict))
            {
                foreach (var (structuredKey, englishValue) in engDict)
                {
                    if (englishValue == key)
                    {
                        bestStructuredKey = structuredKey;
                        break;
                    }
                }
            }

            if (bestStructuredKey != null) break;
        }

        if (bestStructuredKey != null)
        {
            // Try current language first
            foreach (var (_, modTranslations) in Translations)
            {
                if (modTranslations.TryGetValue(currentLang, out var langDict) &&
                    langDict.TryGetValue(bestStructuredKey, out var revTranslated))
                {
                    return revTranslated;
                }
            }

            foreach (var (_, modTranslations) in Translations)
            {
                if (modTranslations.TryGetValue(MiraLanguage.English, out var engDict) &&
                    engDict.TryGetValue(bestStructuredKey, out var revEnglish))
                {
                    return revEnglish;
                }
            }
        }

        return key;
    }

    private static void CacheToDisk(string modGuid, MiraLanguage lang, string json)
    {
        try
        {
            var dir = Path.Combine(Application.persistentDataPath, CacheDirectory, modGuid);
            Directory.CreateDirectory(dir);
            File.WriteAllText(Path.Combine(dir, $"{lang}.json"), json);
        }
        catch (Exception e)
        {
            Warning($"Failed to cache translation to disk for {modGuid}/{lang}: {e.Message}");
        }
    }
}
