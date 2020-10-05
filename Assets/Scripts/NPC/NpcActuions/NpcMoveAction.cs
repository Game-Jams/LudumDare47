using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    internal sealed class NpcMoveAction : NpcActionBehaviour
    {
        private const float StoppingDistance = 0.1f;

        private NavMeshAgent _navAgent;
        private Transform _target;

        public NpcMoveAction(GameObject owner, Transform target) : base(owner)
        {
            _target = target;
        }

        public override void StartAction()
        {
            Initialize();

            _navAgent.destination = _target.position;
        }

        public override void UpdateState()
        {
            if (!_navAgent.pathPending && _navAgent.remainingDistance <= StoppingDistance)
            {
                EndAction();
            }
        }

        private void Initialize()
        {
            _navAgent = _owner.GetComponent<NavMeshAgent>();
        }
    }
}