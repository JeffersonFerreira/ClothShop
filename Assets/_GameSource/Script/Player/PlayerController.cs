using Game.Inventory;
using UnityEngine;

namespace Game
{
    public class PlayerController : MonoBehaviour
    {
        // TODO: Test only, remove later
        [SerializeField] private GameDatabase _gameDatabase;

        private PlayerInventory _inventory;
        private PlayerMovement _movement;

        private InventoryMenu _inventoryMenu;


        private void Awake()
        {
            _inventory = GetComponent<PlayerInventory>();
            _movement = GetComponent<PlayerMovement>();

            _inventoryMenu = FindObjectOfType<InventoryMenu>(true);
        }

        private void Start()
        {
            // TODO: Test only, remove later
            foreach (var equipmentSo in _gameDatabase.Equipment)
                _inventory.TryAppend(equipmentSo);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
                _inventoryMenu.ToggleOpen();

            if (!_inventoryMenu.IsOpen)
            {
                float h = Input.GetAxisRaw("Horizontal");
                float v = Input.GetAxisRaw("Vertical");

                _movement.Move(h, v);
            }
        }
    }
}