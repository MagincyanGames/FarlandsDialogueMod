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
        public static bool isSourcesLoaded = false;
        public static bool isDialoguesLoaded = false;

        public static List<SourceJSON> sources = null;

        private static void InitSources()
        {
            var src = DialogueModPlugin.Instance.GetFiles("", "*.source.json", SearchOption.TopDirectoryOnly);
            if (src.Count() < 1) return;

            sources = src.Select(SourceJSON.FromFile).ToList();
        }

        public static void export()
        {
            if (DialogueModPlugin.Config_exportDialogues.Value)
            {
                var source = SourceJSON.FromFull(LocalizationManager.Sources.First(), DialogueManager.instance.masterDatabase);

                File.WriteAllText(
                    Path.Combine(Paths.PluginPath, "FarlandsDialogueMod/export.json"),
                    Newtonsoft.Json.JsonConvert.SerializeObject(source)
                );
            }
        }

        [HarmonyPatch(typeof(LocalizationManager), "RegisterSceneSources")]
        [HarmonyPostfix]
        public static void SourcePatch()
        {
            if (isSourcesLoaded) return;
            if (isDialoguesLoaded) export();

            isSourcesLoaded = true;

            

            if(sources == null)
                InitSources();

            if (sources.Count() < 1) return;

            var mainSource = sources.Select(LoadOneSource).ToList().First();
           

            mainSource.UpdateDictionary(true);

        }


        // TODO: posible mejora en el siguiente código
        [HarmonyPatch(typeof(DialogueSystemController), "Awake")]
        [HarmonyPostfix]
        public static void DialoguePatch()
        {
            if (isDialoguesLoaded) return;
            if (isSourcesLoaded) export();

            isDialoguesLoaded = true;

            if (sources == null)
                InitSources();

            if (sources.Count() < 1) return;

            var data = sources.Select(LoadOneData).ToList();
        }

        private static LanguageSourceData LoadOneSource(SourceJSON source) => source.LoadInMain();
        private static DialogueDatabase LoadOneData(SourceJSON source) => source.LoadInData();
    }
}
