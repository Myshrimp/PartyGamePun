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
    }
}
