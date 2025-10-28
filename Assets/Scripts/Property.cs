using System;

namespace Party
{
    [Serializable]
    public class Property
    {
        public string key;
    }

    [Serializable]
    public class FloatProperty:Property
    {
        public float value;
    }

    [Serializable]
    public class BoolProperty:Property
    {
        public bool value;
    }

    [Serializable]
    public class Vector3Property : Property
    {
        public UnityEngine.Vector3 value;
    }

    [Serializable]
    public class IntProperty : Property
    {
        public int value;
    }
}