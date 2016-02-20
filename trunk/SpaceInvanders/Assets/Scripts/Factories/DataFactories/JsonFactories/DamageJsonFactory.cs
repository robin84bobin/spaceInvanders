using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Data.DataSource.Impacts.Damage;
using Assets.Scripts.ModelComponents.Skills.Modifiers.ModifyStartegies;

namespace Assets.Scripts.Factories.DataFactories.JsonFactories
{
    public class DamageJsonFactory : AbstractJsonFactory
    {
        public override IBaseData Create(string jsonString_)
        {
            //first override all simple type fields...
            DamageData data = DefaultJsonFactory.Create<DamageData>(jsonString_);

            //...and then more complex fields
            JSONObject jo = new JSONObject(jsonString_);
            data.skills = GetStringArray(jo, "skills");
            data.strategy = ModifyStrategy.Serial; //ModifyStrategy.Get( jo["strategy"].str);
            return data;
        }
    }
}
