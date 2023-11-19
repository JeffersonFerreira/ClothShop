using UnityEngine;
using UnityEngine.UI;

namespace Game.Inventory
{
    public class InventorySlotDraggable : MonoBehaviour
    {
        [SerializeField] protected Image _icon;
        public static InventorySlotDraggable Instance { get; private set; }

        public EquipmentSO Equip { get; private set; }

        private void Awake()
        {
            Instance = this;
            gameObject.SetActive(false);
        }

        private void Update()
        {
            transform.position = Input.mousePosition;
        }

        public void Show(EquipmentSO equip)
        {
            if (equip != null)
                _icon.overrideSprite = equip.EquipmentSprite;

            Equip = equip;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            Equip = null;
            _icon.overrideSprite = null;
            gameObject.SetActive(false);
        }
    }
}