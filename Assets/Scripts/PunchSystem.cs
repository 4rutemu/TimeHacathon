using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchSystem : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Input.GetAxisRaw("Horizontal") == 0)
        {
            _animator.Play("Punch");
            TimeObject punchObject = canPunch();
            if (punchObject != null)
            {
                punchObject.DestroyTimeObject();
            }
        }
    }


    public TimeObject canPunch()
    {
        Vector2 position = _rigidbody.transform.position;
        float distance = 0.25f;
        
        
        RaycastHit2D[] left = Physics2D.RaycastAll(position, Vector2.left, distance);
        foreach (RaycastHit2D hit in left)
        {
            if (hit.collider != null && hit.collider.gameObject.tag != "Player")
            {
                if (hit.collider.gameObject.GetComponent<TimeObject>() != null) return hit.collider.gameObject.GetComponent<TimeObject>();
            }
        }
        RaycastHit2D[] right = Physics2D.RaycastAll(position, Vector2.right, distance);
        foreach (RaycastHit2D hit in right)
        {
            if (hit.collider != null && hit.collider.gameObject.tag != "Player")
            {
                if (hit.collider.gameObject.GetComponent<TimeObject>() != null) return hit.collider.gameObject.GetComponent<TimeObject>();
            }
        }

        return null;
        
    }
}
