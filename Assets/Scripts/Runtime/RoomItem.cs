using System;
using UnityEngine;
using UnityEngine.UI;

namespace ShrimpFPS.Runtime
{
    public class RoomItem : MonoBehaviour
    {
        [SerializeField] private Text roomNameText;
        [SerializeField] private Text playerCountText;
        private RoomManager roomManager;
        private string roomName;

        private void Awake()
        {
            roomManager = GameObject.FindWithTag("RoomManager").GetComponent<RoomManager>();
        }

        public void SetRoomName(string name)
        {
            roomNameText.text = name;
            roomName = name;
        }

        public void SetPlayerCount(int count)
        {
            playerCountText.text = count.ToString();
        }

        public void JoinRoom()
        {
            roomManager.JoinRoom(roomName);
        }
    }
}