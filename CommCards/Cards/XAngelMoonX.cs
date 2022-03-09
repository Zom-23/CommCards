using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using CommCards.Extensions;
using UnboundLib.GameModes;

namespace CommCards.Cards
{
    //Moon based effects - Effect for each of the moon phases
    //Change every 7.5 seconds
    class XAngelMoonX : CustomCard
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            int[] phase = new int[] { 0, 1, 2, 3, 4, 5, 6, 7 };
            int currentPhase = 0;

            GameModeManager.AddHook(GameModeHooks.HookBattleStart, startCycle);

            IEnumerator startCycle(IGameModeHandler gm)
            {
                phaseSwap(); yield break;
            }

            void phaseSwap()
            {
                switch (currentPhase)
                {
                    case 0:
                        newMoon(); break;
                    case 1:
                        waxingCrescent(); break;
                    case 2:
                        firstQuarter(); break;
                    case 3:
                        waxingGibbous(); break;
                    case 4:
                        fullMoon(); break;
                    case 5:
                        waningGibbous(); break;
                    case 6:
                        thirdQuarter(); break;
                    case 7:
                        waningCrescent(); currentPhase = -1; break;
                    default:
                        break;
                }

                if (PlayerStatus.PlayerAliveAndSimulated(player))
                {
                    player.ExecuteAfterSeconds(7.5f, () =>
                    { currentPhase++; phaseSwap(); });
                }
                else if (currentPhase == -1)
                    currentPhase = 0;
            }

            void newMoon()
            {//Invisible player
            }

            void waxingCrescent()
            {//grow effect
             //right side less than half filled
            }

            void firstQuarter()
            {//flight
             //right side half filled
            }

            void waxingGibbous()
            {//
             //right side over half filled
            }

            void fullMoon()
            {//Large stat bonuses
             //blood moon chance - give all effects or special berserker effect
            }

            void waningGibbous()
            {//
             //left side over half filled
            }

            void thirdQuarter()
            {//meteor shower
             //left side half filled
            }

            void waningCrescent()
            {//lowered block cooldown
             //left side less than half filled
            }
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