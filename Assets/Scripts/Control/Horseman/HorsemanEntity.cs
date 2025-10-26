using System.Collections.Generic;
using Control.States;
using GameFramework.Fsm;
using Party.Base;
using UnityEngine;
namespace Control
{
    public class HorsemanEntity : EntityBase
    {
        private static int SERIAL_ID=1;
        private IFsm<HorsemanEntity> _fsm;
        private Animator _animator;
        private InputHandler _input_handler;

        private void Awake()
        {
            _input_handler = new InputHandler();
            _animator = GetComponent<Animator>();
            InitFsm();
        }

        private void InitFsm()
        {
            List<FsmState<HorsemanEntity>> state_list = new List<FsmState<HorsemanEntity>>();
            state_list.Add(new IdleState());
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
    }
}