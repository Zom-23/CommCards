using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using HarmonyLib;

namespace CommCards.Extensions
{
    [Serializable]
    public class CharacterStatModifiersAdditionalData
    {
        public int grenades;

        public CharacterStatModifiersAdditionalData()
        {
            grenades = 0;
        }
    }

    public static class CharacterStatModifiersExtension
    {
        public static readonly ConditionalWeakTable<CharacterStatModifiers, CharacterStatModifiersAdditionalData> data =
            new ConditionalWeakTable<CharacterStatModifiers, CharacterStatModifiersAdditionalData>();

        public static CharacterStatModifiersAdditionalData GetAdditionalData(this CharacterStatModifiers characterstats)
        {
            return data.GetOrCreateValue(characterstats);
        }

        public static void AddData(this CharacterStatModifiers characterstats, CharacterStatModifiersAdditionalData value)
        {
            try
            {
                data.Add(characterstats, value);
            }
            catch (Exception) { }
        }

    }

    [HarmonyPatch(typeof(CharacterStatModifiers), "ResetStats")]
    class CharacterStatModifiersPatchResetStats
    {
        private static void Prefix(CharacterStatModifiers _instance)
        {
            _instance.GetAdditionalData().grenades = 0;
        }
    }
}
