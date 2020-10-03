using UnityEngine;
using UnityEngine.AI;

#pragma warning disable CS0649
namespace Player.Control
{
    [RequireComponent(typeof(NavMeshAgent))]
    internal sealed class PlayerController : MonoBehaviour
    {
        private Transform _transform;
        private NavMeshAgent _agent;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();

            _transform = transform;
        }

        private void Update()
        {
            if (Input.anyKey)
            {
                Move();
            }
        }

        private void Move()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(-horizontal, 0f, -vertical).normalized;

            _agent.SetDestination(_transform.position + direction);
        }
    }
}