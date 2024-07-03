using I2.Loc;
using PixelCrushers.DialogueSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering;
using static FarlandsDialogueMod.Patchers.SourceJSON.TranslationJSON;

namespace FarlandsDialogueMod.Patchers
{
    public class SourceJSON
    {
        public string name;
        public string code;
        public TranslationJSON translations;
        public TermJSON terms;

        public class TranslationJSON
        { 
            public Dictionary<string, string> General;
            public Dictionary<string, InventoryJSON> Inventory;
            public DialoguesJSON Dialogues;

            public class InventoryJSON
            {
                public string name;
                public string description;
            }

            public class DialoguesJSON
            { 
                public Dictionary<string, string> Actors;
                public Dictionary<string, string> Variables;
                public Dictionary<string, Dictionary<string, string>> Conversations;
            }
        }
        public class TermJSON
        {
            public Dictionary<string, TermData> General;
            public Dictionary<string, InventoryJSON> Inventory;
            public DialoguesJSON Dialogues;
            public Dictionary<string, TermData> Export;
            public class InventoryJSON
            {
                public TermData name;
                public TermData description;
            }

            public class DialoguesJSON
            {
                public Dictionary<string, TermData> Actors;
                public Dictionary<string, TermData> Variables;
                public Dictionary<string, Dictionary<string, TermData>> Conversations;
            }
        }
        public static SourceJSON FromLSD(LanguageSourceData data) => new()
        {
            terms = new() { Export = data.mDictionary}
        };
        public static SourceJSON FromJson(string json) =>
            Newtonsoft.Json.JsonConvert.DeserializeObject<SourceJSON>(json);
        public static SourceJSON FromFile(string path) =>
            FromJson(DialogueModPlugin.Instance.Read(path));

        private void AddContainingTerm(string key, string value, LanguageSourceData data)
        {
            if (data.ContainsTerm(key))
            {
                var term = data.GetTermData(key);
                term.Languages[data.GetLanguages(false).IndexOf(name)] = value;
            }
        }
        private void AddNewTerm(string key, TermData value, LanguageSourceData data)
        {
            if (!data.ContainsTerm(key))
            {
                var term = data.AddTerm(key, value.TermType);
                term.Languages = value.Languages;
                term.Flags = value.Flags;
            }
        }

        public LanguageSourceData LoadInMain() => LoadIn(LocalizationManager.Sources.First());
        public LanguageSourceData LoadIn(LanguageSourceData data) 
        {
            Debug.Log("RECARGANDO DATA");
            data.AddLanguage(name, code);

            if (translations != null)
            {
                if (translations.General != null)
                    foreach (var pair in translations.General)
                        AddContainingTerm($"General/{pair.Key}", pair.Value, data);

                if(translations.Inventory != null)
                    foreach (var pair in translations.Inventory)
                    {
                        var translation = pair.Value;
                        AddContainingTerm($"Inventory/item_name_{pair.Key}", 
                            translation.name, data);
                        AddContainingTerm($"Inventory/item_description_{pair.Key}", 
                            translation.description, data);
                    }

                if (translations.Dialogues != null)
                {
                    if (translations.Dialogues.Actors != null)
                        foreach (var pair in translations.Dialogues.Actors)
                        {
                            AddContainingTerm($"Dialogue System/Actor/{pair.Key}/Name", pair.Value,data);
                        }

                    if (translations.Dialogues.Variables != null)
                        foreach (var pair in translations.Dialogues.Variables)
                        {
                            AddContainingTerm($"Dialogue System/Variable/{pair.Key}/Initial Value", pair.Value, data);
                        }

                    if (translations.Dialogues.Conversations != null)
                        foreach (var pair in translations.Dialogues.Conversations)
                        {
                            if (pair.Value != null)
                                foreach (var c in pair.Value)
                                    AddContainingTerm($"Dialogue System/Conversation/{pair.Key}/Entry/{c.Key}/Dialogue Text",
                                        c.Value, data);
                        }
                }
            }

            if (terms != null)
            {
                if (terms.General != null)
                    foreach (var pair in terms.General)
                        AddNewTerm($"General/{pair.Key}", pair.Value, data);

                if (terms.Inventory != null)
                    foreach (var pair in terms.Inventory)
                    {
                        var translation = pair.Value;
                        AddNewTerm($"Inventory/item_name_{pair.Key}",
                            translation.name, data);
                        AddNewTerm($"Inventory/item_description_{pair.Key}",
                            translation.description, data);
                    }

                if (terms.Dialogues != null)
                {
                    if (terms.Dialogues.Actors != null)
                        foreach (var pair in terms.Dialogues.Actors)
                        {
                            AddNewTerm($"Dialogue System/Actor/{pair.Key}/Name", pair.Value, data);
                        }

                    if (terms.Dialogues.Variables != null)
                        foreach (var pair in terms.Dialogues.Variables)
                        {
                            AddNewTerm($"Dialogue System/Variable/{pair.Key}/Initial Value", pair.Value, data);
                        }

                    if (terms.Dialogues.Conversations != null)
                        foreach (var pair in terms.Dialogues.Conversations)
                        {
                            if (pair.Value != null)
                                foreach (var c in pair.Value)
                                    AddNewTerm($"Dialogue System/Conversation/{pair.Key}/Entry/{c.Key}/Dialogue Text",
                                        c.Value, data);
                        }
                }
            }

            return data;
        }
        public string ToJson() => Newtonsoft.Json.JsonConvert.SerializeObject(this);
    }
}
