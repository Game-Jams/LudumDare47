using Observable;
using UnityEngine;

namespace GameActions
{
    [RequireComponent(typeof(Animator))]
    public class BanditController : MonoBehaviour, IGameActionInvokedListener
    {
        private Animator _bandit;

        private void Awake()
        {
            _bandit = GetComponent<Animator>();

            this.Subscribe<IGameActionInvokedListener, GameActionParams>();
        }

        private void OnDestroy()
        {
            this.Unsubscribe<IGameActionInvokedListener, GameActionParams>();
        }

        void IObserver<IGameActionInvokedListener, GameActionParams>.Completed(GameActionParams parameters)
        {
            if (parameters.Action == GameAction.BanditKilled)
            {
                _bandit.SetTrigger("");

                this.Unsubscribe<IGameActionInvokedListener, GameActionParams>();
            }
        }
    }
}
