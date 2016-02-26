using System;
using System.Collections.Generic;
using Assets.Scripts.Data;
using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Data.DataSource.Impacts;
using UnityEngine;
using SkillImpactData = Assets.Scripts.Data.DataSource.Impacts.Damage.SkillImpactData;

namespace Assets.Scripts.ModelComponents.Impacts
{
    public class ImpactFactory
    {
        private static ImpactFactory _instance;
        public static ImpactFactory Instance{
            get{
                if (_instance == null) {
                    _instance = new ImpactFactory();
                }
                return _instance;
            }
        }

        private readonly Dictionary<string, Func<ImpactInfo, IImpact>> _factoryFuncs;
        private ImpactFactory()
        {
            _factoryFuncs = new Dictionary<string, Func<ImpactInfo, IImpact>>() {
                {DataTypes.SKILL_IMPACT, CreateSkillImpact },
                {DataTypes.TIMER_IMPACT, CreateTimerImpact },
                {DataTypes.BEHAVIOUR_IMPACT, CreateBehaviourImpact }
            };
        }

        public IImpact Create(ImpactInfo info_)
        {
            if (_factoryFuncs.ContainsKey(info_.impactType)) {
                return _factoryFuncs[info_.impactType].Invoke(info_);
            }
            else {
                Debug.LogError(this.GetType().Name +": Can't create impact of type:" + info_.impactType);
                return null;
            }
        }

        private IImpact CreateSkillImpact(ImpactInfo info_)
        {
            SkillImpactData data = Main.Inst.Data.Get<SkillImpactData>(info_.impactObjectId);
            SkillImpact impact = new SkillImpact(data.value, data.skills) {
                Strategy = data.strategy,
            };
            return impact;
        }

        private IImpact CreateTimerImpact(ImpactInfo info_)
        {
            TimerImpactData timerImpactData = Main.Inst.Data.Get<TimerImpactData>(info_.impactObjectId);
            if (timerImpactData.impactInfos == null || timerImpactData.impactInfos.Length <= 0) {
                Debug.LogError(string.Format("{0} (id:{1}) has no impacts to apply ", timerImpactData.GetType().Name, timerImpactData.ObjectId));
                return null;
            }
            //create impacts
            int cnt = timerImpactData.impactInfos.Length;
            IImpact[] impacts = new IImpact[cnt];
            for (int i = 0; i < cnt; i++) {
                ImpactInfo info = timerImpactData.impactInfos[i];
                IImpact impact = Create(info);
                impacts[i] = impact;
            }
            //create timer
            TimerData timerData = Main.Inst.Data.Get<TimerData>(timerImpactData.timerId);
            TimerImpact timerImpact = new TimerImpact(timerData, impacts);
            return timerImpact;
        }

        private IImpact CreateBehaviourImpact(ImpactInfo info_)
        {
            BehaviourImpactData data = Main.Inst.Data.Get<BehaviourImpactData>(info_.impactObjectId);
            BehaviourImpact behaviourImpact = new BehaviourImpact(data);
            return behaviourImpact;
        }

    }
}
