using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using ModdingUtils.MonoBehaviours;
using CommCards.Extensions;
using Photon.Pun;

namespace CommCards.MonoBehaviours
{
    class BounceStatChanges : RayHitReflect
    {
        Player player;
        ProjectileHit projHit;
        int BounceCount = 0;

        void Start()
        {
            this.player = this.GetComponent<Player>();
            this.projHit = this.GetComponent<ProjectileHit>();
        }

        public override HasToReturn DoHitEffect(HitInfo hit)
        {
            projHit.ownPlayer.data.stats.movementSpeed *= 1.125f;
            BounceCount++;
            UnityEngine.Debug.Log($"Bounce Count: {BounceCount}");
            if (BounceCount % 5 == 0)
                projHit.ownPlayer.data.weaponHandler.gun.reflects++;
            return HasToReturn.hasToReturn;
        }
    }
}
