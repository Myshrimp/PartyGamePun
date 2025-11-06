using System;
using System.Collections.Generic;
using GameFramework.Event;
using Party.Base;
using Party.Event;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ShrimpFPS.Runtime
{
    public class RoomManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private bool autoSyncScene;
        private List<RoomInfo> _existing_rooms;

        public Action JoinedLobby;
        private void Awake()
        {
            _existing_rooms = new List<RoomInfo>();
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

        public void FetchRooms()
        {
            RoomUpdateArgs room_args = new RoomUpdateArgs();
            room_args.room_infos = _existing_rooms;
            GameEntry.Event.FireNow(this, room_args);
        }
        public void CreateAndJoinRoom(string room_name)
        {
            PhotonNetwork.CreateRoom(room_name);
            Debug.Log("Created a room");
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
            GameEntry.UI.OpenUIForm(GameEntry.Config.GetString("StartView"), "MainMenu", false, this);
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
            Debug.Log("Connected to master");
        }
        public override void OnRoomListUpdate(List<RoomInfo> infos)
        {
            RoomUpdateArgs room_args = new RoomUpdateArgs();
            room_args.room_infos = infos;
            _existing_rooms = infos;
            GameEntry.Event.FireNow(this, room_args);
            Debug.Log("Room list updated");
        }

        public override void OnJoinedRoom()
        {

        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            NewPlayerArgs player_args = new NewPlayerArgs();
            player_args.player = newPlayer;
            GameEntry.Event.FireNow(this, player_args);
            Debug.Log("Player entered room:"+newPlayer.UserId);
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
    
    public class RoomUpdateArgs : GameEventArgs
    {
        public List<RoomInfo> room_infos;
        
        public override void Clear()
        {
            room_infos.Clear();
        }

        public override int Id
        {
            get { return (int)EventTypes.OnRoomListUpdate; }
        }
    }

    public class NewPlayerArgs : GameEventArgs
    {
        public Player player;
        public override void Clear()
        {
            
        }

        public override int Id
        {
            get { return (int)EventTypes.OnNewPlayerJoined; }
        }
    }
}