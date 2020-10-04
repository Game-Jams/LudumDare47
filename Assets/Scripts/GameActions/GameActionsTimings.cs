using UnityEngine;

namespace GameActions
{
    [CreateAssetMenu(fileName = "ActionsTimings", menuName = "GameDesign/ActionsTimings")]
    internal sealed class GameActionsTimings : ScriptableObject
    {
        [SerializeField] private ActionTiming[] _timings;

        public ActionTiming[] Timings => _timings;
    }
}