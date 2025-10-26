using System.Collections.Generic;
using UnityEngine;
namespace Control
{
    public enum InputType
    {
        Move, Jump, Attack, MouseMove, Sprint, 
    }
    public class InputHandler
    {

        protected Dictionary<InputType, bool> _bool_input;
        protected Dictionary<InputType, Vector3> _vec3_input;

        public InputHandler()
        {
            _bool_input = new Dictionary<InputType, bool>();
            _vec3_input = new Dictionary<InputType, Vector3>();
        }
        public void OnUpdate()
        {
            
        }

        public bool GetBool(InputType i_type)
        {
            return _bool_input[i_type];
        }

        public Vector3 GetVec3(InputType i_type)
        {
            return _vec3_input[i_type];
        }
    }
}