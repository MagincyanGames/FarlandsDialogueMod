using BepInEx;
using FarlandsCoreMod.Patchers;
using FarlandsCoreMod.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using HarmonyLib;
using I2.Loc;
using System;
using UnityEngine.UI;
using TMPro;
using System.Reflection;
using static UnityEngine.ParticleSystem.PlaybackState;

namespace FarlandsDialogueMod.Patchers
{
    [Patcher]
    public class LanguageSelectorPatcher
    {
        [HarmonyPatch(typeof(LanguageSelectionScript), "Start")]
        [HarmonyPostfix]
        public static void Start(LanguageSelectionScript __instance)
        {
            var dr = __instance.GetComponent<TMP_Dropdown>();

            int value = LocalizationManager.GetAllLanguages().IndexOf(LocalizationManager.CurrentLanguage) + 1;
            if (DialogueModPlugin.Config_dialogEnable.Value) dr.value = 0;
            else dr.value = value;
        }

        [HarmonyPatch(typeof(LanguageSelectionScript), "PopulateLanguages")]
        [HarmonyPrefix]
        public static bool PopulateLanguages(LanguageSelectionScript __instance)
        {

            var dr = __instance.GetComponent<TMP_Dropdown>();
            dr.ClearOptions();
            dr.AddOptions(["Custom"]);

            dr.AddOptions(LocalizationManager.GetAllLanguages(true));

            return false;

        }

        [HarmonyPatch(typeof(LanguageSelectionScript), "ChangeLanguage")]
        [HarmonyPrefix]
        public static bool ChangeLanguage(LanguageSelectionScript __instance, int index)
        {
            index--;
            Debug.Log(index);

            DialogueModPlugin.Config_dialogEnable.Value = index < 0;

            var all = LocalizationManager.GetAllLanguages();
                       

            if (index >= 0) LocalizationManager.CurrentLanguage = all[index];
            else 
            {
                var inherit = "English";
                if (LocalizePatcher.Dialogues.ContainsKey("Mod/Inherit") && all.Contains(LocalizePatcher.Dialogues["Mod/Inherit"]))
                    inherit = LocalizePatcher.Dialogues["Mod/Inherit"];

                
                LocalizationManager.CurrentLanguage = all[all.IndexOf(inherit)];
            }

            string supportedLanguage = LocalizationManager.GetSupportedLanguage(LocalizationManager.CurrentLanguage);
            if (!string.IsNullOrEmpty(supportedLanguage))
            {
                LocalizationManager.SetLanguageAndCode(supportedLanguage, LocalizationManager.GetLanguageCode(supportedLanguage), Force: true);
            }

            return false;

        }
    }
}