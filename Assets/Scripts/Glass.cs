using Unity.VisualScripting;
using UnityEngine;

public class Glass : MonoBehaviour
{
    [SerializeField] private float breakingForce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.TryGetComponent<PlayerController>(out var player))
        {
            if (collision.relativeVelocity.magnitude >= breakingForce) 
            {
                Break();
            }
        }
    }

    public void Break()
    {
        Destroy(gameObject);
    }

}
