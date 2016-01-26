using Assets.Scripts.Input;

namespace Assets.Scripts.ModelComponents.Level
{
    public class LevelStatePlay  : ILevelState
    {
        readonly LevelModel _ownerModel;

        public LevelStatePlay (LevelModel ownerModel_)
        {
            _ownerModel = ownerModel_;
        }

        #region ILevelState implementation

        public void Update ()
        {
            _ownerModel.UpdateGamePlay ();
        }

        public void OnEnterState ()
        {
            Main.Inst.input.SetState (LevelInputMode);
        }

        public void OnExitState ()
        {
            //
        }

        public InputMode LevelInputMode {
            get {
                return InputMode.GAME;
            }
        }
        #endregion


    }
}

