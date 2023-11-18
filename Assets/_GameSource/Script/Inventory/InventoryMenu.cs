using System;
using UnityEngine;

namespace Game.Inventory
{
    public class InventoryMenu : MonoBehaviour
    {
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

        private void Awake()
        {
            _slotContainer.GetComponentInChildren<InventorySlot>();
        }

        // todo: Add player equipment
        public void Show(PlayerInventory playerInventory)
        {

        }
    }
}