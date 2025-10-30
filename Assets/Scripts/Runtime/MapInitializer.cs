using Photon.Pun;
using ShrimpFPS.Helper;
using ShrimpFPS.ScriptableObjects;
using UnityEngine;

namespace ShrimpFPS.Runtime
{
    public abstract class MapInitializer : MonoBehaviourPunCallbacks
    {
        [SerializeField] protected GameObject playerPrefab;
        [SerializeField] protected MapData mapData;
        [SerializeField] protected Transform spawnPoints;
        
        protected GameObject currentLocalPlayer;
        public abstract void SpawnPlayer();
        public abstract void InitializePlayer();
        public abstract void InitMap();
    }
}