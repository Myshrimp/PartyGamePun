using UnityEngine;

namespace ShrimpFPS.Helper
{
    public static class MapHelper
    {
        public static Transform GetRandomSpawnPoint(Transform trans)
        {
            int id = Random.Range(0, trans.childCount);
            return trans.GetChild(id);
        }
    }
}