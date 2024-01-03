using UnityEngine;
using Utils;

public class PickupFeather : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tag.CHICKEN))
        {
            GameManager.Instance.PickFeather(gameObject);
        }
    }
}