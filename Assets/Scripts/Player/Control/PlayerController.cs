using UnityEngine;
using UnityEngine.AI;

#pragma warning disable CS0649
namespace Player.Control
{
    [RequireComponent(typeof(NavMeshAgent))]
    internal sealed class PlayerController : MonoBehaviour
    {
        private const string MoveTrigger = "Move";
        private const string StopTrigger = "Stop";
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";
        
        [SerializeField] private Animator _animator = null;
        [SerializeField] private float _animationStopDistance = .5f;
        [SerializeField] private float _distance = .6f;
        
        private Transform _transform;
        private NavMeshAgent _agent;

        private int _moveHash;
        private int _stopHash;
        private bool _isMoving;

        private float _horizontal;
        private float _vertical;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();

            _transform = transform;

            _moveHash = Animator.StringToHash(MoveTrigger);
            _stopHash = Animator.StringToHash(StopTrigger);
        }

        private void Update()
        {
            _horizontal = Input.GetAxis(Horizontal);
            _vertical = Input.GetAxis(Vertical);
            
            if (Input.anyKey)
            {
                _horizontal = Input.GetAxis(Horizontal);
                _vertical = Input.GetAxis(Vertical);
                
                if (_horizontal != 0 || _vertical != 0)
                {
                    Move();
                }
            }

            if (_isMoving && _agent.remainingDistance < _animationStopDistance)
            {
                _animator.SetTrigger(_stopHash);
                _isMoving = false;
            }
        }

        private void Move()
        {
            Vector3 direction = new Vector3(-_horizontal, 0f, -_vertical).normalized * _distance;

            _agent.SetDestination(_transform.position + direction);

            if (!_isMoving)
            {
                _animator.SetTrigger(_moveHash);
                _isMoving = true;
            }
        }
    }
}