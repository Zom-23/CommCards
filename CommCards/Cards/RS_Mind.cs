using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using UnboundLib.GameModes;

namespace CommCards.Cards
{
    //bad outside of highly specific build
    //greatly decrease damage and slowly increase the damage the longer the round goes
    class RS_Mind : CustomCard
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            int damageIncreases = 0;
            bool continueIncrease = true;
            GameModeManager.AddHook(GameModeHooks.HookRoundStart, startIncrease);
            GameModeManager.AddHook(GameModeHooks.HookRoundEnd, damageDecrease);

            IEnumerator startIncrease(IGameModeHandler gm)
            {
                continueIncrease = true;
                increaseDmg(); 
                yield break;
            }
            IEnumerator damageDecrease(IGameModeHandler gm)
            {
                continueIncrease = false;
                gun.damage /= (float)Math.Pow(1.01, damageIncreases);
                yield break;
            }
            void increaseDmg()
            {
                damageIncreases++;
                player.data.ExecuteAfterFrames(10, () =>
                { gun.damage *= 1.05f; });
                if (continueIncrease)
                    increaseDmg();
            }
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.damage = .01f;
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {

        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "Continously increasing damage during a round";
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Common;
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat
                {
                    positive = false,
                    stat = "Damage",
                    amount = "-99%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotLower
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "",
                    amount = "",
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
            return "RS_Mind";
        }

        public override string GetModName()
        {
            return "Comm";
        }
    }
}
