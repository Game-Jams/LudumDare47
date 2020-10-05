using Observable;
using UnityEngine;

namespace GameActions
{
    public class PlayerController : MonoBehaviour, IGameActionInvokedListener
    {
        private Animator _playerAnimator;

        private void Start()
        {
            _playerAnimator = GetComponent<Animator>();

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
                _playerAnimator.SetTrigger("Shoot");
            }
        }
    }
}