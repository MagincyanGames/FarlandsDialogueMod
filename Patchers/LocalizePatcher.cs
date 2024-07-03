using BepInEx;
using FarlandsCoreMod.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using HarmonyLib;
using I2.Loc;
using System;
using UnityEngine.UI;
using System.Linq;
using System.Text.RegularExpressions;

namespace FarlandsDialogueMod.Patchers
{
    [Patcher]
    public class LocalizePatcher
    {

        //public static Dictionary<string, string> ReadedDialogues = new();
        //public static Dictionary<string, string> Dialogues = new();
        //public static Dictionary<string, string> Tags = new();
        //public static Dictionary<string, string> Translations = new();
        //public static List<string> TranslationsList = new();

        //public static string ApplyTags(string input)
        //{
        //    foreach (var tag in Tags)
        //    {
        //        input = Regex.Replace(input, tag.Key, tag.Value);
        //    }

        //    return input;
        //}

        //[PatcherPreload]
        //public static void Preload()
        //{
        //    Directory.GetFiles(Path.Combine(Paths.PluginPath, "FarlandsDialogueMod/"),
        //        "*.json",
        //        SearchOption.TopDirectoryOnly).ToList().ForEach(LoadDialoguesFrom);


        //    DialogueModPlugin.Config_dialogIndex.Value = Math.Min(DialogueModPlugin.Config_dialogIndex.Value, Translations.Count - 1);
        //    LoadTranslation();
        //}

        //public static void LoadDialoguesFrom(string path)
        //{
        //    Debug.Log("Preloading");

        //    if (File.Exists(path))
        //    {
        //        try
        //        {
        //            string jsonString = File.ReadAllText(path);

        //            Dialogues = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);

        //            if (!Dialogues.ContainsKey("Mod/Translation")) return;

        //            Translations.Add(Dialogues["Mod/Translation"], path);
        //            TranslationsList.Add(Dialogues["Mod/Translation"]);
        //        }
        //        catch (JsonReaderException err)
        //        {
        //            Debug.Log(err.Message);
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        Debug.LogError($"No exist \"{path}\"");
        //    }
        //}
        //public static void LoadTranslation() => LoadTranslation(TranslationsList[DialogueModPlugin.Config_dialogIndex.Value]);
        //public static void LoadTranslation(string translation)
        //{
        //    Debug.Log("Loadding " + translation);
        //    string path = Translations[translation];

        //    if (File.Exists(path))
        //    {
        //        string jsonString = File.ReadAllText(path);

        //        Tags.Clear();

        //        Dialogues = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
        //        Dialogues.Where(d => d.Key.StartsWith("Mod/Tag/"))
        //            .ToList()
        //            .ForEach(x =>
        //            {
        //                Debug.Log(x.Key);
        //                if (!Tags.ContainsKey(x.Key)) Tags.Add(x.Key.Replace("Mod/Tag/", ""), x.Value);
        //            });
        //        if (Dialogues == null) Debug.LogError("EFE");
        //    }
        //    else
        //    {
        //        Debug.LogError($"No exist \"{path}\"");
        //    }
        //}

        //[HarmonyPatch(typeof(LocalizationManager), "GetTranslation")]
        //[HarmonyPrefix]
        //public static bool Localize(
        //    string Term, bool FixForRTL,
        //    int maxLineLengthForRTL, bool ignoreRTLnumbers,
        //    bool applyParameters, GameObject localParametersRoot,
        //    string overrideLanguage, bool allowLocalizedParameters, ref string __result)
        //{
        //    if (DialogueModPlugin.Config_termDialog.Value)
        //    {
        //        __result = Term;
        //        return false;
        //    }
        //    else
        //    {
        //        if (!DialogueModPlugin.Config_dialogEnable.Value || !Dialogues.ContainsKey(Term))
        //            return true;

        //        __result = ApplyTags(Dialogues[Term]);
        //        return false;
        //    }
        //}

        //[HarmonyPatch(typeof(LocalizationManager), "GetTranslation")]
        //[HarmonyPostfix]
        //public static void OnFinishTranslation(
        //    string Term, bool FixForRTL,
        //    int maxLineLengthForRTL, bool ignoreRTLnumbers,
        //    bool applyParameters, GameObject localParametersRoot,
        //    string overrideLanguage, bool allowLocalizedParameters, ref string __result)
        //{
        //    if (!DialogueModPlugin.Config_exportDialogues.Value || Dialogues.ContainsKey(Term)) return;

        //    Debug.Log($"Result: {__result}");
        //    if (!ReadedDialogues.ContainsKey(Term)) ReadedDialogues.Add(Term, __result);
        //    string path = Path.Combine(Paths.PluginPath, "FarlandsDialogueMod/export.json");

        //    File.WriteAllText(path, JsonConvert.SerializeObject(ReadedDialogues, Formatting.Indented));

        //}

        //static void AdjustFontSize(Text textComponent, int maxWidth)
        //{
        //    RectTransform rectTransform = textComponent.GetComponent<RectTransform>();

        //    // Set the width and height constraints
        //    rectTransform.sizeDelta = new Vector2(maxWidth, rectTransform.sizeDelta.y);

        //    // Adjust the font size to fit within the specified width and height
        //    int fontSize = textComponent.fontSize;
        //    float textWidth = textComponent.preferredWidth;
        //    float textHeight = textComponent.preferredHeight;


        //    while ((textWidth > maxWidth || textHeight > rectTransform.sizeDelta.y) && fontSize > 1)
        //    {
        //        fontSize--;
        //        Debug.Log(fontSize);
        //        textComponent.fontSize = fontSize;
        //        textWidth = textComponent.preferredWidth;
        //        textHeight = textComponent.preferredHeight;
        //    }

        //    fontSize -= 1;
        //    textComponent.fontSize = fontSize;
        //}
    }
}