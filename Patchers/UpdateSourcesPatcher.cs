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

namespace FarlandsDialogueMod.Patchers
{
    [Patcher]
    public class UpdateSourcesPatcher
    {
        [HarmonyPatch(typeof(LocalizationManager), "RegisterSourceInResources")]
        [HarmonyPostfix]
        public static void PrefixPatch()
        {
            var all = LocalizationManager.Sources;
            string path = Path.Combine(Paths.PluginPath, "FarlandsDialogueMod/source.json.source");
            var json = File.ReadAllText(path);

            var source = Newtonsoft.Json.JsonConvert.DeserializeObject<SourceJSON>(json);

            if (DialogueModPlugin.Config_exportDialogues.Value)
                File.WriteAllText(
                    Path.Combine(Paths.PluginPath, "FarlandsDialogueMod/export.json"),
                    Newtonsoft.Json.JsonConvert.SerializeObject(
                        SourceJSON.FromLSD(LocalizationManager.Sources.First())
                    )
                );

            source.LoadInMain();
        }
    }
}
