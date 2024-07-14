using FarlandsCoreMod.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using I2.Loc;
using JetBrains.Annotations;
using System.Linq;
using System.IO;
using BepInEx;
using System.Diagnostics;
using PixelCrushers.DialogueSystem;

namespace FarlandsDialogueMod.Patchers
{
    [Patcher]
    public static class UpdateSourcesPatcher
    {
        public static bool isLoaded = false;
        [HarmonyPatch(typeof(LocalizationManager), "RegisterSourceInResources")]
        [HarmonyPostfix]
        public static void PrefixPatch()
        {
            if (isLoaded) return;
            isLoaded = true;

            if (DialogueModPlugin.Config_exportDialogues.Value)
                File.WriteAllText(
                    Path.Combine(Paths.PluginPath, "FarlandsDialogueMod/export.json"),
                    Newtonsoft.Json.JsonConvert.SerializeObject(
                        SourceJSON.FromFull(LocalizationManager.Sources.First(),
                            DialogueManager.instance.masterDatabase)
                    )
                );

            var sources = DialogueModPlugin.Instance.GetFiles("", "*.source.json", SearchOption.TopDirectoryOnly);
            if (sources.Count() < 1) return;

            var allSources = sources.Select(SourceJSON.FromFile).ToList();

            var mainSource = allSources.Select(LoadOneSource).ToList().First();
            var data = allSources.Select(LoadOneData).ToList();

            mainSource.UpdateDictionary(true);

        }

        private static LanguageSourceData LoadOneSource(SourceJSON source) => source.LoadInMain();
        private static DialogueDatabase LoadOneData(SourceJSON source) => source.LoadInData();
    }
}
