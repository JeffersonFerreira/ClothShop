using UnityEngine;

namespace Game.Inventory
{
    public class InventoryMenu : MonoBehaviour
    {
        [Header("Inventory area")]
        [SerializeField] private Transform _slotContainer;

        private InventorySlot[] _slots;
        private PlayerInventory _playerInventory;
        public bool IsOpen => gameObject.activeSelf;

        private void Awake()
        {
            _playerInventory = FindObjectOfType<PlayerInventory>();
            _slots = _slotContainer.GetComponentsInChildren<InventorySlot>();

            _playerInventory.OnRemoved += (i, equip) => _slots[i].Redraw();
            _playerInventory.OnInserted += (i, equip) => _slots[i].Redraw();

            for (int i = 0; i < _slots.Length; i++)
            {
                _slots[i].SetIndex(i);
                _slots[i].Redraw();
            }
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