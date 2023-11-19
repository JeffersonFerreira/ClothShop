using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Inventory
{
    public class InventorySlotEquipment : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _image;
        [SerializeField] private EquipmentCategory _category;

        public EquipmentCategory Category => _category;

        private PlayerEquipment _equipment;

        private void Awake()
        {
            _equipment = FindObjectOfType<PlayerEquipment>();
        }

        public void Redraw()
        {
            EquipmentSO equip = _equipment[_category];

            _image.overrideSprite = equip == null ? null : equip.EquipmentSprite;
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData _)
        {
            var draggable = InventorySlotDraggable.Instance;

            if (!draggable.gameObject.activeSelf)
            {
                // If drag isn't active and we have an item, lets begin dragging
                if (_equipment.TryDequip(_category, out EquipmentSO equip))
                    draggable.Show(equip);
            }

            // If dragging something
            else
            {
                // This slot is holding equipment, lets swap
                if (_equipment.TryDequip(_category, out EquipmentSO equip))
                {
                    _equipment.TryEquip(draggable.Equip);
                    draggable.Show(equip);
                }

                // This slot is free, let's get the one from drag
                else if (_equipment.TryEquip(draggable.Equip))
                {
                    draggable.Hide();
                }
            }
        }
    }
}