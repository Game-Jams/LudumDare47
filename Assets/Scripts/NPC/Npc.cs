using UnityEngine;

#pragma warning disable CS0649
namespace NPC
{
    [RequireComponent(typeof(ActionSequence))]
    internal sealed class Npc : MonoBehaviour
    {
        [SerializeField] private NpcState _state;

        private NpcInteraction _npcInteraction;
        private ActionSequence _actionSequence;

        private void Awake()
        {
            _npcInteraction = GetComponent<NpcInteraction>();
            _actionSequence = GetComponent<ActionSequence>();

            _npcInteraction.IsDisabled = true;

            _actionSequence.Initialized += OnInitializeEnded;
            _actionSequence.Disabled += OnDisabled;
            _npcInteraction.InteractionEnded += OnInteractionEnded;
        }

        private void OnDestroy()
        {
            _actionSequence.Initialized -= OnInitializeEnded;
            _actionSequence.Disabled -= OnDisabled;
            _npcInteraction.InteractionEnded -= OnInteractionEnded;
        }

        private void OnInitializeEnded()
        {
            _state = NpcState.Idle;

            _npcInteraction.IsDisabled = false;
        }

        private void OnDisabled()
        {
            _npcInteraction.IsDisabled = true;

            Destroy(_npcInteraction);
        }

        private void OnInteractionEnded()
        {
            _state = NpcState.AlternativeAction;

            _npcInteraction.IsDisabled = true;
        }
    }
}