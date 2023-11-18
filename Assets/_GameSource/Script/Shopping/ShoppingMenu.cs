using System;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class ShoppingMenu : MonoBehaviour
    {
        [SerializeField] private GameDatabase _gameDatabase;

        [Space]
        [SerializeField] private ShoppingListItem _itemPrefab;
        [SerializeField] private Transform _itemContainer;

        private void Awake()
        {
            _itemContainer.DestroyChildren();
        }

        private void Start()
        {
            foreach (EquipmentSO equip in _gameDatabase.Equipment)
            {
                var shoppingListItem = Instantiate(_itemPrefab, _itemContainer);
                shoppingListItem.Draw(equip);
            }
        }
    }
}