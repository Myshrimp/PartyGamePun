using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace ShrimpFPS.Runtime
{
    public class NetworkBullet : MonoBehaviourPunCallbacks
    {
        [SerializeField] private float damage;
        private void OnCollisionEnter(Collision other)
        {
            Debug.Log("Hit something");
            var target = other.transform.GetComponent<NetworkPlayerBody>();
            if (!target)
            {
                Debug.Log("No player target script on it");
                return;
            }
            if(isSameTeam(target.photonView.Owner))Debug.Log("They are same team");
            if (target && !isSameTeam(target.photonView.Owner))
            {
                Debug.Log("hit player");
                target.OnTakeDamage(damage,PhotonNetwork.LocalPlayer);
            }
        }

        public bool isSameTeam(Player player)
        {
            var teamObject = player.CustomProperties["team"];
            if (teamObject == null || PhotonNetwork.LocalPlayer.CustomProperties["team"]==null)
                return true;
            return (int)teamObject == (int)PhotonNetwork.LocalPlayer.CustomProperties["team"];
        }
    }
}