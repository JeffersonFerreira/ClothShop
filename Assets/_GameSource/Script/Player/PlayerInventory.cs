using System.Collections.Generic;
using Game.Inventory;
using UnityEngine;

namespace Game
{
    public class PlayerInventory : MonoBehaviour
    {
        private const int TOTAL_SLOTS = 6 * 3;

        public IReadOnlyCollection<EquipmentSO> Slots => _slots;

        public bool IsOpen => _inventoryMenu.IsOpen;

        // Hashset to avoid duplicates
        private EquipmentSO[] _slots;

        private InventoryMenu _inventoryMenu;

        private void Awake()
        {
            _slots = new EquipmentSO[TOTAL_SLOTS];
            _inventoryMenu = FindObjectOfType<InventoryMenu>(true);
        }

        public void ToggleOpen()
        {
            if (_inventoryMenu.IsOpen)
                _inventoryMenu.Close();
            else
                _inventoryMenu.Open();
        }

        public bool TryAppend(EquipmentSO equip)
        {
            // Try to add element at the first empty slot
            for (var i = 0; i < _slots.Length; i++)
            {
                if (_slots[i] == null)
                {
                    _slots[i] = equip;
                    return true;
                }
            }

            return false;
        }

        public bool TryInsert(int index, EquipmentSO equip)
        {
            if (equip == null || index >= _slots.Length || _slots[index] != null)
                return false;

            _slots[index] = equip;

            return true;
        }

        public bool TryRemove(int index, out EquipmentSO equip)
        {
            if (index >= _slots.Length)
            {
                equip = null;
                return false;
            }

            equip = _slots[index];
            _slots[index] = null;
            return equip != null;
        }
    }
}