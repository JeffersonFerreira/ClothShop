using UnityEngine;

namespace Game
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float _smoothTime = 0.5f;
        [SerializeField] private float _maxSpeed = 25;
        [SerializeField] private float _lookAheadDist = 3;

        private Transform _playerTransform;
        private PlayerMovement _playerMovement;

        private Vector3 _offset;
        private Vector3 _currVelocity;

        private void Awake()
        {
            _playerMovement = FindObjectOfType<PlayerMovement>();
            _playerTransform = _playerMovement.transform;
        }

        private void Start()
        {
            _offset = transform.position - _playerTransform.position;
        }

        private void LateUpdate()
        {
            Vector3 lookAhead = _playerMovement.MovementDir * _lookAheadDist;
            Vector3 targetPos = _playerTransform.position + _offset + lookAhead;

            transform.position = Vector3.SmoothDamp(
                transform.position, targetPos,
                ref _currVelocity, _smoothTime, _maxSpeed
            );
        }
    }
}