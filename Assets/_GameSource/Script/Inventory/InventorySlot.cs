using UnityEngine;
using UnityEngine.UI;

namespace Game.Inventory
{
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        private EquipmentSO _currEquip;

        public void Draw(EquipmentSO equip)
        {
            _currEquip = equip;
            _icon.overrideSprite = equip.EquipmentSprite;
        }

        public void Clear()
        {
            _currEquip = null;
            _icon.overrideSprite = null;
        }
    }
}