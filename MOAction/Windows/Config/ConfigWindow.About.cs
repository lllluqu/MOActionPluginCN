using System.Numerics;
using Dalamud.Interface.Colors;
using Dalamud.Interface.Utility;
using Dalamud.Interface.Utility.Raii;
using Dalamud.Bindings.ImGui;
using MOAction.Localization;

namespace MOAction.Windows.Config;

public partial class ConfigWindow
{
    private static Tabs About()
    {
        using var tabItem = ImRaii.TabItem(Loc.T("About"));
        if (!tabItem.Success)
            return Tabs.None;

        ImGuiHelpers.ScaledDummy(5.0f);

        ImGui.TextUnformatted(Loc.T("Author:"));
        ImGui.SameLine();
        ImGui.TextColored(ImGuiColors.ParsedGold, Plugin.PluginInterface.Manifest.Author);

        ImGui.TextUnformatted(Loc.T("Discord:"));
        ImGui.SameLine();
        ImGui.TextColored(ImGuiColors.ParsedGold, "@infi");

        ImGui.TextUnformatted(Loc.T("Version:"));
        ImGui.SameLine();
        ImGui.TextColored(ImGuiColors.ParsedOrange, Plugin.PluginInterface.Manifest.AssemblyVersion.ToString());

        return Tabs.About;
    }

    private void DrawAboutButtons()
    {
        using (ImRaii.PushColor(ImGuiCol.Button, ImGuiColors.ParsedBlue))
        {
            if (ImGui.Button(Loc.T("Discord Thread")))
                Dalamud.Utility.Util.OpenLink("https://discord.com/channels/581875019861328007/1317992760208265308");
        }

        ImGui.SameLine();

        using (ImRaii.PushColor(ImGuiCol.Button, ImGuiColors.DPSRed))
        {
            if (ImGui.Button(Loc.T("Issues")))
                Dalamud.Utility.Util.OpenLink("https://github.com/Infiziert90/MOActionPlugin/issues");
        }

        ImGui.SameLine();

        using (ImRaii.PushColor(ImGuiCol.Button, new Vector4(0.12549f, 0.74902f, 0.33333f, 0.6f)))
        {
            if (ImGui.Button(Loc.T("Ko-Fi Tip")))
                Dalamud.Utility.Util.OpenLink("https://ko-fi.com/infiii");
        }
    }
}
