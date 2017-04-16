using System.Collections.Generic;
using Assets.Scripts.Data;
using Assets.Scripts.Data.DataSource;
using Parse;
using UnityEngine;

namespace Assets.Scripts.Factories.DataFactories.ParseFactories
{
    public class ParseFactory
    {
        private static ParseFactory _instance;

        private readonly Dictionary<string, IConcreteParseFactory> _factories;

        private ParseFactory()
        {
            _factories = new Dictionary<string, IConcreteParseFactory> {
                {DataTypes.USER, new UserParseFactory()},
                {DataTypes.LEVEL, new LevelParseFactory()},
                {DataTypes.HERO, new HeroParseFactory()},
                {DataTypes.ENEMY, new EnemyParseFactory()},
                {DataTypes.BULLET, new BulletParseFactory()},
                {DataTypes.WEAPON, new WeaponParseFactory()}
            };
        }

        public static ParseFactory Instance
        {
            get { return _instance ?? (_instance = new ParseFactory()); }
        }

        public IBaseData Create(string dataType_, ParseObject po_)
        {
            if (_factories.ContainsKey(dataType_))
                return _factories[dataType_].Create(po_);
            Debug.LogError(string.Format("Can't find factory to create {0} from ParseObject:{1}",
                dataType_, po_.ClassName));
            return null;
        }
    }
}