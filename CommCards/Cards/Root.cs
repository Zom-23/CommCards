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
    {
        int StatToChange;
        int SubStat;
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //adjust 5 stats, first 3 are positive changes, last 2 are negative changes
            System.Random rand = new System.Random();
            for(int i = 0; i < 5; i++)
            {
                StatToChange = rand.Next(6);
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
                    case 0: //Gun
                        switch (SubStat)
                        {
                            case 0: //attack speed
                                if(i < 3)
                                {
                                    gun.attackSpeed *= 2f;
                                }
                                else
                                {
                                    gun.attackSpeed /= 1.5f;
                                }
                                break;
                            case 1: //bursts
                                if (i < 3)
                                {
                                    gun.bursts += 2;
                                }
                                else
                                {
                                    gun.bursts = 0;
                                }
                                break;
                            case 2: //damage
                                if (i < 3)
                                {
                                    gun.damage *= 1.5f;
                                }
                                else
                                {
                                    gun.damage /= 2f;
                                }
                                break;
                            case 3: //gravity
                                if (i < 3)
                                {
                                    gun.gravity /= 1.2f;
                                }
                                else
                                {
                                    gun.gravity *= 3f;
                                }
                                break;
                            case 4: //reflects
                                if (i < 3)
                                {
                                    gun.reflects += 2;
                                }
                                else
                                {
                                    gun.reflects = 0;
                                }
                                break;
                            case 5: //reload time
                                if (i < 3)
                                {
                                    gun.reloadTime /= 2f;
                                }
                                else
                                {
                                    gun.reloadTime += .5f;
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case 1: //ammo
                        switch (SubStat)
                        {
                            case 0: //max ammo
                                if (i < 3)
                                {
                                    gunAmmo.maxAmmo += 5;
                                }
                                else
                                {
                                    gunAmmo.maxAmmo -= 3;
                                }
                                break;
                            case 1: //reload time
                                if (i < 3)
                                {
                                    gun.reloadTime /= 2f;
                                }
                                else
                                {
                                    gun.reloadTime += .5f;
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case 2: //health
                        switch (SubStat)
                        {
                            case 0: //regeneration
                                if (i < 3)
                                {
                                    health.regeneration += 5f;
                                }
                                else
                                {
                                    health.regeneration -= 3f;
                                }
                                break;
                            case 1: //max health
                                if (i < 3)
                                {
                                    data.maxHealth *= 2f;
                                }
                                else
                                {
                                    data.maxHealth *= .6f;
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case 3: //gravity
                            //gravity force
                        if (i < 3)
                        {
                            gravity.gravityForce /= 1.5f;
                        }
                        else
                        {
                            gravity.gravityForce *= 3f;
                        }
                        break;
                    case 4: //block
                        switch (SubStat)
                        {
                            case 0: //additional Blocks
                                if (i < 3)
                                {
                                    block.additionalBlocks++;
                                }
                                else
                                {
                                    block.additionalBlocks = 0;
                                }
                                break;
                            case 1: //cooldown
                                if (i < 3)
                                {
                                    block.cooldown *= .7f;
                                }
                                else
                                {
                                    block.cooldown *= 1.3f;
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case 5: //character stats
                        switch (SubStat)
                        {
                            case 0: //life steal
                                if (i < 3)
                                {
                                    characterStats.lifeSteal += .3f;
                                }
                                else
                                {
                                    characterStats.lifeSteal /= 2f;
                                }
                                break;
                            case 1: //speed
                                if (i < 3)
                                {
                                    characterStats.movementSpeed *= 1.5f;
                                }
                                else
                                {
                                    characterStats.movementSpeed *= .7f;
                                }
                                break;
                            case 2: //jumps
                                if (i < 3)
                                {
                                    characterStats.numberOfJumps += 2;
                                }
                                else
                                {
                                    characterStats.numberOfJumps = 1;
                                }
                                break;
                            case 3: //respawns
                                if (i < 3)
                                {
                                    characterStats.respawns++;
                                }
                                else
                                {
                                    characterStats.respawns = 0;
                                }
                                break;
                            case 4: //dmg over time
                                if (i < 3)
                                {
                                    characterStats.secondsToTakeDamageOver = 3f;
                                }
                                else
                                {
                                    characterStats.secondsToTakeDamageOver /= 2f;
                                }
                                break;
                            case 5: //size
                                if (i < 3)
                                {
                                    characterStats.sizeMultiplier /= 1.3f;
                                }
                                else
                                {
                                    characterStats.sizeMultiplier *= 1.3f;
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
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
            return "Randomly increase 3 stats while randomly decreasing 2 stats";
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
                    stat = "???",
                    amount = "+",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = true,
                    stat = "???",
                    amount = "+",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = true,
                    stat = "???",
                    amount = "+",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "???",
                    amount = "-",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "???",
                    amount = "-",
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
