using UnityEngine;

public class PlayEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _systems;
    
    public void PlayAllEffects()
    {
        foreach (ParticleSystem system in _systems)
        {
            system.Play();
        }
    }
}
