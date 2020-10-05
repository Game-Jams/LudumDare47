using Interactions;
using Observable;
using UnityEngine;
using UnityEngine.Playables;

#pragma warning disable CS0649
namespace GameActions
{
    [RequireComponent(typeof(PlayableDirector))]
    public class GuardController : MonoBehaviour, IGameActionInvokedListener
    {
        [SerializeField] private SimpleInteraction _revolver;

        private PlayableDirector _director;

        private bool _wagonArrived;
        private bool _richGuySentToBarberShop;

        private void Awake()
        {
            _director = GetComponent<PlayableDirector>();

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
                case GameAction.RichGuySentToBarberShop:
                    _richGuySentToBarberShop = true;
                    break;
                case GameAction.AmmoWagonArrived:
                    _wagonArrived = true;
                    break;
                case GameAction.GuardDistracted when _wagonArrived && _richGuySentToBarberShop:
                    _director.Play();
                    _revolver.enabled = true;

                    this.Unsubscribe<IGameActionInvokedListener, GameActionParams>();
                    break;
            }
        }
    }
}
