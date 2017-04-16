using System;
using System.Collections.Generic;
using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Data.DataSource.Impacts.Damage;
using UnityEngine;
using Assets.Scripts.Data.DataSource.Impacts;

namespace Assets.Scripts.Factories.DataFactories.JsonFactories
{
    public class JsonFactory
    {
        private static JsonFactory _instance;
        public static JsonFactory Instance
        {
            get { return _instance ?? (_instance = new JsonFactory()); }
        }

        private readonly Dictionary<Type, AbstractJsonFactory> _factories;

        private JsonFactory()
        {
            _factories = new Dictionary<Type, AbstractJsonFactory> {
                {typeof (BulletData), new BulletJsonFactory()},
                {typeof (SkillImpactData), new SkillImpactJsonFactory()},
                {typeof (PeriodImpactData), new PeriodImpactJsonFactory()},
                {typeof (BehaviourImpactData), new BehaviourImpactJsonFactory()},
                {typeof (HeroData), new HeroJsonFactory()},
                {typeof (EnemyData), new EnemyJsonFactory()},
            };

        }

        public T Create<T>( string jsonString_) where T : IBaseData, new()
        {
            if (_factories.ContainsKey(typeof(T))) {
                return (T)_factories[typeof(T)].Create(jsonString_) ;
            }
            else {
                return DefaultJsonFactory.Create<T>( jsonString_);
            }
        }
    }

    internal static class DefaultJsonFactory
    {
        public static T Create<T>( string jsonString_) where T : IBaseData, new () 
        {
           T data = new T();
           JsonUtility.FromJsonOverwrite(jsonString_, data);
           return data;
        }
    }
}
