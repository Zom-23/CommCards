using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using CommCards.MonoBehaviours;
using HarmonyLib;
using CommCards.Extensions;

namespace CommCards.Cards
{
    class Poppycars : CustomCard
    {

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            gun.reflects += 3;
            characterStats.GetAdditionalData().hasPoppy = true;
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            characterStats.GetAdditionalData().hasPoppy = false;
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "Move faster each bounce & gain a bounce every 5 bounces";
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Rare;
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat
                {
                    positive = true,
                    stat = "Bounces",
                    amount = "+3",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.ColdBlue;
        }

        protected override string GetTitle()
        {
            return "Poppycars";
        }

        public override string GetModName()
        {
            return "Comm";
        }
    }
    
    [HarmonyPatch(typeof(RayHitReflect), "DoHitEffect")]
    class ReflectEffectPatch
    {
        private static void Postfix(RayHitReflect __instance)
        {
            Player player = __instance.GetComponent<ProjectileHit>().ownPlayer;
            if (player.data.stats.GetAdditionalData().hasPoppy)
            {
                player.data.stats.movementSpeed *= 1.05f;
                player.data.stats.GetAdditionalData().bounceCount++;
            }
            if (player.data.stats.GetAdditionalData().bounceCount % 5 == 0 && player.data.stats.GetAdditionalData().hasPoppy)
            {
                player.data.weaponHandler.gun.reflects++;
                player.data.stats.GetAdditionalData().bounceCount = 0;
            }
                

        }
    }
}
