using Observable;
using Scripts.Services;
using UnityEngine;

#pragma warning disable CS0649
namespace GameActions
{
    internal sealed class GameActionsScheduler : MonoBehaviour, IObserverNotify<IGameActionInvokedListener, GameActionParams>
    {
        [SerializeField] private GameActionsTimings _timings;

        private void Start()
        {
            Timer timer = Timer.Instance;

            foreach (ActionTiming timing in _timings.Timings)
            {
                timer.RegisterActionAbsoluteTime(timing.TimeOfActivation, () =>
                {
                    this.NotifyListeners(new GameActionParams(timing.GameAction));
                    Debug.Log($"Action invoked: {timing.GameAction}");
                });
            }
        }
    }
}