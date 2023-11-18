using UnityEngine;

namespace Game.Inventory
{
    public class InventorySlotDraggable : InventorySlot
    {
        public static InventorySlotDraggable Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            gameObject.SetActive(false);
        }

        private void Update()
        {
            transform.position = Input.mousePosition;
        }

        public override void Draw(EquipmentSO equip)
        {
            base.Draw(equip);
            gameObject.SetActive(true);
        }

        public override void Clear()
        {
            base.Clear();
            gameObject.SetActive(false);
        }
    }
}