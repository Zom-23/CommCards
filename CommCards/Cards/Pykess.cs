using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using UnboundLib.GameModes;
using Photon.Pun;
using Photon;

namespace CommCards.Cards
{
    public abstract class PykessBase : CustomCard
    {
        internal static CardCategory category = CustomCardCategories.instance.CardCategory("Pykess");

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            cardInfo.categories = new CardCategory[] { PykessBase.category };
            int PykessCount = 0;

            foreach (CardInfo c in gun.player.data.currentCards)
            {
                if(c.cardName == "Pykess I" || c.cardName == "Pykess II" || c.cardName == "Pykess III" || c.cardName == "Pykess IV")
                {
                    PykessCount++;
                }
            }

            if (PykessCount == 4)
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(gun.player, Pykess.self, addToCardBar: true);
        }

        protected override string GetDescription()
        {
            return "Collect all parts of pykess to get something special~";
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {}

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Common;
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.MagicPink;
        }

        public override string GetModName()
        {
            return "Comm";
        }
    }

    public class PykessI : PykessBase
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            gun.reloadTime *= .9f;
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat
                {
                    positive = true,
                    stat = "Reload Speed",
                    amount = "+10%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
                }
            };
        }

        protected override string GetTitle()
        {
            return "Pykess I";
        }
    }

    public class PykessII : PykessBase
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            block.cooldown *= .9f;
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat
                {
                    positive = true,
                    stat = "Block Cooldown",
                    amount = "-10%",
                    simepleAmount = CardInfoStat.SimpleAmount.slightlyLower
                }
            };
        }

        protected override string GetTitle()
        {
            return "Pykess II";
        }
    }

    public class PykessIII : PykessBase
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            characterStats.movementSpeed *= 1.1f;
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat
                {
                    positive = true,
                    stat = "Movement Speed",
                    amount = "+10%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
                }
            };
        }

        protected override string GetTitle()
        {
            return "Pykess III";
        }
    }

    public class PykessIV : PykessBase
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            gun.damage *= 1.1f;
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat
                {
                    positive = true,
                    stat = "Damage",
                    amount = "+10%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
                }
            };
        }

        protected override string GetTitle()
        {
            return "Pykess IV";
        }
    }

    public class Pykess : CustomCard
    {
        internal static CardInfo self = null;

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            GameModeManager.AddHook(GameModeHooks.HookRoundStart, murderEffect);
            

            IEnumerator murderEffect(IGameModeHandler gm)
            {
                List<Player> oppPlayers = new List<Player>(PlayerManager.instance.players.ToArray().Where(p => p.teamID != player.teamID));

                foreach (Player oppPlayer in oppPlayers)
                {
                    Unbound.Instance.ExecuteAfterSeconds(2f, delegate
                    {
                        oppPlayer.data.view.RPC("RPCA_Die", RpcTarget.All, new object[]
                        {
                                    new Vector2(0, 1)
                        });

                    });
                }


                yield break;
            }
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "Murder your opponents at the start of each round!";
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Rare;
        }

        protected override CardInfoStat[] GetStats()
        {
            return null;
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.MagicPink;
        }

        protected override string GetTitle()
        {
            return "Completed Pykess";
        }

        public override bool GetEnabled()
        {
            return false;
        }
    }
}