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
    class otDan : CustomCard
    {
        
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            Player[] players = PlayerManager.instance.players.ToArray();
            List<Player> opponents = new List<Player>();
            List<CardInfo> validCards = new List<CardInfo>();
            Player borrowedFrom = null;
            CardInfo borrowedCard = null;
            int borrowedIndex = -1;
            
            GameModeManager.AddHook(GameModeHooks.HookPointStart, cardBorrow);
            GameModeManager.AddHook(GameModeHooks.HookPointEnd, cardReturn);

            IEnumerator cardBorrow(IGameModeHandler gm)
            {
                foreach (Player p in players)
                {
                    if (p.teamID != player.teamID)
                        opponents.Add(p);
                }
                borrow();
                yield break;
            }
            IEnumerator cardReturn(IGameModeHandler gm)
            {
                if(borrowedFrom != null && borrowedCard != null)
                    returned();
                yield break;
            }

            void borrow()
            {
                if (opponents.Count() == 0)
                {
                    borrowedFrom = null;
                    borrowedCard = null;
                    return;
                }
                borrowedFrom = opponents.GetRandom<Player>();
                foreach (CardInfo c in borrowedFrom.data.currentCards.Where(c => ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, c)))
                    validCards.Add(c);
                if (validCards.Count() >= 1)
                    borrowedCard = validCards.GetRandom<CardInfo>();
                else { opponents.Remove(borrowedFrom); borrow(); }

                for(int i = 0; i < borrowedFrom.data.currentCards.Count(); i++)
                {
                    if (borrowedFrom.data.currentCards.ToArray()[i].Equals(borrowedCard))
                        borrowedIndex = i;
                }

                ModdingUtils.Utils.Cards.instance.RemoveCardFromPlayer(borrowedFrom, borrowedIndex);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, borrowedCard, false, null, 0, 0, true);
            }
            void returned()
            {
                ModdingUtils.Utils.Cards.instance.RemoveCardFromPlayer(player, player.data.currentCards.Count() - 1);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(borrowedFrom, borrowedCard, false, null, 0, 0, true);
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
            return "Hire a criminal organization to work for you\nBorrow a card from a random opponent every round";
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Common;
        }

        protected override CardInfoStat[] GetStats()
        {
            return null;
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.ColdBlue;
        }

        protected override string GetTitle()
        {
            return "otDan";
        }

        public override string GetModName()
        {
            return "Comm";
        }
    }
}