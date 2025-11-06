using GameFramework.Fsm;
using GameFramework.Procedure;
using Party.Base;
using UnityEngine;

namespace Party.Custom.Procedure
{
    public class EntranceProcedure:ProcedureBase
    {
        private bool _resource_init_finished=false;
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            Debug.Log("loading scene");
            if (GameEntry.Base.EditorResourceMode)
            {
                OnResourceInitFinished();
            }
            else
            {
                GameEntry.Resource.InitResources(OnResourceInitFinished);
            }
        }

        private void OnResourceInitFinished()
        {
            _resource_init_finished = true;
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (_resource_init_finished)
            {
                ChangeState<MainMenuProcedure>(procedureOwner);
            }
        }
    }
}