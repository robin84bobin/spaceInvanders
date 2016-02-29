using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Data.DataSource.Impacts;
using UnityEngine;

namespace Assets.Scripts.Factories.DataFactories.JsonFactories
{
    class BulletJsonFactory : AbstractJsonFactory
    {
        public override IBaseData Create(string jsonString_)
        {
            //first override all simple type fields...
            BulletData data = DefaultJsonFactory.Create<BulletData>(jsonString_);
            //...and then more complex fields
            JSONObject jo = new JSONObject(jsonString_);
            //IMPACTS
            JSONObject arrayJo = jo["impact"];
            if (arrayJo != null){
                int cnt = arrayJo.list.Count;
                data.impactInfos = new ImpactInfo[cnt];
                for (int i = 0; i < cnt; i++) {
                    ImpactInfo impactInfo = new ImpactInfo();
                    JsonUtility.FromJsonOverwrite( arrayJo.list[i].ToString(), impactInfo );
                    data.impactInfos[i] = impactInfo;
                }
            }

            return data;
        }
    }
}
