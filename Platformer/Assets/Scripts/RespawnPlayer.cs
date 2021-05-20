using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    public float threshold = -15f;

    public Player player;
    public Sound sound;

    void FixedUpdate()
    {
        if (transform.position.y < threshold)
        {
            transform.position = new Vector3(-15, 7, 0);
            player.Health -= 1;
            player.PlayerHealth();
            sound.HurtNoise();
        }
    }
}
