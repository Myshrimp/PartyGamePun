using System;
using ShrimpFPS.Interfaces;
using UnityEngine;

namespace ShrimpFPS.Runtime
{
    public class DynamicLoader : MonoBehaviour,INetworkOwnership
    {
        [SerializeField] private float detectDistance = 100f;
        [SerializeField] private LayerMask detectLayerMask = default;
        private GameObject[] mapModules;
        private Collider[] colliders;
        private bool isOwner = true;

        private void Start()
        {
            colliders = new Collider[10000];
            Physics.OverlapSphereNonAlloc(transform.position, detectDistance,colliders, detectLayerMask);
            foreach (var col in colliders)
            {
                col.gameObject.SetActive(true);
            }
        }

        private void Update()
        {
            if (!isOwner) return;
            Debug.Log("col length:" + colliders.Length);
            foreach (var col in colliders)
            {
                Debug.Log("Disabling buildings");
                col.gameObject.SetActive(false);
            }
           Physics.OverlapSphereNonAlloc(transform.position, detectDistance, colliders,detectLayerMask);
            foreach (var col in colliders)
            {
                Debug.Log("enabling buildings");
                col.gameObject.SetActive(true);
            }
        }

        public void AbandonControll()
        {
            isOwner = false;
        }
    }
}