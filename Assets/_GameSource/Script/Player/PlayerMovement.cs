using UnityEngine;

namespace Game
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 5;
        [SerializeField] private Animator _animator;

        private static readonly int animIsWalking = Animator.StringToHash("isWalking");

        private Vector3 _movementDir;

        public void Move(float h, float v)
        {
            _movementDir = new Vector3(h, v, 0).normalized;

            // Toggle walking animation if moving
            bool isMoving = _movementDir.sqrMagnitude > 0.01f;
            _animator.SetBool(animIsWalking, isMoving);

            // Flip character scale to make it face the same direction as the input
            if (Mathf.Abs(h) > 0.001f)
            {
                var scale = Vector3.one;
                scale.x *= Mathf.Sign(h);
                transform.localScale = scale;
            }
        }

        private void FixedUpdate()
        {
            transform.position += _movementDir * _moveSpeed * Time.deltaTime;
        }
    }
}