using System.Collections;
using UnityEngine;

public class BoyController : MonoBehaviour
{
    [SerializeField] private Transform throwableSpawnPoint;
    [SerializeField] private GameObject throwablePrefab;
    [SerializeField] private float detectionRadius;
    [SerializeField] private float throwCooldown;

    private bool throwReady = true;

    private Transform target;
    [SerializeField] private float deadDetectZone;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        target = GameManager.Instance.PlayerController.PlayerCenter;
    }

    private void Update()
    {
        if (throwReady && DetectTarget())
        {
            Debug.Log("Detect");
            AttackTarget();
        }
    }

    private void AttackTarget()
    {
        var throwable = Instantiate(throwablePrefab, throwableSpawnPoint.position, Quaternion.identity).GetComponent<Throwable>();
        throwable.Throw((target.position - throwableSpawnPoint.position).normalized);
        StartCoroutine(throwCooldownActivate());
        if(target.position.x> throwableSpawnPoint.position.x)
        {
            spriteRenderer.flipX=false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    private bool DetectTarget()
    {
        if (Vector2.Distance(transform.position, target.position) > detectionRadius)
        {
            Debug.Log("Distance");
            return false;
        }
        //if (Physics2D.Raycast(transform.position, target.position - throwableSpawnPoint.position, detectionRadius, LayerMask.GetMask("Ground")))
        //{
        //    Debug.Log("Raycast");
        //    return false;
        //}
        Vector2 diff = target.position - transform.position;
        if (Mathf.Atan(Mathf.Abs(diff.x) / Mathf.Abs(diff.y)) * Mathf.Rad2Deg <= deadDetectZone)
        {
            Debug.Log("Atan");
            return false;
        }
        return true;
    }

    private IEnumerator throwCooldownActivate()
    {
        throwReady = false;
        yield return new WaitForSeconds(throwCooldown);
        throwReady = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
