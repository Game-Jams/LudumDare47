using GameActions;
using Interactions;
using UnityEngine;

#pragma warning disable CS0649
namespace NPC
{
    [CreateAssetMenu(fileName = "NPC", menuName = "GameDesign/NPC")]
    public class NpcInteractionData : ScriptableObject
    {
        [SerializeField] private GameAction _assignGameAction;
        [Space(10f)]
        [SerializeField] private InteractionData[] _interactions;

        public GameAction Action => _assignGameAction;
        public InteractionData[] Interactions => _interactions;
    }
}