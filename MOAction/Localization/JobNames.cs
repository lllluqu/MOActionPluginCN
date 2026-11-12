using System;
using System.Collections.Generic;

namespace MOAction.Localization;

/// <summary>FF14 职业缩写 → 国服中文职业名（仅用于显示，配置里仍保存缩写）。</summary>
public static class JobNames
{
    private static readonly Dictionary<string, string> Names = new(StringComparer.OrdinalIgnoreCase)
    {
        // 战斗精英
        ["GLA"] = "剑术师",
        ["PGL"] = "格斗家",
        ["MRD"] = "斧术师",
        ["LNC"] = "枪术师",
        ["ROG"] = "双剑师",
        ["PLD"] = "骑士",
        ["WAR"] = "战士",
        ["DRK"] = "暗黑骑士",
        ["GNB"] = "绝枪战士",
        ["MNK"] = "武僧",
        ["DRG"] = "龙骑士",
        ["NIN"] = "忍者",
        ["SAM"] = "武士",
        ["RPR"] = "钐镰客",
        ["VPR"] = "蝰蛇剑士",
        // 魔法导师
        ["CNJ"] = "幻术师",
        ["THM"] = "咒术师",
        ["ACN"] = "秘术师",
        ["WHM"] = "白魔法师",
        ["BLM"] = "黑魔法师",
        ["SMN"] = "召唤师",
        ["SCH"] = "学者",
        ["AST"] = "占星术士",
        ["SGE"] = "贤者",
        ["RDM"] = "赤魔法师",
        ["BLU"] = "青魔法师",
        ["PCT"] = "绘灵法师",
        // 大地/巧手
        ["CRP"] = "木工",
        ["BSM"] = "锻铁",
        ["ARM"] = "铸甲",
        ["GSM"] = "雕金",
        ["LTW"] = "制革",
        ["WVR"] = "裁衣",
        ["ALC"] = "炼金",
        ["CUL"] = "烹调",
        ["MIN"] = "采矿",
        ["BTN"] = "园艺",
        ["FSH"] = "钓鱼",
    };

    /// <summary>返回显示用职业名；英文模式下仍返回缩写。</summary>
    public static string Get(string? abbreviation)
    {
        if (string.IsNullOrWhiteSpace(abbreviation))
            return abbreviation ?? string.Empty;

        if (abbreviation == "Unset Job")
            return Loc.T(abbreviation);

        if (!Loc.IsChinese)
            return abbreviation;

        return Names.TryGetValue(abbreviation, out var chinese) ? chinese : abbreviation;
    }
}
