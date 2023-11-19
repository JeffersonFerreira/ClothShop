using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Inventory
{
    public class InventorySlot : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] protected Image _icon;

        public int Index { get; private set; }
        private PlayerInventory _inventory;

        private void Awake()
        {
            _inventory = FindObjectOfType<PlayerInventory>();
        }

        public void SetIndex(int index)
        {
            Index = index;
        }

        public void Redraw()
        {
            var equip = _inventory[Index];
            _icon.overrideSprite = equip == null ? null : equip.EquipmentSprite;
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData _)
        {
            var draggable = InventorySlotDraggable.Instance;

            if (!draggable.gameObject.activeSelf)
            {
                // If drag isn't active and we have an item, lets begin dragging
                if (_inventory.TryRemove(Index, out EquipmentSO equip))
                    draggable.Show(equip);
            }

            // If dragging something
            else
            {
                // This slot is holding equipment, lets swap
                if (_inventory.TryRemove(Index, out EquipmentSO inventoryEquip))
                {
                    _inventory.TryInsert(Index, draggable.Equip);
                    draggable.Show(inventoryEquip);
                }

                // This slot is free, let's get the one from drag
                else if (_inventory.TryInsert(Index, draggable.Equip))
                {
                    draggable.Hide();
                }
            }
        }
    }
}