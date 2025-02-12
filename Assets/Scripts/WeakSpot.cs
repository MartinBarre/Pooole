using UnityEngine;
using Utils;

public class WeakSpot : MonoBehaviour
{
    public float jumpForce;
    public GameObject objetToDestroy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tag.CHICKEN))
        {
            if (!collision.GetComponent<PlayerController>().isInvincible)
            {
                collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpForce);
            }

            if (objetToDestroy != null)
            {
                Destroy(objetToDestroy);
            }
        }
    }
}