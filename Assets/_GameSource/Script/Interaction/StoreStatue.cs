using UnityEngine;

namespace Game.Interaction
{
    public class StoreStatue : MonoBehaviour, IInteractable
    {
        private ShoppingMenu _shoppingMenu;

        private void Awake()
        {
            _shoppingMenu = FindObjectOfType<ShoppingMenu>(true);
        }

        public void Interact() => _shoppingMenu.Show();
        public void EndInteract() => _shoppingMenu.Hide();
    }
}