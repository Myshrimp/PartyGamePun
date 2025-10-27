using System.Collections.Generic;
using UnityEngine;
namespace Control
{
    public enum InputType
    {
        Move, Jump, Attack, MouseMove, Sprint, Prop
    }
    public class InputHandler
    {

        protected Dictionary<InputType, bool> _bool_input;
        protected Dictionary<InputType, Vector3> _vec3_input;
        private Vector3 _move = Vector3.zero;
        private Vector3 _last_mouse_pos = Vector3.zero;
        public InputHandler()
        {
            _bool_input = new Dictionary<InputType, bool>();
            _vec3_input = new Dictionary<InputType, Vector3>();
        }
        public void OnUpdate()
        {
            float move_x = Input.GetAxisRaw("Horizontal");
            float move_z = Input.GetAxisRaw("Vertical");
            _move.x = move_x;
            _move.z = move_z;
            _move.Normalize();
            _vec3_input[InputType.Move] = _move;

            Vector3 mouse_pos = Input.mousePosition;
            Vector3 mouse_move = mouse_pos - _last_mouse_pos;
            _last_mouse_pos = mouse_pos;
            _vec3_input[InputType.MouseMove] = mouse_move;

            _bool_input[InputType.Sprint] = Input.GetKey(KeyCode.LeftShift);
            _bool_input[InputType.Jump] = Input.GetKeyDown(KeyCode.Space);
            _bool_input[InputType.Attack] = Input.GetKeyDown(KeyCode.Mouse0);
            _bool_input[InputType.Prop] = Input.GetKeyDown(KeyCode.Mouse1);
        }

        public bool GetBool(InputType i_type)
        {
            if (!_bool_input.ContainsKey(i_type))
            {
                return false;
            }
            return _bool_input[i_type];
        }

        public Vector3 GetVec3(InputType i_type)
        {
            if (!_vec3_input.ContainsKey(i_type))
            {
                return Vector3.zero;
            }
            return _vec3_input[i_type];
        }
    }
}