using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        StartCoroutine(destroy());
    }

    void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, Vector2.left, ref velocity, 1f);
    }

    IEnumerator destroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
