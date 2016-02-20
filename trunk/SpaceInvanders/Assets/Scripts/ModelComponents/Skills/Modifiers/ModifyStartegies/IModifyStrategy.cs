using System.Collections.Generic;

namespace Assets.Scripts.ModelComponents.Skills.Modifiers.ModifyStartegies
{
    public interface IModifyStrategy
    {
        void Apply(double value_, string[] skills_, Dictionary<string, Skill> targetSkills_);
    }


    public class ModifyStrategy
    {
        private static Dictionary<string, IModifyStrategy> _map = new Dictionary<string, IModifyStrategy>() {
            {"serial", new SerialModifyStrategy() },
            {"parallel", new ParallelModifyStrategy() }
        };

        public static IModifyStrategy Get(string name_)
        {
            return _map[name_];
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
    }
}