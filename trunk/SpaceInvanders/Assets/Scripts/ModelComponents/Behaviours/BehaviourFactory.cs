using System;
using System.Collections.Generic;
using Assets.Scripts.Data.DataSource.Impacts;
using Assets.Scripts.ModelComponents.Impacts;
using UnityEngine;

namespace Assets.Scripts.ModelComponents.Behaviours
{
    public class BehaviourFactory
    {
        private static BehaviourFactory _inctance;
        public static BehaviourFactory Instance{
            get{
                if (_inctance == null) {
                    _inctance = new BehaviourFactory();
                }
                return _inctance;
            }
        }

        public IBaseComponent Create(BehaviourImpactData data_)
        {
            if (_factoryFuncs.ContainsKey(data_.behaviourType)) {
                return _factoryFuncs[data_.behaviourType].Invoke(data_);
            }
            else {
                Debug.LogError( GetType().Name + " -> can't create item of type :" + data_.behaviourType);
                return null;
            }
        }

        private readonly Dictionary<string, Func<BehaviourImpactData, IBaseComponent>> _factoryFuncs;
        private BehaviourFactory()
        {
            _factoryFuncs = new Dictionary<string, Func<BehaviourImpactData, IBaseComponent>>() {
                { "guided", CreateGuidedBehaviour }
            };
        }

        private IBaseComponent CreateGuidedBehaviour(BehaviourImpactData impactData_)
        {
            return new GuidedBehaviuorComponent();
        }

       
    }
}