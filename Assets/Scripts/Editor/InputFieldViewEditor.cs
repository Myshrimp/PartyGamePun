#if UNITY_EDITOR
using System;
using ShrimpFPS.Runtime;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine;
namespace ShrimpFPS.Editor
{
    [CustomEditor(typeof(InputFieldView))]
    public class InputFieldViewEditor : UnityEditor.Editor
    {
        private InputFieldView view;
        public override VisualElement CreateInspectorGUI()
        {
            
            return base.CreateInspectorGUI();
        }

        private void OnEnable()
        {
            view = (InputFieldView)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (EditorGUILayout.LinkButton("Register InputField"))
            {
                MenuManager menuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();
                menuManager.RegisterInputField(view);
                EditorUtility.SetDirty(menuManager);
                Debug.Log("Successfully registered input field");
            }
        }
    }
}

#endif