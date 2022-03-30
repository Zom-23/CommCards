using System;
using System.Collections.Generic;
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
    //health build counter
    class Penial : CustomCard
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            Player[] players = PlayerManager.instance.players.ToArray();
            List<Player> opponents = new List<Player>();
            int halfCount = 0;

            foreach(Player p in players)
            {
                if (p.teamID != player.teamID)
                    opponents.Add(p);
            }

            GameModeManager.AddHook(GameModeHooks.HookPointStart, healthHalving);
            GameModeManager.AddHook(GameModeHooks.HookRoundEnd, healthReturn);

            IEnumerator healthHalving(IGameModeHandler gm)
            {
                halfCount++;
                foreach (Player p in opponents)
                    p.data.maxHealth *= .5f;
                yield break;
            }

            IEnumerator healthReturn(IGameModeHandler gm)
            {
                halfCount = 0;
                foreach (Player p in opponents)
                    p.data.maxHealth *= (float)Math.Pow(2.0, halfCount);
                yield break;
            }
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            statModifiers.health = .5f;
            gun.damage = .8f;
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
            return "Half your opponent's health each point/nResets on round end";
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
                    stat = "Health",
                    amount = "-50%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotLower
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "Damage",
                    amount = "-20%",
                    simepleAmount = CardInfoStat.SimpleAmount.slightlyLower
                }
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.ColdBlue;
        }

        protected override string GetTitle()
        {
            return "Penial";
        }

        public override string GetModName()
        {
            return "Comm";
        }
    }
}
