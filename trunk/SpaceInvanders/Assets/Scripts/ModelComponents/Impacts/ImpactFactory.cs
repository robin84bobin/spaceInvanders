using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Data.DataSource.Impacts;
using Assets.Scripts.Data.DataSource.Impacts.Damage;
using UnityEditor;

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

        private Dictionary<string, Func<ImpactInfo, IImpact>> _factoryFuncs;
        private ImpactFactory()
        {
            _factoryFuncs = new Dictionary<string, Func<ImpactInfo, IImpact>>() {
                {"damage", CreateDamageImpact }
            };
        }

        public IImpact Create(ImpactInfo info_)
        {
            if (_factoryFuncs.ContainsKey(info_.impactType)) {
                return _factoryFuncs[info_.impactType].Invoke(info_);
            }
            else {
                return null;
            }
        }

        private IImpact CreateDamageImpact(ImpactInfo info_)
        {
            DamageData data = Main.Inst.Data.Get<DamageData>(info_.impactObjectId);
            SkillImpact impact = new SkillImpact(data.value, data.skills);
            impact.modifyStrategy = data.strategy;
            return impact;
        }
    }
}
