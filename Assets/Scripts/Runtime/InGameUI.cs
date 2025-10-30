using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ShrimpFPS.Runtime
{
    public class InGameUI : MonoBehaviour
    {
        public bool inUI = true;

        private void OnEnable()
        {
            inUI = true;
        }

        private void OnDisable()
        {
            inUI = false;
        }

        public void Update()
        {
            if (inUI)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        public void LateUpdate()
        {
            if (inUI)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;   
            }
        }
    }
}