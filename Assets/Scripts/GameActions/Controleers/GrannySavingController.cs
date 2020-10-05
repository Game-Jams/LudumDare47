using Observable;
using UnityEngine;
using UnityEngine.Playables;

namespace GameActions
{
    [RequireComponent(typeof(PlayableDirector))]
    public class GrannySavingController : MonoBehaviour, IGameActionInvokedListener, 
        IObserverNotify<IGameActionInvokedListener,GameActionParams>
    {
        private PlayableDirector _director;

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
            if (parameters.Action == GameAction.BanditKilled)
            {
                _director.Play();

                this.Unsubscribe<IGameActionInvokedListener, GameActionParams>();
            }
        }

        public void Win()
        {
            this.NotifyListeners(new GameActionParams(GameAction.SheriffSavesGranny));
        }
    }
}
