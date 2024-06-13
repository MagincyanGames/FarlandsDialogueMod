using BepInEx;
using BepInEx.Configuration;
using FarlandsCoreMod;
using FarlandsCoreMod.Attributes;
using FarlandsDialogueMod.Patchers;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FarlandsDialogueMod
{
    [BepInPlugin("top.magincian.farlands_dialogue_mod", PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency("top.magincian.fcm","~0.0.2")]
    public class DialogueModPlugin : FarlandsMod
    {
        [Configuration.Bool("Config", "DialogEnable", "If true, the custom traduction is the used", false)]
        public static ConfigEntry<bool> Config_dialogEnable;

        [Configuration.Int("Config", "DialogIndex", "Index of the current custom language", 0)]
        public static ConfigEntry<int> Config_dialogIndex;

        [Configuration.Bool("Debug","TermDialog", "If true, any dialog will be replaced by its term", false)]
        public static ConfigEntry<bool> Config_termDialog;

        [Configuration.Bool("Debug", "ExportDialogues", "If true, a export file will be created and will save all the dialogs you will read", false)]
        public static ConfigEntry<bool> Config_exportDialogues;


        public override void OnStart()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}
