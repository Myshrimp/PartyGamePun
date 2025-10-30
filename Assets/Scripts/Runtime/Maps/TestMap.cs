using System;
using System.Collections.Generic;
using Photon.Pun;
using ShrimpFPS.Helper;
using ShrimpFPS.Interfaces;
using UnityEngine;

namespace ShrimpFPS.Runtime.Maps
{
    public class TestMap : MapInitializer,ISubject
    {
        private Dictionary<SubjectEvents, Action> events;
        private void Start()
        {
            events = new Dictionary<SubjectEvents, Action>();
            InitMap();
            SpawnPlayer();
            InitializePlayer();
            currentLocalPlayer.SetActive(false);
        }

        public void RegisterEvent(SubjectEvents evt,Action act)
        {
            events[evt] = act;
        }

        public void PublishEvent(SubjectEvents evt)
        {
            if (!events.ContainsKey(evt)) return;
            events[evt]?.Invoke();
        }

        public void ChooseATeam(int id)
        {
            currentLocalPlayer.SetActive(true);
            currentLocalPlayer.GetComponent<NetworkInitializer>().ChooseTeam(id);
            MenuManager.Instance.CloseMenu("ChooseTeamMenu");
            PublishEvent(SubjectEvents.OnChoseATeam);
        }
        public override void InitMap()
        {
            
        }

        public override void InitializePlayer()
        {
            
        }

        public override void SpawnPlayer()
        {
            string playerToSpawn = mapData.offlinePlayer.name;
            if (GameManager.Instance.GetMode() == GameMode.Multiplayer)
            {
                playerToSpawn = mapData.onlinePlayer.name;
                Transform spawnPoint = MapHelper.GetRandomSpawnPoint(spawnPoints);
                currentLocalPlayer = PhotonNetwork.Instantiate(playerToSpawn, spawnPoint.position,
                    spawnPoint.rotation);
            }
            else if (GameManager.Instance.GetMode() == GameMode.SinglePlayer)
            {
                Transform spawnPoint = MapHelper.GetRandomSpawnPoint(spawnPoints);
                currentLocalPlayer = Instantiate(mapData.offlinePlayer, spawnPoint.position,
                    spawnPoint.rotation);
            }
        }
    }
}