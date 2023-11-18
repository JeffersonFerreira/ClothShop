using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Inventory
{
    public class InventorySlot : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] protected Image _icon;
        [SerializeField] private EquipmentCategory _categoryFilter;

        public event Action<EquipmentSO> OnItemTaken, OnItemDropped;

        public EquipmentSO Equip { get; protected set; }

        public virtual bool CanDraw(EquipmentSO equip)
        {
            return _categoryFilter == null || equip.Category == _categoryFilter;
        }

        public virtual void Draw(EquipmentSO equip)
        {
            Equip = equip;

            if (equip)
                _icon.overrideSprite = equip.EquipmentSprite;
        }

        public virtual void Clear()
        {
            Equip = null;
            _icon.overrideSprite = null;
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData _)
        {
            var draggable = InventorySlotDraggable.Instance;

            // If draggable active, try get his object
            if (draggable.gameObject.activeSelf)
            {
                if (!CanDraw(draggable.Equip))
                    return;

                // This slot isn't holding any equipment, let's get the one from drag
                if (!Equip)
                {
                    Draw(draggable.Equip);
                    OnItemDropped?.Invoke(draggable.Equip);

                    draggable.Clear();
                }
                // This slot is holding equipment, lets swap
                else
                {
                    var tempEquip = Equip;

                    Draw(draggable.Equip);
                    draggable.Draw(tempEquip);

                    OnItemTaken?.Invoke(tempEquip);
                    OnItemDropped?.Invoke(Equip);
                }
            }
            // If drag isn't active and we have an item, lets put it in the drag
            else if (Equip)
            {
                draggable.Draw(Equip);
                OnItemTaken?.Invoke(Equip);

                Clear();
            }
        }
    }
}