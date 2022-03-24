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
using ModdingUtils.MonoBehaviours;

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
                ReversibleColorEffect colorEffect = player.gameObject.AddComponent<ReversibleColorEffect>();
                colorEffect.SetColor(Color.clear);
                player.ExecuteAfterSeconds(7.5f, () => { colorEffect.Destroy(); });
            }

            void waxingCrescent()
            {//grow effect
             //right side less than half filled
                ReversibleEffect tempStats = player.gameObject.AddComponent<ReversibleEffect>();
                tempStats.gun.damageAfterDistanceMultiplier *= 1.5f;
                player.ExecuteAfterSeconds(7.5f, () => { tempStats.Destroy(); });
            }

            void firstQuarter()
            {//flight
             //right side half filled
                InAirJumpEffect flight = player.gameObject.GetOrAddComponent<InAirJumpEffect>();
                flight.SetJumpMult(0.1f);
                flight.AddJumps(100);
                flight.SetCostPerJump(1);
                flight.SetContinuousTrigger(true);
                flight.SetResetOnWallGrab(true);
                flight.SetInterval(0.1f);

                var prevGrav = gravity.gravityForce;
                gravity.gravityForce = 0.01f;

                player.ExecuteAfterSeconds(7.5f, () => { flight.Destroy(); gravity.gravityForce = prevGrav; });

            }

            void waxingGibbous()
            {//
             //right side over half filled
            }

            void fullMoon()
            {//Large stat bonuses
             //blood moon chance - give all effects or special berserker effect
                ReversibleEffect tempStats = player.gameObject.AddComponent<ReversibleEffect>();
                tempStats.gun.damage *= 2f;
                tempStats.gun.reflects++;
                tempStats.gun.reloadTime /= 2f;
                tempStats.data.stats.regen += 3f;
                tempStats.data.health *= 3f;

                player.ExecuteAfterSeconds(7.5f, () => { tempStats.Destroy(); });

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
                ReversibleEffect tempStats = player.gameObject.AddComponent<ReversibleEffect>();
                tempStats.block.cooldown /= 2f;

                player.ExecuteAfterSeconds(7.5f, () => { tempStats.Destroy(); });
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