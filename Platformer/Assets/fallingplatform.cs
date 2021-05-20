using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class fallingplatform : MonoBehaviour
{
    Rigidbody2D rb;
    public float fallTime = 0.5f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Invoke("DropPlatform", fallTime);
            Destroy(gameObject, 2f);
        }
    }

    void DropPlatform()
    {
        rb.isKinematic = false;
    }
}

    
