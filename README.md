# MOAction (mo' comfy)
This plugin removes the need to use macros for mouseover.   
Thanks to attick, Alluneve, Meli and goat as always.

## About
MOAction is a plugin to allow for abilities to be used while mousing over entities. If you've ever played WoW, this is effectively one of their mouseover macros. If you've ever tried to use a mouseover macro in XIV, you know how painful and clunky it is. This plugin removes the clunk associated with macros and creates seamless ability queuing for anything mouseover. No more awkward swapping to party list to heal someone or buff them, no more macros that don't go off half the time.
## Installation
* Type `/xlplugins` in-game to access the plugin installer and updater. Any releases on this github page have been removed to facilitate proper installation going forward.
## In-game usage
* Type `/pmoaction` to pull up a GUI for editing allowed mouseover actions.
  * "Field mouseover" refers to placing your mouse over something's character model on your screen.
  * "GUI mouseover" refers to placing your mouse over something's name on your UI (party list, enmity list).
* Use the abilities!

## 简体中文汉化版 / Chinese localization

This fork (`lllluqu/MOActionPluginCN`) is a full Simplified Chinese localization of MOAction.
It ships as **MOAction 汉化版** (`InternalName: MOActionCN`) and can be installed alongside the English original.

- 安装源（聚合仓库，收录多个汉化插件）：`https://raw.githubusercontent.com/lllluqu/MyDalamudRepo/main/pluginmaster.json`
- 语言：默认跟随客户端，可在 **`/mo` → 设置 → 语言** 切换「自动 / 简体中文 / English」。
- 汉化范围：设置 / 向导 / 关于三个页签、按钮与提示、动作与目标下拉框、取色器、搜索框、聊天提示与命令帮助。
- 版本号：原版基线 + 汉化修订（原版 `4.10.1` → 汉化修订 1 为 `4.10.1.1`）。
- 与原版的差异：`InternalName` 改为 `MOActionCN`、IPC 前缀改为 `MOActionCN.`，可与英文原版共存；命令 `/mo`、`/moaction`、`/pmoaction` 不变。
- 本地化代码入口：`MOAction/Localization/Loc.cs`，词典：`MOAction/Localization/zh-CN.json`。

