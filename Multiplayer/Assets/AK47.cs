using UnityEngine;

public class AK47 : MonoBehaviour
{
    public float fireRate = 0.1f; // Time between shots in seconds
    public GameObject bulletPrefab; // Assign bullet prefab in the inspector
    public Transform firePoint; // Assign fire point in the inspector

    private float nextFireTime;

    void Update()
    {
        // Check if player wants to shoot
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            // Fire the gun
            Shoot();
            // Set the next allowed fire time based on fire rate
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // Instantiate a bullet prefab at the fire point's position and rotation
        GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(-firePoint.forward));

        // Get the Bullet component from the instantiated bullet GameObject
        Bullet bullet = bulletObject.GetComponent<Bullet>();

        if (bullet != null)
        {
            // Set the velocity of the bullet to shoot in the reverse direction (if using Rigidbody)
            Rigidbody bulletRigidbody = bulletObject.GetComponent<Rigidbody>();
            if (bulletRigidbody != null)
            {
                bulletRigidbody.velocity = -bulletObject.transform.forward * bullet.speed;
            }
        }
    }
}
