using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(Vector3.right * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigga entered");
        if (other.CompareTag("Character"))
        {
            Destroy(gameObject);
        }
    }
}
