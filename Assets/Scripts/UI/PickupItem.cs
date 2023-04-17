using System;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public Item item;
    public Inventory inventory;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = item.itemIcon;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(!col.gameObject.tag.Equals("Player")) return;
        if(inventory.AddItem(item) == false) return;
        Destroy(gameObject);
    }
}
