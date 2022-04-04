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

        void Start()
        {
            this.player = this.gameObject.GetComponent<Player>();
            this.gun = player.data.weaponHandler.gun;
        }

        void Update()
        {
            if (PlayerStatus.PlayerAliveAndSimulated(player))
            {
                // get all alive players that are not this player
                List<Player> otherPlayers = PlayerManager.instance.players.Where(otherplayer => PlayerStatus.PlayerAliveAndSimulated(otherplayer) && (otherplayer.playerID != player.playerID)).ToList();

                foreach (Player otherPlayer in otherPlayers)
                {
                    if (Vector2.Distance(otherPlayer.transform.position, gun.transform.position) <= this.range && gun.sinceAttack >= gun.attackSpeed && PlayerManager.instance.CanSeePlayer(player.transform.position, otherPlayer).canSee)
                    {
                        otherPlayer.data.healthHandler.CallTakeDamage(gun.damage * Vector2.up * 55, otherPlayer.transform.position);
                        otherPlayer.data.stunHandler.AddStun(.7f);
                        gun.sinceAttack = 0f;
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
