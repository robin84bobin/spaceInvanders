using Assets.Scripts.Data;
using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Events.CustomEvents;
using Assets.Scripts.ViewControllers;
using UnityEngine;
using Assets.Scripts.ModelComponents.Entities;

namespace Assets.Scripts.Factories.GameEntitiesFactories
{
    public class GameEntityBuilder
    {
        private static bool _enable = false;
        private static LevelController _parentLevel;

        public static GameObject Create( CreateParams params_)
        {
            if (!_enable) {
                return null;
            }

            //switch (params_.model.DataType) {
            switch (params_.data.Type) {
                case DataTypes.BULLET:
                    return Create<BulletController>( new BulletModel(params_.data as BulletData), params_).gameObject;
                case DataTypes.ENEMY:
                    EnemyData data = params_.data as EnemyData;
                    EnemyModel enemy = new EnemyModel(data);
                    enemy.InitMoveParams(10f, 1.0f);
                    return Create<EnemyController>( enemy, params_).gameObject;
                case DataTypes.HERO:
                    return Create<HeroController>( new HeroModel((params_.data as HeroData)), params_).gameObject;
                
                default:
                    return null;
            }           
        }

        private static T Create<T>(BaseEntityModel model_, CreateParams params_ = null)
            where T : IBaseEntityController
        {

            GameObject prefab = (GameObject) Resources.Load("Prefabs/GameEntities/" + model_.DataType);
            GameObject go = GameObject.Instantiate(prefab) as GameObject;
           
            //Apply params to transform
            Transform t = go.transform;
            if (params_ != null) {
                t.localRotation = Quaternion.Euler(params_.rotation);
                t.position = params_.position;
                //t.localScale = params_.scale;
            }
            else {
                t.localRotation = Quaternion.identity;
                t.position = Vector3.zero;
                //t.localScale = Vector3.one;
            }
            t.parent = _parentLevel.transform;
            go.SetActive(true);

            //Init model
            T actor = go.GetComponent<T>();
            actor.Init(model_);
            return actor;
        }


        public static void Enable(LevelController levelController_)
        {
            _parentLevel = levelController_;
            _enable = true;
        }

        public static void Disable()
        {
            _enable = false;
        }

    }


}