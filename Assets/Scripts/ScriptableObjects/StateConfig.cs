using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "StateConfig", menuName = "Config/StateConfig", order = 0)]
    public class StateConfig : ScriptableObject
    {
        public List<StateProperty> state_properties;
        private Dictionary<string, StateProperty> _state_property_map;
        public StateConfig()
        {
            state_properties = new List<StateProperty>();
            _state_property_map = new Dictionary<string, StateProperty>();
        }

        public void Init()
        {
            foreach (var v in state_properties)
            {
                _state_property_map[v.state_name] = v;
            }
        }
        public StateProperty GetStateProperty(string name)
        {
            return _state_property_map[name];
        }
    }

    [Serializable]
    public struct StateProperty
    {
        public string state_name;
        public float duration;
        public bool is_loop;
    }
}