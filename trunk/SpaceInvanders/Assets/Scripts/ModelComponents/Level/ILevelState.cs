using System;
using Assets.Scripts.CommonComponents.StateSwitcher;
using Assets.Scripts.Input;

namespace Assets.Scripts.ModelComponents.Level
{
    public enum LevelStates
    {
        PLAY,
        PAUSE
    }

    public interface ILevelState : IBaseState
    {
        void Update();
        InputMode LevelInputMode { get; }
    }
}