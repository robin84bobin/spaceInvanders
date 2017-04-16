namespace Assets.Scripts.ModelComponents.Equipments
{
    public interface IEquipmentModel
    {
        string PrefabName { get; }
        string Type { get; }
        void OnEquip();
        void OnUnequip();
    }
}