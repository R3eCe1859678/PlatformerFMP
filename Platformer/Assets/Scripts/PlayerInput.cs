using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{

    Player player;
    public Animator anim;
    public bool flipX;
    private SpriteRenderer mySpriteRenderer;

    void Start()
    {
        player = GetComponent<Player>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();


    }

    void Update()
    {
        Vector2 directionalInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxisRaw("Vertical"));
        

        player.SetDirectionalInput(directionalInput);

        if (directionalInput == new Vector2(0, 0))
        {
            anim.SetBool("IsMoving", false);
            
            //Debug.Log("IM NOT MOVING");

        }
        else 
        {
            anim.SetBool("IsMoving", true);
            
            //Debug.Log("IM MOVING");
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            // if the variable isn't empty we have a reference to our SpriteRenderer
            if (mySpriteRenderer != null)
            {
                // flip the sprite left
                mySpriteRenderer.flipX = true;
            }

        }
        //if (Input.GetKeyDown(KeyCode.D))
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            // flip the sprite right
            mySpriteRenderer.flipX = false;
        }



        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("IsJumping", true);
            anim.SetBool("IsFalling", false);
            player.OnJumpInputDown();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("IsFalling", true);
            anim.SetBool("IsMoving", false);
            anim.SetBool("IsJumping", false);
            player.OnJumpInputUp();
        }
    }
  }
