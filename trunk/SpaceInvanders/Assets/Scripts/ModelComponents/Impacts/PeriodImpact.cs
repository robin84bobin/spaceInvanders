using Assets.Scripts.Data;
using Assets.Scripts.Data.DataSource;
using Assets.Scripts.ModelComponents.Actors;
using Assets.Scripts.ModelComponents.Skills.Modifiers;

namespace Assets.Scripts.ModelComponents.Impacts
{
    public class PeriodImpact : Impact
    {
        private readonly Impact[] _impacts;
        private readonly TimerData _timerData;

        public PeriodImpact( TimerData timerData_, params Impact[] impacts_)
        {
            targetTypes = new string[2] { DataTypes.ENEMY, DataTypes.HERO };
            _impacts = impacts_;
            _timerData = timerData_;
        }

        protected override void Execute(BaseEntityModel entity_)
        {
            var timerImpactComponent = new PeriodImpactComponent(_impacts, _timerData);
            entity_.AddComponent(timerImpactComponent);
        }

    }
}