using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingLight : MonoBehaviour
{
    public Light myLight;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Blink());
    }

    // Update is called once per frame
    void Update()
    {
        
    }   
    IEnumerator Blink()
    {
        while (true)
        {
            myLight.enabled = false;
            yield return new WaitForSeconds(1);
            myLight.enabled = true;
            yield return new WaitForSeconds(1);
        }
    }
}
