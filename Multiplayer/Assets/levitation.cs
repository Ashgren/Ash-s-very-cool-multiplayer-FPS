using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levitation : MonoBehaviour
{
    // Start is called before the first frame update
    public float amplitude = 1.5f;
    public float speed = 1.0f;
    public float initialY;
    void Start()
    {
        initialY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float newY = initialY + amplitude * Mathf.Sin(speed * Time.time);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
