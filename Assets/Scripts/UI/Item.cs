using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject
{
    [Header("Item Info")]
    public Sprite itemIcon;
    public Color color;
}
