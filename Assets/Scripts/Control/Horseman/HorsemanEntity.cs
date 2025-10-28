using System;
using System.Collections.Generic;
using Control.States;
using GameFramework.Fsm;
using Party.Base;
using ScriptableObjects;
using UnityEngine;
namespace Control
{
    public class HorsemanEntity : EntityBase
    {
        [SerializeField] private StateConfig _state_config;
        [SerializeField] private CharacterConfig _character_config;
        [SerializeField] private CameraController _cam_controller;
        private CharacterMovement _move;
        private static int SERIAL_ID=1;
        private IFsm<HorsemanEntity> _fsm;
        private Animator _animator;
        private InputHandler _input_handler;
        private float _max_speed;
        private float _sprint_speed;
        public StateConfig StateConfig
        {
            get { return _state_config; }
        }

        private void Awake()
        {
            _state_config.Init();
            _character_config.Init();
            _input_handler = new InputHandler();
            _animator = GetComponent<Animator>();
            _move = GetComponent<CharacterMovement>();

            _max_speed = _character_config.float_map["MaxSpeed"];
            _sprint_speed = _character_config.float_map["SprintSpeed"];
            InitFsm();
        }

        private void Start()
        {
            _cam_controller.SwitchState(CameraController.CameraState.FollowPlayer);
        }

        private void Update()
        {
            _input_handler.OnUpdate();

            HandleMovement();
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _cam_controller.SwitchState(CameraController.CameraState.LockTarget);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                _cam_controller.SwitchState(CameraController.CameraState.Shake);
            }
        }

        private void HandleMovement()
        {
            float speed = _max_speed;
            if (_input_handler.GetBool(InputType.Sprint))
            {
                speed = _sprint_speed;
            }
            _move.Move(_input_handler.GetVec3(InputType.Move), speed);
            SetAnimState("Speed", _move.GetVelocity().magnitude /_sprint_speed);
        }

        private void InitFsm()
        {
            List<FsmState<HorsemanEntity>> state_list = new List<FsmState<HorsemanEntity>>();
            state_list.Add(new IdleState());
            state_list.Add(new AttackState());
            _fsm = GameEntry.Fsm.CreateFsm<HorsemanEntity>(SERIAL_ID.ToString(), this, state_list);
            _fsm.Start<IdleState>();
            SERIAL_ID++;
        }
        
        public void SetAnimState(string name, bool value)
        {
            _animator.SetBool(name, value);
        }

        public void SetAnimState(string name, float value)
        {
            _animator.SetFloat(name, value);
        }

        public void SetAnimTrigger(string trigger_name)
        {
            _animator.SetTrigger(trigger_name);
        }

        public void SetAnimLayerWeight(int layer, float weight)
        {
            weight = Mathf.Min(1, weight);
            _animator.SetLayerWeight(layer, weight);
        }
        public InputHandler GetInputHandler()
        {
            return _input_handler;
        }

        public float GetAnimProgress(int layer)
        {
            return _animator.GetCurrentAnimatorStateInfo(layer).normalizedTime;
        }
        
        public StateProperty GetStateProperty(string name)
        {
            return _state_config.GetStateProperty(name);
        }
    }
}