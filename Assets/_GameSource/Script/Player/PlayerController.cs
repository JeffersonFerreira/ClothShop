using UnityEngine;

namespace Game
{
    public class PlayerController : MonoBehaviour
    {
        // TODO: Test only, remove later
        [SerializeField] private GameDatabase _gameDatabase;

        public PlayerInventory Inventory { get; private set; }
        private PlayerMovement _movement;

        private void Awake()
        {
            Inventory = GetComponent<PlayerInventory>();
            _movement = GetComponent<PlayerMovement>();
        }

        private void Start()
        {
            // TODO: Test only, remove later
            foreach (var equipmentSo in _gameDatabase.Equipment)
                Inventory.TryAppend(equipmentSo);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
                Inventory.ToggleOpen();

            if (!Inventory.IsOpen)
            {
                float h = Input.GetAxisRaw("Horizontal");
                float v = Input.GetAxisRaw("Vertical");

                _movement.Move(h, v);
            }
        }
    }
}