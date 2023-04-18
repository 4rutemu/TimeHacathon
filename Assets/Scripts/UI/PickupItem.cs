using System;
using System.Collections;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public Item item;
    public Inventory inventory;

    private bool canPickup = true;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = item.itemIcon;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(!canPickup) return;
        if(!col.gameObject.tag.Equals("Player")) return;
        if(inventory.AddItem(item) == false) return;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(!canPickup) return;
        if(!col.gameObject.tag.Equals("Player")) return;
        if(inventory.AddItem(item) == false) return;
        Destroy(gameObject);
    }

    public void disablePickuping(float seconds)
    {
        canPickup = false;
        StartCoroutine(DisablePickuping(seconds));
    }

    private IEnumerator DisablePickuping(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        canPickup = true;
    }
}
