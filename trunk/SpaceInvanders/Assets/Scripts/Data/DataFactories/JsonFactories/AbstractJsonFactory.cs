using Assets.JSON;
using Assets.Scripts.Data.DataSource;

namespace Assets.Scripts.Factories.DataFactories.JsonFactories
{
    public abstract class AbstractJsonFactory
    {
        public abstract IBaseData Create(string jsonString_);

        protected int GetInt(JSONObject jo_, string fieldName_)
        {
            int value = 0;
            jo_.GetField(ref value, fieldName_);
            return value;
        }

        protected float GetFloat(JSONObject jo_, string fieldName_)
        {
            float value = 0f;
            jo_.GetField(ref value, fieldName_);
            return value;
        }

        protected bool GetBool(JSONObject jo_, string fieldName_)
        {
            bool value = false;
            jo_.GetField(ref value, fieldName_);
            return value;
        }

        protected string GetString(JSONObject jo_, string fieldName_)
        {
            string value = string.Empty;
            jo_.GetField(ref value, fieldName_);
            return value;
        }

        protected string[] GetStringArray(JSONObject jo_, string fieldName_)
        {
            JSONObject arrayJo = jo_[ fieldName_ ];
            if (arrayJo != null)
            {
                int cnt = arrayJo.list.Count;
                var array = new string[cnt];
                for (int i = 0; i < cnt; i++)
                {
                    array[i] = arrayJo.list[i].str;
                }
                return array;
            }
            return null;
        }

        protected int[] GetIntArray(JSONObject jo_, string fieldName_)
        {
            JSONObject arrayJo = jo_[fieldName_];
            if (arrayJo != null)
            {
                int cnt = arrayJo.list.Count;
                var array = new int[cnt];
                for (int i = 0; i < cnt; i++)
                {
                    int val = (int)arrayJo.list[i].i;
                    array[i] = val;
                }
                return array;
            }

            return null;
        }

    }
}