using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TimeObject : MonoBehaviour
{
    [HideInInspector] public List<Vector3> positions = new List<Vector3>();
    void Start()
    {
        RevertTime.revertedObjects.Add(this);
    }
    
    void FixedUpdate()
    {   
        if(RevertTime.isReverting) return;
        if(positions.Contains(transform.position)) return; //!!!
        if (positions.Count >= RevertTime.savedTransformsLimit)
        {
            positions.RemoveAt(99);
        }
        positions.Insert(0, transform.position);
    }
}
