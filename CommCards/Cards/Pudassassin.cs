using System.Collections.Generic;
using UnboundLib.Cards;
using UnityEngine;
using System.Linq;
using CommCards.MonoBehaviours;
using System.Reflection;
using Photon.Pun;
using System.Collections.ObjectModel;
using UnboundLib.Utils;
using CommCards.Extensions;
using HarmonyLib;
using System.Runtime.CompilerServices;
using System;
using InControl;
using ModdingUtils.MonoBehaviours;
using UnboundLib;
using System.Collections;

namespace CommCards.Cards
{
    //high intelligent bullets/high firepower
    class Pudassassin : CustomCard
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            player.gameObject.GetOrAddComponent<GrenadeLaunch>();
            
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
        ReversibleEffect grenade;
        void Start()
        {
            player = gameObject.GetComponent<Player>();
            gun = player.data.weaponHandler.gun;
            
        }

        void Go()
        {
            if (!PlayerStatus.PlayerAliveAndSimulated(player))
                return;
            buildGrenade();
            this.player.data.weaponHandler.gun.Attack(0f, true, gun.damage, 1f, false);
        }

        void buildGrenade()
        {
            grenade = new ReversibleEffect();
            grenade.gun.damage *= 2f;


            //Code taken from Boss Sloth
            var explosiveBullet = (GameObject)Resources.Load("0 cards/Explosive bullet");
            var a_Explosion = explosiveBullet.GetComponent<Gun>().objectsToSpawn[0].effect;
            var explo = Instantiate(a_Explosion);
            explo.transform.position = new Vector3(1000, 0, 0);
            explo.hideFlags = HideFlags.HideAndDontSave;
            explo.name = "customExplo";
            DestroyImmediate(explo.GetComponent<RemoveAfterSeconds>());
            var explodsion = explo.GetComponent<Explosion>();
            explodsion.force = 100000;
            gun.damage = 2f;

            grenade.gun.objectsToSpawn = new[]
            {
                new ObjectsToSpawn
                {
                    effect = explo,
                    normalOffset = 0.1f,
                    numberOfSpawns = 1,
                    scaleFromDamage = .5f,
                    scaleStackM = 0.7f,
                    scaleStacks = true,
                },
                new ObjectsToSpawn
                {
                    scaleFromDamage = 0f
                }
            };
        }
    }

    [HarmonyPatch(typeof(PlayerActions))]
    [HarmonyPatch(MethodType.Constructor)]
    [HarmonyPatch(new Type[] { })]
    class PlayerActionsPatchPlayerActions
    {
        private static void Postfix(PlayerActions __instance)
        {
            __instance.GetAdditionalData().switchWeapon = (PlayerAction)typeof(PlayerActions).InvokeMember("CreatePlayerAction",
                                    BindingFlags.Instance | BindingFlags.InvokeMethod |
                                    BindingFlags.NonPublic, null, __instance, new object[] { "Switch Weapon" });

        }
    }
    [HarmonyPatch(typeof(PlayerActions), "CreateWithKeyboardBindings")]
    class PlayerActionsPatchCreateWithKeyboardBindings
    {
        private static void Postfix(ref PlayerActions __result)
        {
            __result.GetAdditionalData().switchWeapon.AddDefaultBinding(Key.G);
        }
    }
}
