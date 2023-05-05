using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private HingeJoint2D firstBoneHinge;
    [SerializeField] private HingeJoint2D lastBoneHinge;
    [SerializeField] private DistanceJoint2D firstBoneDistance;
    [SerializeField] private DistanceJoint2D lastBoneDistance;

    [SerializeField] private GameObject lastBone;

    [SerializeField] private ItemPicker itemPicker;

    [SerializeField] private SpriteRenderer sprite;

    private void OnEnable()
    {
        sprite.enabled= false;
        itemPicker = FindAnyObjectByType<ItemPicker>();
        firstBoneDistance.connectedBody = itemPicker.handForRope;
        firstBoneHinge.connectedBody = itemPicker.handForRope;
        itemPicker.interractionWithRopeItem += OnInterrationWithRopeItem;
        Debug.Log("onenable");
        StartCoroutine(EnableSpriteCoroutine());
    }

    private void OnInterrationWithRopeItem(ItemForRope item)
    {
        //lastBone.transform.position = item.transform.position;

        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    Transform child = transform.GetChild(i);
        //    child.position= item.transform.position;
        //}

        itemPicker.itemForRope.GetComponent<HingeJoint2D>().connectedBody = lastBoneHinge.connectedBody;
        itemPicker.itemForRope.GetComponent<DistanceJoint2D>().connectedBody = lastBoneDistance.connectedBody;
        Debug.Log("Event");
    }

    private void OnDisable()
    {
        itemPicker.interractionWithRopeItem -= OnInterrationWithRopeItem;
    }

    IEnumerator EnableSpriteCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        sprite.enabled = true;
    }
}
