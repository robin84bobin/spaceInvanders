﻿using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Data.DataSource.Impacts.Damage;
using Assets.Scripts.ModelComponents.Skills.Modifiers.ModifyStartegies;

namespace Assets.Scripts.Factories.DataFactories.JsonFactories
{
    class SkillImpactJsonFactory : AbstractJsonFactory
    {
        public override IBaseData Create(string jsonString_)
        {
            //first override all simple type fields...
            SkillImpactData data = DefaultJsonFactory.Create<SkillImpactData>(jsonString_);

            //...and then more complex fields
            JSONObject jo = new JSONObject(jsonString_);
            data.skills = GetStringArray(jo, "skills");

            JSONObject strategy = jo["strategy"];
            if (strategy != null) {
                data.strategy = SkillModifyStrategyMap.Get(strategy.str);
            }
           
            return data;
        }
    }
}