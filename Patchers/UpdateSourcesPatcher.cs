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

namespace FarlandsDialogueMod.Patchers
{
    [Patcher]
    public class UpdateSourcesPatcher
    {
        [HarmonyPatch(typeof(LocalizationManager), "RegisterSourceInResources")]
        [HarmonyPostfix]
        public static void PrefixPatch()
        {
            if (DialogueModPlugin.Config_exportDialogues.Value)
                File.WriteAllText(
                    Path.Combine(Paths.PluginPath, "FarlandsDialogueMod/export.json"),
                    Newtonsoft.Json.JsonConvert.SerializeObject(
                        SourceJSON.FromLSD(LocalizationManager.Sources.First())
                    )
                );
            var sources = DialogueModPlugin.Instance.GetFiles("", "*.source.json", SearchOption.TopDirectoryOnly);
            if (sources.Count() < 1) return;
            
            var main =  sources.Select(LoadOneFromPath).ToList().First();

            main.UpdateDictionary(true);
        }

        private static LanguageSourceData LoadOneFromPath(string path)
        {
            var source = SourceJSON.FromFile(path);
            var main = source.LoadInMain();

            return main;
        }
    }
}
