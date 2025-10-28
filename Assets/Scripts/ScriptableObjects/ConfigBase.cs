using UnityEngine;
using System.Collections.Generic;
using Party;
namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Config", menuName = "Config/Base", order = 0)]
    public class ConfigBase : ScriptableObject
    {
        public List<FloatProperty> float_properties = new();
        public List<Vector3Property> vector3_properties= new();
        public List<BoolProperty> bool_properties= new();
        public List<IntProperty> int_properties= new();

        public Dictionary<string, float> float_map= new();
        public Dictionary<string, bool> bool_map= new();
        public Dictionary<string, Vector3> vector3_map= new();
        public Dictionary<string, int> int_map= new(); 
        public void Init()
        {
            foreach (var v in float_properties )
            {
                float_map[v.key] = v.value;
            }
            foreach (var v in int_properties )
            {
                int_map[v.key] = v.value;
            }
            foreach (var v in bool_properties )
            {
                bool_map[v.key] = v.value;
            }
            foreach (var v in vector3_properties )
            {
                vector3_map[v.key] = v.value;
            }
        }
    }
}