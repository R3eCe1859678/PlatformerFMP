using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Transform castPoint;


    public Transform player;

    
    public float agroRange = 5;

    
    public float moveSpeed = 2;
    
    Rigidbody2D rb2d;

    bool isAgro = false;
    private bool isSearching;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CanSeePlayer(agroRange))
        {
            isAgro = true;
            
        }
        else
        {
            if (isAgro)
            {
                if (!isSearching)
                {
                    isSearching = true;
                    Invoke("StopChasingPlayer", 5);
                }
                
            }
            
        }
        if (isAgro)
        {
            ChasePlayer();
        }
    }

    private void StopChasingPlayer()
    {
        isAgro = false;
        isSearching = false;
        rb2d.velocity = new Vector2(0, 0);
    }

    private void ChasePlayer()
    {
        if(transform.position.x < player.position.x)
        {
            rb2d.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            rb2d.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
        }
    }

    bool CanSeePlayer(float distance)
    {
        bool val = false;
        var castDist = distance;

        Vector2 endPos = castPoint.position + Vector3.right * distance;
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Player"));
        

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }
            else
            {
                val = false;
            }
            
        }

        return val;

    } 
}
