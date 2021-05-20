using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    PlayerInput playerInput;

    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float moveSpeed = 6;
    public Sound sound;

    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;

    public float wallSlideSpeedMax = 3;
    public float wallStickTime = .25f;
    float timeToWallUnstick;

    private float _prevY;
    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    Controller2D controller;

    Vector2 directionalInput;
    bool wallSliding;
    int wallDirX;

    public static int Gems = 0;
    public TextMeshProUGUI GemText;
    public int Health = 5;
    public GameObject[] hearts;

    void Start()
    {
        controller = GetComponent<Controller2D>();
        

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }

    void Update()
    {
        _prevY = transform.position.y;
        CalculateVelocity();
        HandleWallSliding();
        PlayerHealth();
        

        controller.Move(velocity * Time.deltaTime, directionalInput);

        if (controller.collisions.above || controller.collisions.below)
        {
            if (controller.collisions.slidingDownMaxSlope)
            {
                velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;
            }
            else
            {
                velocity.y = 0;
            }
        }

        GemText.text = "x" + Gems.ToString();
    }

    public void SetDirectionalInput(Vector2 input)
    {
        directionalInput = input;
    }

    public void OnJumpInputDown()
    {

        if (wallSliding)
        {
            if (wallDirX == directionalInput.x)
            {
                velocity.x = -wallDirX * wallJumpClimb.x;
                velocity.y = wallJumpClimb.y;
            }
            else if (directionalInput.x == 0)
            {
                velocity.x = -wallDirX * wallJumpOff.x;
                velocity.y = wallJumpOff.y;
            }
            else
            {
                velocity.x = -wallDirX * wallLeap.x;
                velocity.y = wallLeap.y;
            }
        }
        if (controller.collisions.below)
        {
            if (controller.collisions.slidingDownMaxSlope)
            {
                if (directionalInput.x != -Mathf.Sign(controller.collisions.slopeNormal.x))
                { // not jumping against max slope
                    velocity.y = maxJumpVelocity * controller.collisions.slopeNormal.y;
                    velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x;
                }
            }
            else
            {
                velocity.y = maxJumpVelocity;
            }
        }

    }

    public void OnJumpInputUp()
    {

        if (velocity.y > minJumpVelocity)
        {
            velocity.y = minJumpVelocity;
        }

    }


    void HandleWallSliding()
    {
        wallDirX = (controller.collisions.left) ? -1 : 1;
        wallSliding = false;
        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        {
            wallSliding = true;

            if (velocity.y < -wallSlideSpeedMax)
            {
                velocity.y = -wallSlideSpeedMax;
            }

            if (timeToWallUnstick > 0)
            {
                velocityXSmoothing = 0;
                velocity.x = 0;

                if (directionalInput.x != wallDirX && directionalInput.x != 0)
                {
                    timeToWallUnstick -= Time.deltaTime;
                }
                else
                {
                    timeToWallUnstick = wallStickTime;
                }
            }
            else
            {
                timeToWallUnstick = wallStickTime;
            }

        }

    }

    void CalculateVelocity()
    {
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Collectable")
        {
            Gems += 1;
            Destroy(col.gameObject);
            sound.GemNoise();
        }

        if (col.gameObject.tag == "Spikes")
        {
            Health -= 1;
            PlayerHealth();
            sound.HurtNoise();
        }

        if (col.gameObject.tag == "Enemy")
        {
            Health -= 1;
            PlayerHealth();
            sound.HurtNoise();
        }
    }

    public void PlayerHealth()
    {
        if (Health < 1)
        {
            Destroy(hearts[0].gameObject);
            Destroy(gameObject);
            
            SceneManager.LoadScene("Death");
            Health = 5;
            Gems = 0;
        }
        else if (Health < 2)
        {
            Destroy(hearts[1].gameObject);
            
        }
        else if (Health < 3)
        {
            Destroy(hearts[2].gameObject);
        }
        else if (Health < 4)
        {
            Destroy(hearts[3].gameObject);
            
        }
        else if (Health < 5)
        {
            Destroy(hearts[4].gameObject);
            
        }
    }

    public bool IsDescending()
    {
        return transform.position.y < _prevY;
        
    }
}
