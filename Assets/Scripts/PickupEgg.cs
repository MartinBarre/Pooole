using UnityEngine;
using Utils;

public class PickupEgg : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tag.CHICKEN))
        {
            GameManager.Instance.PickEgg(gameObject);
        }
    }
}