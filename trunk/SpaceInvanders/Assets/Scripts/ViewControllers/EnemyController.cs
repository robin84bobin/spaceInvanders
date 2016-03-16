using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Events.CustomEvents;
using Assets.Scripts.ModelComponents.Entities;
using UnityEngine;

namespace Assets.Scripts.ViewControllers
{
    public class EnemyController : BaseEntityController<EnemyModel>
    {
        #region implemented abstract members of BaseEntityController

        protected override void OnInit ()
        {
            model.MoveEvent += OnMove;
            model.DropBonusEvent += OnDropBonusEvent;
        }

        protected override void Release ()
        {
            model = null;
        }

        #endregion

        void OnDropBonusEvent(BonusData[] bonuses_)
        {
            foreach (BonusData bonusData in bonuses_) {
                for (int i = 0; i < bonusData.amount; i++) {
                    CreateParams createParams = new CreateParams
                    {
                        position = transform.position,
                        data = Main.Inst.Data.Get(bonusData.bonusEntityType, bonusData.bonusEntityId)
                    };
                    GameObjectsBuilder.GameObjectsBuilder.Create(createParams);
                }
            }
        }

        void OnMove (Vector3 moveVector_)
        {
            transform.Translate (moveVector_ * 3f);
        }

        public float GetYSize ()
        {
            return transform.localScale.y;
        }

    }
}
