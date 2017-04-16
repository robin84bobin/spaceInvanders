using Assets.JSON;
using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Data.DataSource.Impacts;

namespace Assets.Scripts.Factories.DataFactories.JsonFactories
{
     class BehaviourImpactJsonFactory : AbstractJsonFactory
    {
         public override IBaseData Create(string jsonString_)
         {
            //first override all simple type fields...
            BehaviourImpactData data = DefaultJsonFactory.Create<BehaviourImpactData>(jsonString_);
            //...and then more complex fields
            JSONObject jo = new JSONObject(jsonString_);
            //targetTypes
            data.targetTypes = GetStringArray(jo, "targetTypes");
            return data;
        }
    }
}