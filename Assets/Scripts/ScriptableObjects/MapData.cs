using UnityEngine;

namespace ShrimpFPS.ScriptableObjects
{
    [CreateAssetMenu(fileName = "MapData", menuName = "MapData", order = 0)]
    public class MapData : ScriptableObject
    {
        public int SceneID;
        public GameObject offlinePlayer;
        public GameObject onlinePlayer;
    }
}