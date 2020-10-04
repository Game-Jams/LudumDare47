using Interactions;
using UnityEngine;

#pragma warning disable CS0649
namespace NPC
{
    [CreateAssetMenu(fileName = "NPC", menuName = "GameDesign/NPC")]
    public class NpcInteractionData : ScriptableObject
    {
        [SerializeField] private InteractionData[] _interactions;

        public InteractionData[] Interactions => _interactions;
    }
}