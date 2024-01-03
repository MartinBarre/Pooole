using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public bool destroyOnCollision;

    private void OnCollisionEnter2D(Collision2D other)
    {
        var playerController = other.transform.GetComponent<PlayerController>();

        if (playerController)
        {
            var contact = other.GetContact(0).point;

            if (!playerController.isInvincible)
            {
                playerController.Damage(contact);
            }

            if (destroyOnCollision)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var playerController = other.transform.GetComponent<PlayerController>();

        if (playerController)
        {
            if (!playerController.isInvincible)
            {
                playerController.Damage(transform.position);
            }

            if (destroyOnCollision)
            {
                Destroy(gameObject);
            }
        }
    }
}