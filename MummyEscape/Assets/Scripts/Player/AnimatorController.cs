using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorController : MonoBehaviour
{
    private Animator animator;
  
    private const string IDLE = "Idle";
    private const string RUN = "Run";

    private void Awake() => animator = GetComponent<Animator>();

    public void PlayIdle() => animator.Play(IDLE);

    public void PlayRun() => animator.Play(RUN);
}
