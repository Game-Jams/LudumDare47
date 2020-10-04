using UnityEngine;

public class HangTrigger : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void Hang()
    {
        _animator.SetTrigger(Animator.StringToHash("Hang"));
    }
}
