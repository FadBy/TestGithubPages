using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public float speed = 5f;
    public float standingStraightDelay;
    public float flipDeadZone = 7f;
    public float deadJumpZone = 75f;
    public float deathFlySpeed;
    public float deathFlyDuration;
    public float deathFallAccelarationMultiplier = 2f;
    public float groundCircleRadius = 0.5f;

    public float jumpForce = 10f;
    public AudioSource runningSound;

    public UnityEvent onDie;

    private SpriteRenderer spriteRenderer;

    private float standStraightTimer;

    private bool isGrounded;
    private bool IsGrounded
    {
        get { return isGrounded; }
        set {
            isGrounded = value;
            anim.SetBool("isGrounded", value);
            
        }
    }
    private Animator anim;
    private Camera mainCamera;
    private Collider2D col;
    private Rigidbody2D rb;

    private IEnumerator standingCoroutine;

    public Transform PlayerCenter => spriteRenderer.transform;

    private bool canMove = true;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        mainCamera = Camera.main;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        //animator = GetComponent<Animator>();
        anim.SetBool("isDead", false);
    }


    void Update()
    {
        CheckIfGrounded();
        Bend(Time.deltaTime, Input.GetAxis("Horizontal"));
        Flip();
        if (Input.GetAxis("Horizontal") == 0f)
        {
            StandStraight(Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Flip()
    {
        if (transform.rotation.eulerAngles.z >= flipDeadZone || 360 - transform.rotation.eulerAngles.z <= flipDeadZone)
        {
            spriteRenderer.flipX = transform.rotation.eulerAngles.z < 180;
        }
    }

    private void StandStraight(float time)
    {
        if (!isGrounded)
            return;
        standStraightTimer -= time;
        if (standStraightTimer > 0f)
            return;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, rotationSpeed * time);
    }

    private void CheckIfGrounded()
    {
        var hits = Physics2D.CircleCastAll(new Vector2(transform.position.x, col.bounds.min.y), groundCircleRadius, Vector2.up);
        IsGrounded = hits.Any((h) => h.collider.gameObject.layer == LayerMask.NameToLayer("Ground"));
    }

    private void Bend(float time, float horizontalInput)
    {
        if (!canMove)
            return;
        if (horizontalInput == 0f)
            return;
        float z = transform.rotation.eulerAngles.z;
        if (z >= deadJumpZone && z <= 180 && horizontalInput < 0)
            return;
        if (360 - z >= deadJumpZone && z >= 180 && horizontalInput > 0)
            return;
        transform.Rotate(Vector3.back * horizontalInput * rotationSpeed * time);
        standStraightTimer = standingStraightDelay;
    }

    void Jump()
    {
        if (!canMove)
            return;
        Vector2 jumpVector = ConvertAngleToDirection(transform.rotation.eulerAngles.z);
        rb.velocity = jumpVector * jumpForce;
        anim.SetTrigger("Jump");
        AudioManager.instance.PlayFlapSound();

    }

    private static Vector2 ConvertAngleToDirection(float angle)
    {
        float angleInRadians = -angle * Mathf.Deg2Rad;
        float sinAngle = Mathf.Sin(angleInRadians);
        float cosAngle = Mathf.Cos(angleInRadians);
        return new Vector2(sinAngle, cosAngle).normalized;
    }

    public void Die()
    {
        Die(Vector2.up);
    }

    public void Die(Vector2 deathDirection)
    {
        anim.SetBool("isDead", true);
        canMove = false;
        AudioManager.instance.PlayDeathSound();
        onDie.Invoke();
        transform.DOMove((Vector2)PlayerCenter.position + deathFlySpeed * deathDirection, deathFlyDuration).OnComplete(() =>
        {
            col.enabled = false;
            rb.velocity = Vector2.zero; 
            rb.gravityScale *= deathFallAccelarationMultiplier;
        });
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioManager.instance.PlayWoodImpactSound();
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, GetComponent<Collider2D>().bounds.min.y), groundCircleRadius);
    }

    private IEnumerator StandStraightDelay()
    {

        yield return new WaitForSeconds(standingStraightDelay);

    }
}
