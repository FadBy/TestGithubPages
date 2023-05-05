using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(Collider2D))]
public class PressurePlate : Button
{
    [SerializeField] private Transform plateUpperLimit;
    [SerializeField] private Transform plateLowerLimit;
    [SerializeField] private float pressingSpeed;
    [SerializeField] private Transform bodyTransform;

    private Vector2 moveTo;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Vector3 pos = bodyTransform.position;
        float z = pos.z;
        pos = Vector2.MoveTowards(bodyTransform.position, moveTo, pressingSpeed * Time.deltaTime);
        pos.z = z;
        bodyTransform.position = pos;
    }

    private void Start()
    {
        moveTo = plateUpperLimit.position;
    }

    protected override bool IsActive()
    {
        return (Vector2)bodyTransform.position == (Vector2)plateLowerLimit.position;
    }

    protected override bool IsDeactive()
    {
        return (Vector2)bodyTransform.position == (Vector2)plateUpperLimit.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        moveTo = plateLowerLimit.position;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        moveTo = plateUpperLimit.position;
    }
}
