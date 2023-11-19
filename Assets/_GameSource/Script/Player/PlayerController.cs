using Game.Interaction;
using UnityEngine;

namespace Game
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerInventory _inventory;
        private PlayerMovement _movement;

        private IInteractable _nearbyInteraction;
        private IInteractable _activeInteraction;

        private void Awake()
        {
            _inventory = GetComponent<PlayerInventory>();
            _movement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
                ActionInteraction();

            if (_activeInteraction == null)
            {
                float h = Input.GetAxisRaw("Horizontal");
                float v = Input.GetAxisRaw("Vertical");

                _movement.Move(h, v);
            }
        }

        private void ActionInteraction()
        {
            if (_nearbyInteraction != null && _nearbyInteraction != _activeInteraction)
            {
                if (_activeInteraction != null)
                    _activeInteraction.EndInteract();

                _activeInteraction = _nearbyInteraction;
                _activeInteraction.Interact();
            }
            else if (_activeInteraction != null)
            {
                _activeInteraction.EndInteract();
                _activeInteraction = null;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out IInteractable interactable))
                _nearbyInteraction = interactable;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out IInteractable _))
                _nearbyInteraction = null;
        }
    }
}