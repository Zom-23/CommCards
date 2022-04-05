using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using UnboundLib.GameModes;

namespace CommCards.Cards
{
    //high intelligent bullets/high firepower
    class Pudassassin : CustomCard
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            player.gameObject.GetOrAddComponent<RadarShot>();
            RadarShot rs = player.gameObject.GetComponent<RadarShot>();

            //gun.ShootPojectileAction += onShoot();
            gun.AddAttackAction(onShoot);


            void onShoot()
            {
                rs.Go();
            }
                
            /*
            Action<UnityEngine.GameObject> onShoot()
            {
                return delegate (UnityEngine.GameObject trigger)
                {
                    rs.Go();
                };     
            }*/
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "Automatically hit people around you when you fire";
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
}
