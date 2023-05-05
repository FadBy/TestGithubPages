using System.Collections;
using UnityEngine;
[RequireComponent (typeof(Rigidbody2D))]
public class Throwable : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifetime;
    [SerializeField] private LayerMask destroyObjects;
    private Rigidbody2D rb2D;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void Throw(Vector2 direction)
    {
        rb2D.velocity = direction.normalized * speed;
        StartCoroutine(DestroyTimer());
    }

    private IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out var player))
        {
            player.Die((collision.gameObject.transform.position - transform.position).normalized);
            Destroy(gameObject);
        }
        if (((1 << collision.gameObject.layer) & destroyObjects.value) != 0)
        {
            Destroy(gameObject);
        }
    }


}
