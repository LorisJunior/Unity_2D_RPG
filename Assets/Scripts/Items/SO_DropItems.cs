using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_DropItems", menuName = "Scriptable Objects/Items/Drop Items")]
public class SO_DropItems : ScriptableObject
{
    public List<GameObject> dropItems;

    public GameObject GetDropItem(int i)
    {
        return dropItems[i];
    }
}