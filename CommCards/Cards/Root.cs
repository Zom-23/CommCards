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
        
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //adjust 5 stats, first 3 are positive changes, last 2 are negative changes
            
            System.Random rand = new System.Random();
            int StatToChange;
            int SubStat = 0;
            bool positive = true;
            for (int i = 0; i < 5; i++)
            {
                StatToChange = rand.Next(6);
                if (i < 3)
                    positive = true;
                else
                    positive = false;
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

                if (data.view.IsMine)
                {
                    GetComponent<PhotonView>().RPC("RPCA_SetStats", RpcTarget.All, new object[] { StatToChange, SubStat, player, positive });
                }
            }
        }

        [PunRPC]
        public static void RPCA_SetStats(int statChange, int sub, Player player, bool posneg)
        {
            Gun gun = player.data.weaponHandler.gun;
            CharacterData data = player.data;
            HealthHandler health = player.data.healthHandler;
            CharacterStatModifiers characterStats = player.data.stats;
            Block block = player.data.block;
            switch (statChange)
            {
                case 0: //Gun
                    switch (sub)
                    {
                        case 0: //attack speed
                            if (posneg)
                            {
                                gun.attackSpeed *= 2f;
                            }
                            else
                            {
                                gun.attackSpeed /= 1.5f;
                            }
                            break;
                        case 1: //bursts
                            if (posneg)
                            {
                                gun.bursts += 2;
                            }
                            else
                            {
                                gun.bursts = 0;
                            }
                            break;
                        case 2: //damage
                            if (posneg)
                            {
                                gun.damage *= 1.5f;
                            }
                            else
                            {
                                gun.damage /= 2f;
                            }
                            break;
                        case 3: //gravity
                            if (posneg)
                            {
                                gun.gravity /= 1.2f;
                            }
                            else
                            {
                                gun.gravity *= 3f;
                            }
                            break;
                        case 4: //reflects
                            if (posneg)
                            {
                                gun.reflects += 2;
                            }
                            else
                            {
                                gun.reflects = 0;
                            }
                            break;
                        case 5: //reload time
                            if (posneg)
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
                    switch (sub)
                    {
                        case 0: //max ammo
                            if (posneg)
                            {
                                gun.ammo += 5;
                            }
                            else
                            {
                                gun.ammo -= 3;
                            }
                            break;
                        case 1: //reload time
                            if (posneg)
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
                    switch (sub)
                    {
                        case 0: //regeneration
                            if (posneg)
                            {
                                health.regeneration += 5f;
                            }
                            else
                            {
                                health.regeneration -= 3f;
                            }
                            break;
                        case 1: //max health
                            if (posneg)
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
                    if (posneg)
                    {
                        characterStats.gravity /= 1.5f;
                    }
                    else
                    {
                        characterStats.gravity *= 3f;
                    }
                    break;
                case 4: //block
                    switch (sub)
                    {
                        case 0: //additional Blocks
                            if (posneg)
                            {
                                block.additionalBlocks++;
                            }
                            else
                            {
                                block.additionalBlocks = 0;
                            }
                            break;
                        case 1: //cooldown
                            if (posneg)
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
                    switch (sub)
                    {
                        case 0: //life steal
                            if (posneg)
                            {
                                characterStats.lifeSteal += .3f;
                            }
                            else
                            {
                                characterStats.lifeSteal /= 2f;
                            }
                            break;
                        case 1: //speed
                            if (posneg)
                            {
                                characterStats.movementSpeed *= 1.5f;
                            }
                            else
                            {
                                characterStats.movementSpeed *= .7f;
                            }
                            break;
                        case 2: //jumps
                            if (posneg)
                            {
                                characterStats.numberOfJumps += 2;
                            }
                            else
                            {
                                characterStats.numberOfJumps = 1;
                            }
                            break;
                        case 3: //respawns
                            if (posneg)
                            {
                                characterStats.respawns++;
                            }
                            else
                            {
                                characterStats.respawns = 0;
                            }
                            break;
                        case 4: //dmg over time
                            if (posneg)
                            {
                                characterStats.secondsToTakeDamageOver = 3f;
                            }
                            else
                            {
                                characterStats.secondsToTakeDamageOver /= 2f;
                            }
                            break;
                        case 5: //size
                            if (posneg)
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
    {//block when you take damage
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            characterStats.WasDealtDamageAction = blockAgain;

            void blockAgain(Vector2 damage, bool selfDamage)
            {
                block.CallDoBlock(true, true);
            }
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            statModifiers.health = 1.75f;
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
            return "Block immediately after taking damage";
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
                    stat = "Health",
                    amount = "+75%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotOf
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
