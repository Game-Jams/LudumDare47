using UnityEngine;

namespace NPC
{
    internal sealed class Npc : MonoBehaviour
    {
        [SerializeField] private NpcState _state;

        private NpcInteraction _npcInteraction;
        private ActionSequence _actionSequence;

        private void Start()
        {
            _npcInteraction = GetComponent<NpcInteraction>();
            _actionSequence = GetComponent<ActionSequence>();

            _npcInteraction.InteractionEnded += OnInteractionEnded;
        }

        private void OnDestroy()
        {
            _npcInteraction.InteractionEnded -= OnInteractionEnded;
        }

        private void OnInteractionEnded()
        {
            _state = NpcState.AlternativeAction;

            _npcInteraction.enabled = false;
            _actionSequence.InvokeAlternativeAction();
        }
    }
}