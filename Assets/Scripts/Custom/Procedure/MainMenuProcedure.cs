using GameFramework.Fsm;
using GameFramework.Procedure;
using Party.Base;

namespace Party.Custom.Procedure
{
    public class MainMenuProcedure:ProcedureBase
    {
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameEntry.Scene.LoadScene(GameEntry.Config.GetString("StartMenuScene"));
        }
    }
}