using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 2f;
    public float activationDelay = 0.2f; // Time before the bullet can be destroyed on collision

    private bool isActivated = false;

    private void Start()
    {
        // Destroy the bullet after a certain amount of time
        Destroy(gameObject, lifetime);
        // Start the activation delay coroutine
        StartCoroutine(ActivateAfterDelay());
    }

    void Update()
    {
        // Move the bullet forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the bullet is activated and the collided object has the "Player" tag
        if (isActivated && other.CompareTag("Player"))
        {
            // Destroy the bullet
            Destroy(gameObject);
        }
    }

    private IEnumerator ActivateAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(activationDelay);
        // Activate the bullet for collisions
        isActivated = true;
    }
}
