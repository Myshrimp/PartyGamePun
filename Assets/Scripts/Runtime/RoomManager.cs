using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace ShrimpFPS.Runtime
{
    public class RoomManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private bool autoSyncScene = true;
        [SerializeField] private InputField roomNameText;
        [SerializeField] private GameObject roomPrefab;
        [SerializeField] private GameObject playerInfoPrefab;
        [SerializeField] private Transform roomListTransform;
        [SerializeField] private Transform playerListTransform;
        private List<Room> existingRooms;

        public Action JoinedLobby;
        private void Awake()
        {
            existingRooms = new List<Room>();
            ConnectToChina();
            DontDestroyOnLoad(this);
        }

        void ConnectToChina()
        {
            PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "cn";
            PhotonNetwork.PhotonServerSettings.AppSettings.UseNameServer = true;
            PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime =
                "cc11d9a2-e7a4-4475-8891-f2c8de77cb90"; // 替换为您自己的国内区appID
            PhotonNetwork.PhotonServerSettings.AppSettings.Server = "ns.photonengine.cn";
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.LocalPlayer.NickName = Random.Range(0, 10000).ToString();
        }

        public void CreateAndJoinRoom()
        {
            PhotonNetwork.CreateRoom(roomNameText.text);
        }

        public void JoinRoom(string room_id)
        {
            PhotonNetwork.JoinRoom(room_id);
        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();
            Debug.Log("Successfully joined lobby");
            PhotonNetwork.AutomaticallySyncScene = autoSyncScene;
            JoinedLobby?.Invoke();
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
            Debug.Log("Connected to master");
        }
        public override void OnRoomListUpdate(List<RoomInfo> infos)
        {
            Debug.Log("Room list update");
            foreach(Transform trans in roomListTransform)
            {
                Destroy(trans);
            }

            for (int i = 0; i < infos.Count; i++)
            {
                Instantiate(roomPrefab, roomListTransform).GetComponent<RoomItem>().SetRoomName(infos[i].Name);
            }
        }

        public override void OnJoinedRoom()
        {
            MenuManager.Instance.OpenMenu("RoomMenu");
            MenuManager.Instance.CloseMenu("MultiplayerMenu");
            foreach (var player in PhotonNetwork.PlayerList)
            {
                Instantiate(playerInfoPrefab, playerListTransform).GetComponent<PlayerInfoItem>().SetUp(player.NickName);
            }
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Instantiate(playerInfoPrefab, playerListTransform).GetComponent<PlayerInfoItem>().SetUp(newPlayer.NickName);
        }

        public void LoadScene(int sceneID)
        {
            PhotonNetwork.LoadLevel(sceneID);
        }

        public void SetPlayerNickName(string name)
        {
            PhotonNetwork.LocalPlayer.NickName = name;
        }
    }

    public class Room
    {
        public GameObject roomPrefab;
        public string name;
        public int count;
    }
}