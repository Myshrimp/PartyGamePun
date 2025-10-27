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
        private static int SERIAL_ID=1;
        private IFsm<HorsemanEntity> _fsm;
        private Animator _animator;
        private InputHandler _input_handler;

        public StateConfig StateConfig
        {
            get { return _state_config; }
        }

        private void Awake()
        {
            _state_config.Init();
            var cof = _state_config;
            _input_handler = new InputHandler();
            _animator = GetComponent<Animator>();
            InitFsm();
        }

        private void Update()
        {
            _input_handler.OnUpdate();
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