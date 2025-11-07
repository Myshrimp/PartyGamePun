using UnityEngine;
using UnityEngine.UI;
using Party.Base;

namespace Party.Custom.UI
{
    public class StartInterface:CustomUILogic
    {
        [SerializeField] private Button _btn_rooms;
        [SerializeField] private Button _btn_create_room;
        [SerializeField] private InputField _txt_input_room_name;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            _btn_rooms.onClick.AddListener(OpenRoomList);
            _btn_create_room.onClick.AddListener(CreateRoom);
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
        }

        private void CreateRoom()
        {
            string ui_path = GameEntry.Config.GetString("RoomView");
            GameEntry.UI.OpenUIForm(ui_path, "MainMenu", true);
        }

        private void Close()
        {
            GameEntry.UI.CloseUIForm(m_ui_form);
        }

        private void OpenRoomList()
        {
            Debug.Log("Opening room list");
            string ui_path = GameEntry.Config.GetString("RoomListView");
            GameEntry.UI.OpenUIForm(ui_path,"MainMenu", true);
        }
    }
}