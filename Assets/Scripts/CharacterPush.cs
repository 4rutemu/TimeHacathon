using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPush : MonoBehaviour
{
    
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    public LayerMask layerMask;

    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        isPushing();
        
    }
    
    
    
    public void isPushing()
    {
        if (Input.GetAxisRaw("Horizontal") == 0f)
        {
            _animator.SetBool("isPushing", false);
            return;
        }
        
        Vector2 position = _rigidbody.transform.position;
        float distance = 0.1f;
    
        RaycastHit2D leftHit = Physics2D.Raycast(position, Vector2.left, distance, layerMask);
        if (leftHit.collider != null) {
            _animator.SetBool("isPushing", true);
            return;
        }
        
        RaycastHit2D rightHit = Physics2D.Raycast(position, Vector2.right, distance, layerMask);
        if (rightHit.collider != null) {
            _animator.SetBool("isPushing", true);
            return;
        }
        
        _animator.SetBool("isPushing", false);
    }
}
