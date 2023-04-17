using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarDoor : MonoBehaviour
{
    private Vector3 startPosition;
    public float y = 0;
    private void Start()
    {
        startPosition = transform.position;
    }

    private Coroutine currentCoroutine;
    
    public void open()
    {
        if(currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(openE());
    }

    public void close()
    {
        if(currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(closeE());
    }

    private IEnumerator openE()
    {
        while (gameObject.transform.position.y > y)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.1f, gameObject.transform.position.z);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator closeE()
    {
        while (gameObject.transform.position.y < startPosition.y)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.1f, gameObject.transform.position.z);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
