using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHurtBox : MonoBehaviour
{
    public Player player;


    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Enemy touched a Player. Killing Player.");

        if (collider.gameObject.tag == "Player")
        {
            player.Health -= 1;
            if (player.Health < 1)
            {
                Destroy(player.hearts[0].gameObject);
                Destroy(gameObject);
                SceneManager.LoadScene("MainMenu");
                player.Health = 5;
            }
            else if (player.Health < 2)
            {
                Destroy(player.hearts[1].gameObject);
            }
            else if (player.Health < 3)
            {
                Destroy(player.hearts[2].gameObject);
            }
            else if (player.Health < 4)
            {
                Destroy(player.hearts[3].gameObject);
            }
            else if (player.Health < 5)
            {
                Destroy(player.hearts[4].gameObject);
            }
        }
    }
}
