#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Reflection;
using ShrimpFPS.Runtime;
using Menu = ShrimpFPS.Runtime.Menu;

namespace ShrimpFPS.Editor
{
    [CustomEditor(typeof(Menu))]
    public class MenuEditor : UnityEditor.Editor
    {
        private Menu menu;

        private void OnEnable()
        {
            menu = (Menu)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (EditorGUILayout.LinkButton("Register Menu"))
            {
                MenuManager menuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();
                menuManager.RegisterMenu(menu);
                EditorUtility.SetDirty(menuManager);
                Debug.Log("Successfully registered menu");
            }
        }
    }
}

#endif