using System;
using UnityEngine;

namespace ShrimpFPS.Runtime
{
    public class Menu : MonoBehaviour
    {
        private GameObject ui;
        public string name;

        private void Awake()
        {
            ui = this.gameObject;
        }

        public void Open()
        {
            if (!ui)
            {
                ui = this.gameObject;
            }
            ui.SetActive(true);
        }

        public void Close()
        {
            if (!ui)
            {
                ui = this.gameObject;
            }
            ui.SetActive(false);
        }
    }
}