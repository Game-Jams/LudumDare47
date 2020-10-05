using Observable;
using UnityEngine;
using UnityEngine.Playables;

namespace GameActions
{
    [RequireComponent(typeof(PlayableDirector))]
    public class WagonController : MonoBehaviour, IGameActionInvokedListener
    {
        private PlayableDirector _director;

        private void Awake()
        {
            _director = GetComponent<PlayableDirector>();

            this.Subscribe<IGameActionInvokedListener, GameActionParams>();
        }

        private void Destroy()
        {
            this.Unsubscribe<IGameActionInvokedListener, GameActionParams>();
        }

        void IObserver<IGameActionInvokedListener, GameActionParams>.Completed(GameActionParams parameters)
        {
            _director.Play();

            this.Unsubscribe<IGameActionInvokedListener, GameActionParams>();
        }
    }
}
