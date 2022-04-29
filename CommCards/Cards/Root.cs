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
using Photon.Pun;

namespace CommCards.Cards
{
    //tess - like chaos
    //lilith - cool monos/"deals"
    class Root
    {
    }

    class Tess : CustomCard
    {//Experimental treatment effect
        int StatToChange;
        int SubStat;
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //0 = gun stat, 1 = ammo, 2 = health, 3 = gravity, 4 = block, 5 = stats
            System.Random rand = new System.Random();
            StatToChange = rand.Next(6);
            //Gun: 0 = attack speed, 1 = bursts, 2 = damage, 3 = gravity, 4 = reflects, 5 = reload time
            //Ammo: 0 = max ammo, 1 = reload time
            //Health: 0 = regeneration, 1 = max health
            //Gravity: Gravity force
            //Block: 0 = additional Blocks, 1 = cooldown
            //Stats: 0 = life steal, 1 = speed, 2 = jumps, 3 = respawns, 4 = dmg over time, 5 = size
            switch (StatToChange)
            {
                case 0:
                    SubStat = rand.Next(6);
                    break;
                case 1:
                    SubStat = rand.Next(2);
                    break;
                case 2:
                    SubStat = rand.Next(2);
                    break;
                case 3:
                    SubStat = 0;
                    break;
                case 4:
                    SubStat = rand.Next(2);
                    break;
                case 5:
                    SubStat = rand.Next(6);
                    break;
                default:
                    break;
            }

            base.GetComponent<PhotonView>().RPC("RPCA_SetStats", RpcTarget.All, new object[] { StatToChange, SubStat });

            switch (StatToChange)
            {
                case 0:
                    switch (SubStat)
                    {
                        case 0:
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                        default:
                            break;
                    }
                    break;
                case 1:
                    switch (SubStat)
                    {
                        case 0:
                            break;
                        case 1:
                            break;
                        default:
                            break;
                    }
                    break;
                case 2:
                    switch (SubStat)
                    {
                        case 0:
                            break;
                        case 1:
                            break;
                        default:
                            break;
                    }
                    break;
                case 3:
                    
                    break;
                case 4:
                    switch (SubStat)
                    {
                        case 0:
                            break;
                        case 1:
                            break;
                        default:
                            break;
                    }
                    break;
                case 5:
                    switch (SubStat)
                    {
                        case 0:
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }



        }

        [PunRPC]
        public void RPCA_SetStats(int statChange, int sub)
        {
            this.StatToChange = statChange;
            this.SubStat = sub;
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
            return "";
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
            return "Root: Tess";
        }

        public override string GetModName()
        {
            return "Comm";
        }
    }

    class Lilith : CustomCard
    {//Necromancy
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {

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
            return "";
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
            return "Root: Lilith";
        }

        public override string GetModName()
        {
            return "Comm";
        }
    }
}
