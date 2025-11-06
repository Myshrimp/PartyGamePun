using System;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Party.Base
{
    /// <summary>
    /// 游戏入口。
    /// </summary>
    public partial class GameEntry : MonoBehaviour
    {
        public static Action OnGameEntryReady;
        public bool GameInit = false;
        public static ItemComponent Item
        {
            get;
            private set;
        }

        public static DataComponent Data
        {
            get;
            private set;
        }


        private static void InitCustomComponents()
        {
            Item = UnityGameFramework.Runtime.GameEntry.GetComponent<ItemComponent>();
            Data = UnityGameFramework.Runtime.GameEntry.GetComponent<DataComponent>();
        }

        private void Update()
        {
            if (!GameInit)
            {
                Config.ParseData("UIConfig");
                Config.ParseData("SceneConfig");
                GameInit = true;
            }
        }
    }
}
