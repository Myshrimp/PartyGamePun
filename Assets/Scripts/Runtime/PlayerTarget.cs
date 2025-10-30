using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using ShrimpFPS.Extensions;
using ShrimpFPS.Helper;
using UnityEngine;
using UnityEngine.UI;

namespace ShrimpFPS.Runtime
{
    public class PlayerTarget : MonoBehaviourPunCallbacks
    {
        [SerializeField]private float health = 100;
        [SerializeField] private int score = 0;
        private PhotonView photonView;
        private NetworkInitializer networkInitializer;
        private Text healthBar;
        private Player killer;
        private Player player;
        private int bonus = 1;
        private void Awake()
        {
            networkInitializer = GetComponent<NetworkInitializer>();
            photonView = GetComponent<PhotonView>();
        }

        private void Start()
        {
            if (MenuManager.Instance.GetMenu("HealthBar"))
            {
                healthBar = MenuManager.Instance.GetMenu("HealthBar").GetComponent<Text>();
            }
            if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("health"))
            {
                health = (float)PhotonNetwork.LocalPlayer.CustomProperties["health"];
            }

            player = photonView.Owner;
        }

        public void TakeDamage(float value,Player dealer)
        {
            photonView.RPC(nameof(RPC_TakeDamage),photonView.Owner,value);
            killer = dealer;
        }

        private void Update()
        {
            if (healthBar != null && photonView.IsMine)
            {
                healthBar.text = health.ToString();
            }
            if (player.CustomProperties.ContainsKey("health"))
            {
                health = (float)player.CustomProperties["health"];
            }

            if (!photonView.IsMine)
            {
                if (health <= 0)
                {
                    Die();
                }
            }

            if (player.CustomProperties.ContainsKey("score"))
            {
                score = (int)player.CustomProperties["score"];
            }
        }

        private void Die()
        {
            if (killer!=null)
            {
                var hash = killer.CustomProperties;
                hash.TryAddInt("score",bonus);
                killer.SetCustomProperties(hash);
            }

            photonView.RPC(nameof(Respawn),photonView.Owner);
        }

        private Transform spawnPoints;
        
        [PunRPC]
        private void Respawn(PhotonMessageInfo info)
        {
            Transform respawnPoint;
            var sp = GameObject.FindWithTag("SpawnPoints");
            if (sp)
            {
                spawnPoints = sp.transform;
                respawnPoint = MapHelper.GetRandomSpawnPoint(spawnPoints);
            }
            else
            {
                Debug.Log("No spawn points find!,please tag a gameobject as SpawnPoints");
                respawnPoint = gameObject.transform;
            }

            transform.position = respawnPoint.position;
            transform.rotation = respawnPoint.rotation;

            var hash = player.CustomProperties;
            hash["health"] = 100.0f;
            player.SetCustomProperties(hash);
        }
        [PunRPC]
        private void RPC_TakeDamage(float value,PhotonMessageInfo info)
        {
            var hash = PhotonNetwork.LocalPlayer.CustomProperties;
            hash["health"] = health - value;
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
            health = (float)hash["health"];
            killer = info.photonView.Owner;
        }
    }
}