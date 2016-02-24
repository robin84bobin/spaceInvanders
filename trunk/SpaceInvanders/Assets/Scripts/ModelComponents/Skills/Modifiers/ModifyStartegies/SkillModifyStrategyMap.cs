using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ModelComponents.Skills.Modifiers.ModifyStartegies
{

    public class SkillModifyStrategyMap
    {
        public static ISkillModifyStrategy Get(string name_)
        {
            if (!StrategyMap.ContainsKey(name_)) {
                Debug.LogError(string.Format("Couldn't get strategy by name \'{0}\'", name_));
                return null;
            }
            return StrategyMap[name_];
        }

        // ReSharper disable once InconsistentNaming
        private static readonly ParallelModifyStrategy _parallel = new ParallelModifyStrategy();
        public static ParallelModifyStrategy Parallel
        {
            get { return _parallel; }
        }

        // ReSharper disable once InconsistentNaming
        private static readonly SerialModifyStrategy _serial = new SerialModifyStrategy();
        public static SerialModifyStrategy Serial
        {
            get { return _serial; }
        }

        private static readonly Dictionary<string, ISkillModifyStrategy> StrategyMap = new Dictionary<string, ISkillModifyStrategy>() {
            {"serial", Serial },
            {"parallel", Parallel }
        };
    }

    public interface ISkillModifyStrategy
    {
        void Apply(double value_, string[] skills_, Dictionary<string, Skill> targetSkills_);
    }
}