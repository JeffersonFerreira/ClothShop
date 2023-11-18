using UnityEngine;
using UnityEngine.UI;

namespace Game.Inventory
{
    public class InventorySlotDraggable : MonoBehaviour
    {
        [SerializeField] private Image _icon;

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

        public void Draw(EquipmentSO equip)
        {
            gameObject.SetActive(true);

            if (equip)
            {
                Equip = equip;
                _icon.overrideSprite = equip.EquipmentSprite;
            }
        }

        public void Clear()
        {
            Equip = null;
            _icon.overrideSprite = null;
            gameObject.SetActive(false);
        }
    }
}