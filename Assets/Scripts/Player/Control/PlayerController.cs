using Observable;
using UnityEngine;
using UnityEngine.AI;

#pragma warning disable CS0649
namespace Player.Control
{
    [RequireComponent(typeof(NavMeshAgent))]
    internal sealed class PlayerController : MonoBehaviour, ISessionStartedListener
    {
        private const string MoveBool = "IsMoving";
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";
        
        [SerializeField] private Animator _animator = null;
        [SerializeField] private float _animationStopDistance = .5f;
        [SerializeField] private float _distance = .6f;
        
        private Transform _transform;
        private NavMeshAgent _agent;
        
        private bool _isSessionStarted;
        private int _moveBool;
        private bool _isMoving;

        private float _horizontal;
        private float _vertical;

        private void Awake()
        {
            this.Subscribe<ISessionStartedListener>();
        }
        
        private void OnDestroy()
        {
            this.Unsubscribe<ISessionStartedListener>();
        }
        
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();

            _transform = transform;

            _moveBool = Animator.StringToHash(MoveBool);
        }

        private void Update()
        {
            if (!_isSessionStarted)
            {
                return;
            }
            
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

            if (_isMoving && _agent.remainingDistance < _animationStopDistance || _agent.isStopped)
            {
                _isMoving = false;
                _animator.SetBool(_moveBool, _isMoving);
            }
        }

        private void Move()
        {
            Vector3 direction = new Vector3(-_horizontal, 0f, -_vertical).normalized * _distance;

            _agent.SetDestination(_transform.position + direction);

            if (!_isMoving)
            {
                _isMoving = true;
                _animator.SetBool(_moveBool, _isMoving);
            }
        }

        void IObserver<ISessionStartedListener, EmptyParams>.Completed(EmptyParams parameters)
        {
            _isSessionStarted = true;
        }
    }
}