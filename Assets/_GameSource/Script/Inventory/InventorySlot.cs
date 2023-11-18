using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Inventory
{
    public class InventorySlot : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _icon;
        public event Action<EquipmentSO> OnItemTaken, OnItemDropped;

        public int Index { get; private set; }
        private EquipmentSO _currEquip;

        public void SetIndex(int i)
        {
            Index = i;
        }

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

        void IPointerClickHandler.OnPointerClick(PointerEventData _)
        {
            var draggable = InventorySlotDraggable.Instance;

            // If draggable active, try get his object
            if (draggable.gameObject.activeSelf)
            {
                // This slot aren't holding any equipment, let's get the one from drag
                if (!_currEquip)
                {
                    Draw(draggable.Equip);
                    OnItemDropped?.Invoke(draggable.Equip);

                    draggable.Clear();
                }
                // This slot is holding equipment, lets swap
                else
                {
                    var tempEquip = _currEquip;

                    Draw(draggable.Equip);
                    draggable.Draw(tempEquip);

                    OnItemTaken?.Invoke(tempEquip);
                    OnItemDropped?.Invoke(_currEquip);
                }
            }
            // If drag isn't active and we have an item, lets put it in the drag
            else if (_currEquip)
            {
                draggable.Draw(_currEquip);
                OnItemTaken?.Invoke(_currEquip);

                Clear();
            }
        }
    }
}