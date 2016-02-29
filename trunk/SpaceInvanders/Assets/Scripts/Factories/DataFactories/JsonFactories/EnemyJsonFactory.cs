using Assets.Scripts.Data.DataSource;
using UnityEngine;

namespace Assets.Scripts.Factories.DataFactories.JsonFactories
{
    internal class EnemyJsonFactory : AbstractJsonFactory
    {
        public override IBaseData Create(string jsonString_)
        {
            //first override all simple type fields...
            EnemyData data = DefaultJsonFactory.Create<EnemyData>(jsonString_);
            //...and then more complex fields
            JSONObject jo = new JSONObject(jsonString_);
            //SKILLS
            JSONObject arrayJo = jo["skills"];
            if (arrayJo != null)
            {
                int cnt = arrayJo.list.Count;
                data.skillInfos = new SkillInfo[cnt];
                for (int i = 0; i < cnt; i++)
                {
                    SkillInfo skillInfo = new SkillInfo();
                    JsonUtility.FromJsonOverwrite(arrayJo.list[i].ToString(), skillInfo);
                    data.skillInfos[i] = skillInfo;
                }
            }
            return data;
        }
    }
}