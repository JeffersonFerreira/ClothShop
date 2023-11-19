using System;
using Game.Inventory;
using UnityEngine;

namespace Game.Interaction
{
    public class InventoryBox : MonoBehaviour, IInteractable
    {
        private InventoryMenu _inventoryMenu;

        private void Awake()
        {
            _inventoryMenu = FindObjectOfType<InventoryMenu>(true);
        }

        public void Interact() => _inventoryMenu.Open();
        public void EndInteract() => _inventoryMenu.Close();
    }
}