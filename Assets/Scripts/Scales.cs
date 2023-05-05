//using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Events;

public class Scales : Button
{
    [SerializeField] private Transform lever;
    [SerializeField] private float leverTolerance;

    protected override bool IsActive()
    {
        return Mathf.Abs(lever.rotation.eulerAngles.z) <= leverTolerance;
    }

    protected override bool IsDeactive()
    {
        return !IsActive();
    }
}
