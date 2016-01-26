using Assets.Scripts.Input;

namespace Assets.Scripts.ModelComponents.Level
{
    public class LevelStatePause  : ILevelState
    {
        LevelModel _ownerModel;
        public LevelStatePause (LevelModel ownermodel_)
        {
            _ownerModel = ownermodel_;
        }

        #region ILevelState implementation

        public void Update ()
        {
            //
        }

        public void OnEnterState ()
        {
            Main.Inst.input.SetState (InputMode.UI);
            _ownerModel.Pause (true);
        }

        public void OnExitState ()
        {
            //
        }

        public InputMode LevelInputMode {
            get {
                return InputMode.UI;
            }
        }

        #endregion
    }
}

