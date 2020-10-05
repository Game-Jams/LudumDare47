using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    internal sealed class NpcMoveAction : NpcActionBehaviour
    {
        private const float StoppingDistance = 0.1f;

        private NavMeshAgent _navAgent;
        private Transform _target;
        private Animator _characterAnimator;

        private bool _isEnded;

        public NpcMoveAction(GameObject owner, Transform target) : base(owner)
        {
            _target = target;
        }

        public override void StartAction()
        {
            Initialize();

            _navAgent.destination = _target.position;
            _characterAnimator.SetBool("IsMoving", true);
        }

        public override void UpdateState()
        {
            if (!_navAgent.pathPending && _navAgent.remainingDistance <= StoppingDistance && !_isEnded)
            {
                EndAction();
                _isEnded = true;
                _characterAnimator.SetBool("IsMoving", false);
            }
        }

        private void Initialize()
        {
            _characterAnimator = _owner.transform.GetChild(0).GetComponent<Animator>();
            _navAgent = _owner.GetComponent<NavMeshAgent>();
        }
    }
}