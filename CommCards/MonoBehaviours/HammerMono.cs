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
    class HammerMono : MonoBehaviour
    {
        Player player;
        Gun gun;
        readonly float range = 1.75f;

        void Awake()
        {
            this.player = this.gameObject.GetComponent<Player>();
            this.gun = this.gameObject.GetComponent<Gun>();
        }

        void Update()
        {
            // if any player (friendlies included) is touched (i.e. within a very small range) turn them into gold
            if (PlayerStatus.PlayerAliveAndSimulated(this.player))
            {
                // get all alive players that are not this player
                List<Player> otherPlayers = PlayerManager.instance.players.Where(player => PlayerStatus.PlayerAliveAndSimulated(player) && (player.playerID != this.player.playerID)).ToList();

                Vector2 displacement;

                foreach (Player otherPlayer in otherPlayers)
                {
                    displacement = otherPlayer.transform.position - this.gun.transform.position;
                    if (displacement.magnitude <= this.range)
                    {
                        // if the other player is within range, then add the gold effect to them
                            otherPlayer.data.healthHandler.CallTakeDamage(new Vector3(gun.damage, 1), otherPlayer.transform.position);
                    }

                }
            }

        }

        public void Destroy()
        {
            UnityEngine.Object.Destroy(this);
        }
    }
}
