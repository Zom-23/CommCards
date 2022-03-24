using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using ModdingUtils.MonoBehaviours;

namespace CommCards.Cards
{
    class Orangenalname : CustomCard
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            health.gameObject?.transform?.Find("Limbs")?.gameObject?.SetActive(false);
            characterStats.movementSpeed *= 1.5f;
            InAirJumpEffect flight = player.gameObject.GetOrAddComponent<InAirJumpEffect>();
            flight.SetJumpMult(0.1f);
            flight.AddJumps(int.MaxValue);
            flight.SetCostPerJump(1);
            flight.SetContinuousTrigger(true);
            flight.SetResetOnWallGrab(true);
            flight.SetInterval(0.1f);
            gravity.gravityForce = .01f;
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {

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
            return "Your legs are gone ¯\\_(ツ)_/¯";
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
                    positive = true,
                    stat = "",
                    amount = "",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
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
            return "Orangenalname";
        }

        public override string GetModName()
        {
            return "Comm";
        }
    }
}