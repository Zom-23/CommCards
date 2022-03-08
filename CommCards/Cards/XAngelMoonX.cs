using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace CommCards.Cards
{
    //Moon based effects - Effect for each of the moon phases
    //Change every 7.5 seconds
    class XAngelMoonX : CustomCard
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {

        }

        void newMoon()
        {//Invisible player
        }

        void waxingCrescent()
        {//
            //right side less than half filled
        }

        void firstQuarter()
        {//
            //right side half filled
        }

        void waxingGibbous()
        {//
            //right side over half filled
        }

        void fullMoon()
        {//Large stat bonuses
        }

        void waningGibbous()
        {//
            //left side over half filled
        }

        void thirdQuarter()
        {//
            //left side half filled
        }

        void waningCrescent()
        {//
            //left side less than half filled
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
            return "";
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
            return "";
        }

        public override string GetModName()
        {
            return "Comm";
        }
    }
}