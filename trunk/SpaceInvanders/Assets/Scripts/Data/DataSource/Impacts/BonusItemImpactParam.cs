﻿using Assets.Scripts.Data.Attributes;

namespace Assets.Scripts.Data.DataSource.Impacts
{
    public class BonusItemImpactData : BaseData
    {
        [DbField] public string itemId;
        [DbField] public bool toInventory;
    }
}