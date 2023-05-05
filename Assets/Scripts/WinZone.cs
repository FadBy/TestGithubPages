using UnityEngine;

public class WinZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Letter>(out var letter))
        {
            GameManager.Instance.WinStart();
        }
    }
}
