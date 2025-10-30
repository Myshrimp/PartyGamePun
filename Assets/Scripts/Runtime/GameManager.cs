using System;
using Photon.Pun;
using ShrimpFPS.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShrimpFPS.Runtime
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        private string playerName;
        [SerializeField] private GameMode currentMode=GameMode.SinglePlayer;
        [SerializeField] private RoomManager roomManager;
        [SerializeField] private MapData startMap;
        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);
            if (!roomManager)
            {
                roomManager = GameObject.Find("RoomManager").GetComponent<RoomManager>();
            }
        }

        public void OnSelectMultiplayerOption()
        {
            currentMode = GameMode.Multiplayer;
            MenuManager.Instance.OpenMenu("MultiplayerMenu");
            MenuManager.Instance.CloseMenu("GameModesLayer");
        }

        public void OnSelectSingleplayerOption()
        {

        }

        public void ListRooms()
        {
            
        }

        public void StartMultiplayerGame()
        {
            roomManager.LoadScene(startMap.SceneID);
        }

        public void StartSingleplayerGame()
        {
            SceneManager.LoadScene(startMap.SceneID);
        }

        public GameMode GetMode()
        {
            return currentMode;
        }

        public void Login()
        {
            playerName = MenuManager.Instance.GetInputFieldString("PlayerName");
            PhotonNetwork.LocalPlayer.NickName = playerName;
            Debug.Log("Player name:"+playerName);
            MenuManager.Instance.CloseMenu("LoginMenu");
            MenuManager.Instance.OpenMenu("GameModesLayer");
        }
    }

    public enum GameMode
    {
        SinglePlayer,Multiplayer
    }
}