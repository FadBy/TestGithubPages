using UnityEngine;

public class LevelBound : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out var player))
        {
            player.Die(Vector2.up);
        }
    }
}
