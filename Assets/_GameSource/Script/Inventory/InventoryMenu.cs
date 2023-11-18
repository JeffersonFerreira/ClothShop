using UnityEngine;

namespace Game.Inventory
{
    public class InventoryMenu : MonoBehaviour
    {
        [Space]
        [SerializeField] private InventorySlot _draggableSlot;

        [Header("Equipment area")]
        [SerializeField] private InventorySlot _equipAreaHead;

        [Header("Equipment area/Hand")]
        [SerializeField] private InventorySlot _equipAreaLeftHand;
        [SerializeField] private InventorySlot _equipAreaRightHand;

        [Header("Equipment area/Foot")]
        [SerializeField] private InventorySlot _equipAreaLeftFoot;
        [SerializeField] private InventorySlot _equipAreaRightFoot;

        [Header("Inventory area")]
        [SerializeField] private Transform _slotContainer;

        private InventorySlot[] _slots;
        private PlayerInventory _playerInventory;
        public bool IsOpen => gameObject.activeSelf;

        private void Awake()
        {
            _playerInventory = FindObjectOfType<PlayerInventory>();
            _slots = _slotContainer.GetComponentsInChildren<InventorySlot>();

            for (var i = 0; i < _slots.Length; i++)
            {
                _slots[i].SetIndex(i);

                int capturedIndex = i;
                _slots[i].OnItemTaken += equip => Internal_OnItemTaken(capturedIndex, equip);
                _slots[i].OnItemDropped += equip => Internal_OnItemDropped(capturedIndex, equip);
            }
        }

        private void Internal_OnItemDropped(int i, EquipmentSO equip) => _playerInventory.TryInsert(i, equip);
        private void Internal_OnItemTaken(int i, EquipmentSO equip) => _playerInventory.TryRemove(i, out _);

        public void Open()
        {
            gameObject.SetActive(true);

            // Player slots has the same count as Inventory Menu
            int index = -1;
            foreach (EquipmentSO equip in _playerInventory.Slots)
            {
                index++;
                if (equip == null)
                    _slots[index].Clear();
                else
                    _slots[index].Draw(equip);
            }
        }

        public void Close()
        {
            gameObject.SetActive(false);

            var draggable = InventorySlotDraggable.Instance;

            if (draggable.Equip)
            {
                _playerInventory.TryAppend(draggable.Equip);
                draggable.Clear();
            }
        }
    }
}