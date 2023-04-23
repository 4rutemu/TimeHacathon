using System.Collections;
using UnityEngine;

public class PillarDoor : MonoBehaviour
{
    private Vector3 startPosition;
    public bool up;
    public float y = 0;
    public float x = 0;
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
        if (!up)
        {
            while (gameObject.transform.position.y > y)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                    gameObject.transform.position.y - 0.1f, gameObject.transform.position.z);
                yield return new WaitForSeconds(0.1f);
            }
            while (gameObject.transform.position.x > x)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x - 0.1f,
                    gameObject.transform.position.y, gameObject.transform.position.z);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            while (gameObject.transform.position.y < y)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                    gameObject.transform.position.y + 0.1f, gameObject.transform.position.z);
                yield return new WaitForSeconds(0.1f);
            }
            while (gameObject.transform.position.x < x)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + 0.1f,
                    gameObject.transform.position.y, gameObject.transform.position.z);
                yield return new WaitForSeconds(0.1f);
            }
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
