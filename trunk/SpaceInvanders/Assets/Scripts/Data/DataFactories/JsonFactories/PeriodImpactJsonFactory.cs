using Assets.JSON;
using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Data.DataSource.Impacts;
using UnityEngine;

namespace Assets.Scripts.Factories.DataFactories.JsonFactories
{
    class PeriodImpactJsonFactory : AbstractJsonFactory
    {
        public override IBaseData Create(string jsonString_)
        {
            //first override all simple type fields...
            PeriodImpactData data = DefaultJsonFactory.Create<PeriodImpactData>(jsonString_);

            //...and then more complex fields
            JSONObject jo = new JSONObject(jsonString_);

            //IMPACTS
            JSONObject arrayJo = jo["impact"];
            if (arrayJo != null)
            {
                int cnt = arrayJo.list.Count;
                data.impactInfos = new ImpactInfo[cnt];
                for (int i = 0; i < cnt; i++) {
                    ImpactInfo info = //JsonFactory.Instance.Create<ImpactInfo>(arrayJo.list[i].ToString());
                        JsonUtility.FromJson<ImpactInfo>(arrayJo.list[i].ToString());
                    data.impactInfos[i] = info;
                }
            }
            //targetTypes
            data.targetTypes = GetStringArray(jo, "targetTypes");

            return data;
        }
    }
}