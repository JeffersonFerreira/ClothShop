using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Inventory
{
    public class InventoryMenu : MonoBehaviour
    {
        [FormerlySerializedAs("_slotContainer")]
        [SerializeField] private Transform _inventoryContainer;
        [SerializeField] private Transform _equipmentContainer;

        private InventorySlot[] _slots;
        private Dictionary<EquipmentCategory, InventorySlotEquipment> _equipmentSlots;

        private PlayerInventory _playerInventory;
        private PlayerEquipment _playerEquipment;

        public bool IsOpen => gameObject.activeSelf;

        private void Awake()
        {
            _playerInventory = FindObjectOfType<PlayerInventory>();
            _playerEquipment = FindObjectOfType<PlayerEquipment>();

            _slots = _inventoryContainer.GetComponentsInChildren<InventorySlot>();
            _equipmentSlots = _equipmentContainer.GetComponentsInChildren<InventorySlotEquipment>().ToDictionary(map => map.Category);

            _playerInventory.OnRemoved += (i, equip) => _slots[i].Redraw();
            _playerInventory.OnInserted += (i, equip) => _slots[i].Redraw();

            _playerEquipment.OnEquip += equip => _equipmentSlots[equip.Category].Redraw();
            _playerEquipment.OnDequip += equip => _equipmentSlots[equip.Category].Redraw();

            for (int i = 0; i < _slots.Length; i++)
                _slots[i].SetIndex(i, _playerInventory);
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);

            var draggable = InventorySlotDraggable.Instance;

            if (draggable.Equip)
            {
                _playerInventory.TryAppend(draggable.Equip);
                draggable.Hide();
            }
        }

        public void ToggleOpen()
        {
            if (IsOpen)
                Close();
            else
                Open();
        }
    }
}