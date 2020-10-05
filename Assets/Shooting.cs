using Player.Control;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private PlayerController _controller;
    
    [SerializeField] private Animator _targetAnimator;
    
    [SerializeField] private ParticleSystem[] _systems;
    
    public void ShootTarget()
    {
        _targetAnimator.SetBool("Shooted", true);

        _targetAnimator.transform.rotation =
            Quaternion.LookRotation(transform.position - _targetAnimator.transform.position, Vector3.up);
        
        foreach (ParticleSystem system in _systems)
        {
            system.Play();
        }
    }

    public void StopMoving()
    {
        _controller.enabled = false;
    }
    
    public void ContinueMoving()
    {
        _controller.enabled = true; 
    }
}
