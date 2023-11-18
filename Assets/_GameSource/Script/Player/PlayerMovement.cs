using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class PlayerMovement : MonoBehaviour, IDropHandler
    {
        [SerializeField] private float _moveSpeed = 5;

        private Vector3 _movementDir;

        public void Move(float h, float v)
        {
            _movementDir = new Vector3(h, v, 0);
        }

        private void FixedUpdate()
        {
            transform.position += _movementDir * _moveSpeed * Time.deltaTime;
        }

        public void OnDrop(PointerEventData eventData)
        {
            string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            Debug.Log(methodName);
        }
    }
}