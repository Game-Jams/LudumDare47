using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RandomOffsetAnimation : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Animator>().SetFloat("Offset", Random.Range(0f, 1f));
    }
}
