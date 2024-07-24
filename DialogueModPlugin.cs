using BepInEx;
using BepInEx.Configuration;
using FarlandsCoreMod;
using FarlandsCoreMod.Attributes;
using FarlandsDialogueMod.Patchers;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using PixelCrushers.DialogueSystem;
using Newtonsoft.Json;
using System.IO;

namespace FarlandsDialogueMod
{
    [BepInPlugin("top.magincian.farlands_dialogue_mod", PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency("top.magincian.fcm","~0.0.4")]
    public class DialogueModPlugin : FarlandsMod
    {
        [Configuration.Bool("Debug", "ExportDialogues", "If true, a export file will be created and will save all the dialogues", false)]
        public static ConfigEntry<bool> Config_exportDialogues;

        public static DialogueModPlugin Instance;

        public override string SHORT_NAME => "FDM";

        public override void OnStart()
        {
            Instance = this;
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }

        [OnLoadScene("MainMenu")]
        public static void OnGameLoaded(Scene scene)
        {
            Debug.Log("MM Loaded");
            var dsc = GameObject.FindObjectOfType<DialogueSystemController>();

            Conversation eddy = dsc.masterDatabase.GetConversation("Eddy/1");
            
            foreach (var dialog in eddy.dialogueEntries)
            {
                Debug.Log($"{dialog.Title}:");
                Field.SetValue(dialog.fields, "es", "aaaaaa");
                Field.SetValue(dialog.fields, "spanish", "aaaaaa");
                foreach (var field in dialog.fields)
                {
                    Debug.Log($"{field.title}: {field.value}");
                    
                }
                Debug.Log($">>>>>>>>>");
            }
        }
    }
}
