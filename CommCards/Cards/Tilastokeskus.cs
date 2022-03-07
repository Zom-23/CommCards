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
    class Tilastokeskus : CustomCard
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            block.BlockAction += mapChange(player, block);

            Action<BlockTrigger.BlockTriggerType> mapChange(Player _player, Block _block)
            {
                return delegate (BlockTrigger.BlockTriggerType trigger)
                {
                    string changeTo = MapManager.instance.levels[UnityEngine.Random.Range(0, MapManager.instance.levels.Length)];
                    Player[] playerData = PlayerManager.instance.players.ToArray();

                    MapManager.instance.LoadNextLevel(true, true);
                    MapManager.instance.RPCA_CallInNewMapAndMovePlayers(MapManager.instance.currentLevelID);
                    Player[] newPlayerData = PlayerManager.instance.players.ToArray();
                    for (int i = 0; i < playerData.Length; i++)
                    {
                        newPlayerData[i] = playerData[i];
                    }

                    //MapManager.instance.UnloadScene(MapManager.instance.currentMap.Scene);
                    //MapManager.instance.RPCA_LoadLevel(changeTo);
                    //MapManager.instance.RPCA_CallInNewMapAndMovePlayers(int.Parse(changeTo));
                    //PlayerManager.instance.RPCA_MovePlayers();
                    //PlayerManager.instance.SetPlayersSimulated(true);
                    //MapManager.instance.LoadLevelFromID(int.Parse(MapManager.instance.levels[UnityEngine.Random.Range(0, MapManager.instance.levels.Length)]));
                    
                };
            }
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            block.cdAdd = 10f;
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
            return "Change the map when you block";
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
                    positive = false,
                    stat = "Block Cooldown",
                    amount = "+10s",
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
            return "Tilastokeskus";
        }

        public override string GetModName()
        {
            return "Comm";
        }
    }
}