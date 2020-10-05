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
            switch (parameters.Action)
            {
                case GameAction.BanditShotSheriff:
                    _bandit.SetTrigger("Action");

                    this.Unsubscribe<IGameActionInvokedListener, GameActionParams>();
                    break;
                case GameAction.BanditKilled:
                    this.Unsubscribe<IGameActionInvokedListener, GameActionParams>();
                    break;
                case GameAction.BanditArrived:
                    _bandit.SetTrigger("Shoot");
                    break;
            }
        }
    }
}
