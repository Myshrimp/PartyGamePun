using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace ShrimpFPS.Runtime
{
    public class NetworkPlayerBody : MonoBehaviour
    {
        [SerializeField] private PlayerTarget target;
        [HideInInspector]public PhotonView photonView;

        private void Start()
        {
            photonView = target.photonView;
        }

        public void OnTakeDamage(float value,Player dealer)
        {
            target.TakeDamage(value,dealer);
        }
    }
}