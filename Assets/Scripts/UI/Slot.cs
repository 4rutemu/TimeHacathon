
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public GameObject selector;

    [HideInInspector] public Item _item;
    public Image itemSprite;

    private void Update()
    {
        itemSprite.enabled = _item != null;
        if (_item != null)
        {
            itemSprite.sprite = _item.itemIcon;
            itemSprite.color = _item.color;
        }
    }

    public void select()
    {
        selector.transform.position = gameObject.transform.position;
    }
}
