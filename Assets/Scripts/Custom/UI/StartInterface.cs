using UnityEngine;
using UnityEngine.UI;
using GameEntry = Party.Base.GameEntry;
namespace Party.Custom.UI
{
    public class StartInterface:CustomUILogic
    {
        [SerializeField] private Button _start_btn;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            _start_btn = transform.Find("StartBtn").GetComponent<Button>();
            _start_btn.onClick.AddListener(Close);
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
        }

        private void Close()
        {
            GameEntry.UI.CloseUIForm(m_ui_form);
        }
    }
}