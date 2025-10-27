using GameFramework.Fsm;

namespace Control.States
{
    public class AttackState:HorsemanStateBase
    {
        protected override void OnInit(IFsm<HorsemanEntity> fsm)
        {
            base.OnInit(fsm);
        }

        protected override void OnEnter(IFsm<HorsemanEntity> fsm)
        {
            base.OnEnter(fsm);
            fsm.Owner.SetAnimState("IsAttack", true);
        }

        protected override void OnUpdate(IFsm<HorsemanEntity> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            if (_timer > GetStateProperty("AttackState").duration)
            {
                ChangeState<IdleState>(fsm);
            }
        }

        protected override void OnLeave(IFsm<HorsemanEntity> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            fsm.Owner.SetAnimState("IsAttack", false);
        }

        protected override void OnDestroy(IFsm<HorsemanEntity> fsm)
        {
            base.OnDestroy(fsm);
        }
    }
}