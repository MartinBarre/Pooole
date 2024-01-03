using System;
using UnityEngine;
using Utils;

public class CheckpointReach : MonoBehaviour
{
    public static event Action<Vector3> OnCheckpointReach;

    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip reachClip;

    private bool _triggered;

    private void Start()
    {
        animator.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_triggered && collision.CompareTag(Tag.CHICKEN))
        {
            OnCheckpointReach?.Invoke(transform.position);
            AudioManager.Instance.PlaySound(reachClip, 0.4f);
            animator.enabled = true;
            _triggered = true;
        }
    }
}