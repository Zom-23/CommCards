using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using UnityEngine;
using BepInEx;
using UnboundLib;
using UnboundLib.GameModes;
using UnboundLib.Cards;
using UnboundLib.Utils;
using CommCards.Cards;
using Photon.Pun;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
//using CommCards.MonoBehaviours;
//Mod created by Zom_23 for the game ROUNDS based on the Modding Community

namespace CommCards
{

    //Mods required for this to work
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    //[BepInDependency("com.willuwontu.rounds.RespawnPatch", BepInDependency.DependencyFlags.HardDependency)]

    [BepInPlugin(ModId, ModName, Version)] //Make it an acutal plugin
    [BepInProcess("Rounds.exe")]

    public class CommCards : BaseUnityPlugin
    {
        private const string ModId = "com.Comm.rounds.card";
        private const string ModName = "Community Cards";
        public const string Version = "0.0.1"; //(major.minor.patch)



        //Start up the Cards!!
        public void Start()
        {
            UnityEngine.Debug.Log("[Community Cards] Loading Cards");
            CustomCard.BuildCard<Tilastokeskus>();
            CustomCard.BuildCard<PykessI>();
            CustomCard.BuildCard<PykessII>();
            CustomCard.BuildCard<PykessIII>();
            CustomCard.BuildCard<PykessIV>();
            CustomCard.BuildCard<Pykess>();
            CustomCard.BuildCard<Poppycars>();
            CustomCard.BuildCard<HatchetDaddy>();
            CustomCard.BuildCard<Zom_23>();
            CustomCard.BuildCard<Orangenalname>();
            CustomCard.BuildCard<otDan>();
            CustomCard.BuildCard<Ascyst>();
            CustomCard.BuildCard<RS_Mind>();
            CustomCard.BuildCard<Pudassassin>();
        }
    }
}