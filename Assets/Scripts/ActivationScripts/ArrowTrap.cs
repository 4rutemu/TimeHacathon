using System.Collections;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    public Trap trapScript;
    
    public GameObject projectilePrefab;

    public float cooldown;


    private void Start()
    {
        StartCoroutine(launch());
    }


    public IEnumerator launch()
    {
        yield return new WaitForSeconds(cooldown);

        GameObject arrow = Instantiate(projectilePrefab, gameObject.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(cooldown);
        if (trapScript.isActive) StartCoroutine(launch());
    }
    
}
