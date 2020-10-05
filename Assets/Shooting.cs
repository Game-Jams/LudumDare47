using GameActions;
using Observable;
using UnityEngine;
using UnityEngine.AI;
using PlayerController = Player.Control.PlayerController;

public class Shooting : MonoBehaviour, IObserverNotify<IGameActionInvokedListener, GameActionParams>
{
    [SerializeField] private PlayerController _controller;
    
    [SerializeField] private Animator _targetAnimator;
    
    [SerializeField] private ParticleSystem[] _systems;
    
    public void ShootTarget()
    {
        _targetAnimator.SetBool("Shooted", true);

        Transform target = _targetAnimator.gameObject.GetComponentInParent<NavMeshAgent>().transform;

        target.rotation = Quaternion.FromToRotation(target.position, transform.position);

        GetComponentInParent<NavMeshAgent>().transform.rotation = Quaternion.FromToRotation(transform.position, target.position);
        
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

    public void TriggerGrannySave()
    {
        this.NotifyListeners<IGameActionInvokedListener, GameActionParams>(new GameActionParams(GameAction.BanditKilled));  
    } 
}
