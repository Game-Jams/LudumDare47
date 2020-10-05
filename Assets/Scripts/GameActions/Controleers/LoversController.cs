using Observable;
using UnityEngine;
using UnityEngine.Playables;

namespace GameActions
{
    [RequireComponent(typeof(PlayableDirector))]
    public class LoversController : MonoBehaviour, IGameActionInvokedListener
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
            if (parameters.Action == GameAction.RichGuySentToSaloon)
            {
                _director.Play();

                this.Unsubscribe<IGameActionInvokedListener, GameActionParams>();
            }
        }
    }
}
