using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ShoppingMenu : MonoBehaviour
    {
        [SerializeField] private GameDatabase _gameDatabase;

        [Space]
        [SerializeField] private ShoppingListItem _itemPrefab;
        [SerializeField] private Transform _itemContainer;

        private PlayerInventory _playerInventory;

        private readonly Dictionary<EquipmentSO, ShoppingListItem> _displayedItems = new();

        private void Awake()
        {
            _itemContainer.DestroyChildren();
            _playerInventory = FindObjectOfType<PlayerInventory>();
        }

        private void Start()
        {
            foreach (EquipmentSO equip in _gameDatabase.Equipment)
            {
                ShoppingListItem item = Instantiate(_itemPrefab, _itemContainer);
                item.Draw(this, equip);

                _displayedItems[equip] = item;
            }
        }

        public void Purchase(EquipmentSO equip)
        {
            // Don't let player buy without sufficient inventory space or money
            if (!_playerInventory.HasSpace() || !_playerInventory.TrySpend(equip.Price))
                return;

            Destroy(_displayedItems[equip].gameObject);

            _playerInventory.TryAppend(equip);
            _displayedItems.Remove(equip);
        }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}