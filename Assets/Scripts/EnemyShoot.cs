using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bulletPrefabs;
    public GameObject bulletSpawn;
    public float bulletSpeed;
    public float shootCooldown;
    public List<float> shootsAngle;

    private AudioSource audioSource;
    private float shootTimer;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (shootTimer <= 0)
        {
            Shoot();
            shootTimer += shootCooldown;
        }

        shootTimer -= Time.deltaTime;
    }

    private void Shoot()
    {
        if (audioSource && shootsAngle.Count > 0)
        {
            audioSource.Play();
        }

        foreach (float direction in shootsAngle)
        {
            GameObject bullet = Instantiate(bulletPrefabs, bulletSpawn.transform.position, transform.rotation);
            bullet.transform.Rotate(Vector3.forward, direction, 0);
            bullet.GetComponent<Rigidbody2D>().velocity =
                (Quaternion.Euler(0, 0, direction) * transform.right).normalized * bulletSpeed;
        }
    }
}