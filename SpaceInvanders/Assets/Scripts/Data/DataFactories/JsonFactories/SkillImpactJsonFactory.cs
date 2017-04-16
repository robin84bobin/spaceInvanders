using Assets.JSON;
using Assets.Scripts.Data.DataSource;
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
            //skills
            data.skills = GetStringArray(jo, "skills");
            //strategy
            JSONObject strategy = jo["strategy"];
            if (strategy != null) {
                data.strategy = SkillModifyStrategy.Get(strategy.str);
            }
            //targetTypes
            data.targetTypes = GetStringArray(jo, "targetTypes");
            return data;
        }
    }
}
