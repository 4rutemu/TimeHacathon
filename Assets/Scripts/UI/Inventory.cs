using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] public List<Slot> slots = new List<Slot>();
    public GameObject itemPrefab;
    
    public static int selectedItem = 0;
    void Start()
    {
        slots[0].select();
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0) {
            selectedItem +=  Mathf.FloorToInt(Input.GetAxis("Mouse ScrollWheel") * 10);
            
            if (selectedItem < 0) selectedItem = 9;
            else if (selectedItem > 9) selectedItem = 0;
            
            slots[selectedItem].select();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            RemoveItem();
        }
    }
    
    public bool AddItem(Item item)
    {
        if (slots.FindAll(slot => slot._item == null).Count == 0) return false;
        
        slots.Find(slot => slot._item == null)._item = item;
        return true;
    }

    public void RemoveItem()
    {
        GameObject pickupItem = Instantiate(itemPrefab, CharacterMovement.currentTransform.position, Quaternion.identity);
        PickupItem script = pickupItem.GetComponent<PickupItem>();
        
        script.disablePickuping(3);
        script.item = slots[selectedItem]._item;
        script.inventory = this;
        
        slots[selectedItem]._item = null;
    }
}
