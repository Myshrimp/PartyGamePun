using GameFramework.Fsm;
using Party.Base;

namespace Control.States
{
    public class IdleState:HorsemanStateBase
    {
        protected override void OnInit(IFsm<HorsemanEntity> fsm)
        {
            base.OnInit(fsm);
        }

        protected override void OnEnter(IFsm<HorsemanEntity> fsm)
        {
            base.OnEnter(fsm);
            fsm.Owner.SetAnimTrigger("IdleTrigger");
        }

        protected override void OnUpdate(IFsm<HorsemanEntity> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            
        }

        protected override void OnLeave(IFsm<HorsemanEntity> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
        }

        protected override void OnDestroy(IFsm<HorsemanEntity> fsm)
        {
            base.OnDestroy(fsm);
        }
    }
}