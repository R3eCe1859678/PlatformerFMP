using UnityEngine;

public class EnemyKill : MonoBehaviour
{
    private float _prevY;

    private void FixedUpdate()
    {
        _prevY = transform.position.y;
    }

    private bool IsDescending()
    {
        return transform.position.y < _prevY;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (IsDescending())
        {
            Debug.Log("Player stomped an Enemy. Destroying the Enemy.");
            Destroy(collider.gameObject);
        }
    }
}
