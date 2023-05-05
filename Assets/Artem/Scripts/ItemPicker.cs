using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    // Start is called before the first frame update

    public event Action<ItemForRope> interractionWithRopeItem;

    [SerializeField] private Rope rope;

    [SerializeField] public Rigidbody2D handForRope;
    public ItemForRope itemForRope;

    [SerializeField] private GameObject ropePrefab;

    private bool itemPicked=false;

    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (itemPicked)
            {
                if (rope.gameObject.activeSelf == true)
                {
                    rope.gameObject.SetActive(false);
                    itemPicked = false;
                }
            }
            else
            {
                if (itemForRope != null)
                {
                    rope.transform.position = itemForRope.transform.position;
                    rope.gameObject.SetActive(true);
                    interractionWithRopeItem?.Invoke(itemForRope);
                    itemPicked = true;
                }
            }
        }
       


        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ItemForRope>() is ItemForRope)
        {

            Debug.Log("collider is item");
            itemForRope = collision.gameObject.GetComponent<ItemForRope>();

            //«ƒ≈—‹ ÃŒ∆ÕŒ ¬€ƒ≈À»“‹ ITEM FOR ROPE
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ItemForRope>() is ItemForRope)
        {

            itemForRope = null;

            //«ƒ≈—‹ Õ”∆ÕŒ Œ“¬€ƒ≈À»“‹ ITEM FOR ROPE
        }
    }




}
