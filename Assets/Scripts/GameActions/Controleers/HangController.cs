using Observable;
using UnityEngine;
using UnityEngine.Playables;

namespace GameActions
{
    [RequireComponent(typeof(PlayableDirector))]
    public class HangController : MonoBehaviour, IGameActionInvokedListener
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
            switch (parameters.Action)
            {
                case GameAction.SheriffSavesGranny:
                    this.Unsubscribe<IGameActionInvokedListener, GameActionParams>();
                    break;
                case GameAction.GrannyHanged:
                    _director.Play();

                    this.Unsubscribe<IGameActionInvokedListener, GameActionParams>();
                    break;
            }
        }
    }
}
