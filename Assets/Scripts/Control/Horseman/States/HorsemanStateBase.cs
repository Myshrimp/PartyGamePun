using GameFramework.Fsm;

namespace Control.States
{
    public class HorsemanStateBase:FsmState<HorsemanEntity>
    {
        protected InputHandler _input;
    }
}