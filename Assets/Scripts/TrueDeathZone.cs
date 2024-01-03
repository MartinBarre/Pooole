using UnityEngine;
using Utils;

public class TrueDeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag(Tag.CHICKEN))
        {
            StartCoroutine(GameManager.Instance.LifeLost());
        }
    }
}