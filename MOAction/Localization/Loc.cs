using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace MOAction.Localization;

/// <summary>Which language the plugin UI should be rendered in.</summary>
public enum PluginLanguage
{
    Auto = 0,
    Chinese = 1,
    English = 2,
}

/// <summary>
/// Runtime localization layer. English source text is the lookup key so call sites stay
/// readable and untranslated strings fall back to English.
/// </summary>
public static class Loc
{
    private const string ResourceName = "MOAction.Localization.zh-CN.json";

    private static Dictionary<string, string> _zh = new(StringComparer.Ordinal);
    private static bool _loaded;
    private static readonly HashSet<string> MissingKeys = new(StringComparer.Ordinal);

    public static PluginLanguage Setting { get; private set; } = PluginLanguage.Auto;

    public static bool IsChinese { get; private set; }

    public static void Initialize(PluginLanguage setting)
    {
        EnsureInitialized();
        Setting = setting;
        IsChinese = Resolve(setting);
        Plugin.PluginLog?.Debug($"[MOAction] UI language: {(IsChinese ? "Chinese (zh-CN)" : "English")}");
    }

    public static void SetSetting(PluginLanguage setting)
    {
        EnsureInitialized();
        Setting = setting;
        IsChinese = Resolve(setting);
    }

    private static bool Resolve(PluginLanguage setting) => setting switch
    {
        PluginLanguage.Chinese => true,
        PluginLanguage.English => false,
        _ => DetectChineseClient(),
    };

    public static string T(string english)
    {
        EnsureInitialized();

        if (!IsChinese || string.IsNullOrEmpty(english))
            return english;

        if (_zh.TryGetValue(english, out var translated))
            return translated;

        if (MissingKeys.Add(english))
            Plugin.PluginLog?.Debug($"[MOAction] Missing zh-CN translation: {english}");

        return english;
    }

    public static string T(string english, params object?[] args)
    {
        var format = T(english);
        if (args.Length == 0)
            return format;

        try
        {
            return string.Format(CultureInfo.InvariantCulture, format, args);
        }
        catch (FormatException)
        {
            return format;
        }
    }

    public static bool TryGet(string english, out string translated)
    {
        EnsureInitialized();

        if (IsChinese && _zh.TryGetValue(english, out var value))
        {
            translated = value;
            return true;
        }

        translated = english;
        return false;
    }

    private static void EnsureInitialized()
    {
        if (_loaded)
            return;

        LoadDictionary();
        IsChinese = Resolve(Setting);
    }

    private static void LoadDictionary()
    {
        if (_loaded)
            return;

        _loaded = true;

        try
        {
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(ResourceName);
            if (stream is null)
            {
                Plugin.PluginLog?.Warning($"[MOAction] Localization resource '{ResourceName}' not found.");
                return;
            }

            var parsed = JsonSerializer.Deserialize<Dictionary<string, string>>(stream);
            if (parsed is not null)
                _zh = new Dictionary<string, string>(parsed, StringComparer.Ordinal);
        }
        catch (Exception ex)
        {
            Plugin.PluginLog?.Error(ex, "[MOAction] Failed to load zh-CN localization.");
        }
    }

    private static bool DetectChineseClient()
    {
        try
        {
            var uiLanguage = Plugin.PluginInterface?.UiLanguage;
            if (!string.IsNullOrEmpty(uiLanguage) && uiLanguage.StartsWith("zh", StringComparison.OrdinalIgnoreCase))
                return true;
        }
        catch
        {
            // ignored
        }

        try
        {
            if (Plugin.ClientState!.ClientLanguage.ToString().Contains("Chinese", StringComparison.OrdinalIgnoreCase))
                return true;
        }
        catch
        {
            // ignored
        }

        try
        {
            var sample = Plugin.DataManager!.GetExcelSheet<Lumina.Excel.Sheets.Item>().GetRow(1).Name.ToString();
            foreach (var c in sample)
            {
                if (c >= '\u4E00' && c <= '\u9FFF')
                    return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }
}
