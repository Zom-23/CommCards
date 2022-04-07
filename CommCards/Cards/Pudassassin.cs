using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using UnboundLib.GameModes;
using CommCards.Extensions;

namespace CommCards.Cards
{
    //high intelligent bullets/high firepower
    class Pudassassin : CustomCard
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            player.gameObject.GetOrAddComponent<Explosion>();
            Explosion explosion = player.gameObject.GetComponent<Explosion>();
            gun.AddAttackAction(DoExplosion);
            void DoExplosion()
            {
                explosion.Explode();
            }
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "Press 'G' to launch a grenade";
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
            return CardThemeColor.CardThemeColorType.DestructiveRed;
        }

        protected override string GetTitle()
        {
            return "Pudassassin";
        }

        public override string GetModName()
        {
            return "Comm";
        }
    }

    class GrenadeLaunch : MonoBehaviour
    {
        Player player;
        Gun gun;
        void Start()
        {
            player = gameObject.GetComponent<Player>();
            gun = player.data.weaponHandler.gun;
        }

        void Go()
        {
            if (!PlayerStatus.PlayerAliveAndSimulated(player))
                return;
            
        }

        public HasToReturn DoHitEffect(HitInfo hit)
        {
            if (!hit.transform)
            {
                return HasToReturn.canContinue;
            }

            

            return HasToReturn.canContinue;
        }
    }
}
