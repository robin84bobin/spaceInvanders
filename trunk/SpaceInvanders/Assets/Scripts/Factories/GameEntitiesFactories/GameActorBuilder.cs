using System;
using System.Collections.Generic;
using Assets.Scripts.Data;
using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Events.CustomEvents;
using Assets.Scripts.ModelComponents.Actors;
using Assets.Scripts.ViewControllers;
using UnityEngine;
using Assets.Scripts.Events;

namespace Assets.Scripts.Factories.GameEntitiesFactories
{
    public class GameActorBuilder
    {
        private static bool _enable = false;
        private static LevelController _parentLevel;

        public static GameObject CreateActor( CreateObjectParams params_)
        {
            if (!_enable) {
                return null;
            }

            switch (params_.model.DataType) {
                case DataTypes.BULLET:
                    return CreateActor<BulletController>( params_.model, params_).gameObject;
                case DataTypes.ENEMY:
                    return CreateActor<EnemyController>(params_.model, params_).gameObject;
                case DataTypes.HERO:
                    return CreateActor<HeroController>(params_.model, params_).gameObject;
                default:
                    return null;
            }           
        }

        private static T CreateActor<T>(BaseEntityModel model_, CreateObjectParams params_ = null)
            where T : IBaseActorController
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