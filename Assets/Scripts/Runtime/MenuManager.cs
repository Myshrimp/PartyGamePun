using System;
using System.Collections.Generic;
using UnityEngine;
using Menu = ShrimpFPS.Runtime.Menu;
namespace ShrimpFPS.Runtime
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;
        public List<Menu> register;
        public List<InputFieldView> inputFieldViewRegister;
        private Dictionary<string, Menu> menus;
        private Dictionary<string, InputFieldView> inputFieldViews;
        private void Awake()
        {
            Instance = this;
            menus = new Dictionary<string, Menu>();
            inputFieldViews = new Dictionary<string, InputFieldView>();
            RegisterMenus();
            RegisterInputFields();
        }

        public void RegisterMenu(Menu menu)
        {
            if (register == null) register = new List<Menu>();
            register.Add(menu);
        }

        public void RegisterInputField(InputFieldView view)
        {
            if (inputFieldViewRegister == null) inputFieldViewRegister = new List<InputFieldView>();
            inputFieldViewRegister.Add(view);
        }
        private void RegisterMenus()
        {
            for (int i = 0; i < register.Count; i++)
            {
                if (register[i].name.Length < 1)
                {
                    Debug.Log("Empty menu name!");
                    continue;
                }
                if (menus.ContainsKey(register[i].name))
                {
                    Debug.Log("Duplicate menu name! =>"+register[i].name);
                    continue;
                }
                menus[register[i].name] = register[i];
            }
        }

        private void RegisterInputFields()
        {
            for (int i = 0; i < inputFieldViewRegister.Count; i++)
            {
                if (inputFieldViewRegister[i].viewName.Length < 1)
                {
                    Debug.Log("Empty input field name!");
                    continue;
                }
                if (inputFieldViews.ContainsKey(inputFieldViewRegister[i].viewName))
                {
                    Debug.Log("Duplicate input field name! =>"+inputFieldViewRegister[i].viewName);
                    continue;
                }
                inputFieldViews[inputFieldViewRegister[i].viewName] = inputFieldViewRegister[i];
            }
        }
        public void OpenMenu(string name)
        {
            if (!menus.ContainsKey(name))
            {
                Debug.Log("[Opening menu]No such menu:"+name);
                return;
            }
            menus[name].Open();
        }

        public void CloseMenu(string name)
        {
            if (!menus.ContainsKey(name))
            {
                Debug.Log("[Closing menu]No such menu:"+name);
                return;
            }
            menus[name].Close();
        }

        public Menu GetMenu(string name)
        {
            if (!menus.ContainsKey(name))
            {
                Debug.Log("[Closing menu]No such menu:"+name);
                return null;
            }

            return menus[name];
        }

        public string GetInputFieldString(string viewName)
        {
            if (inputFieldViews.ContainsKey(viewName))
            {
                return inputFieldViews[viewName].inputField.text;
            }
            Debug.Log("No such input field:"+viewName);
            return string.Empty;
        }
    }
}