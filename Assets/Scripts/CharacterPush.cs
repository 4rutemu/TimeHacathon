using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

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
        bool isPull = isPulling();
        bool isPush = isPushing();
        
        if (isPull)
        {
            if(CharacterMovement.currentVelocity != Vector2.zero) _animator.Play("Pull");
            _animator.SetBool("isPulling", isPull);
        }
        else if (!isPull && isPush)
        {
            if(Input.GetAxisRaw("Horizontal") != 0) _animator.Play("Push");
            _animator.SetBool("isPushing", isPush);
        }
        else
        {
            _animator.SetBool("isPulling", false);
            _animator.SetBool("isPushing", false);
        }
    }
    
    
    
    public bool isPushing()
    {
        Vector2 position = _rigidbody.transform.position;
        float distance = 0.1f;
    
        RaycastHit2D hit = Physics2D.Raycast(position, CharacterMovement.side, distance, layerMask);
        if (hit.collider != null) {
            return true;
        }
        return false;
    }

    public bool isPulling()
    {
        if (!Input.GetMouseButton(1)) return false;
        
        Vector2 position = _rigidbody.transform.position;
        float distance = 0.1f;
        
        RaycastHit2D left = Physics2D.Raycast(position, Vector2.left, distance, layerMask);
        if (left.collider != null)
        {
            GameObject gameObject = left.collider.gameObject;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(CharacterMovement.currentVelocity.x * -0.3f,
                CharacterMovement.currentVelocity.y * -0.3f);
            return true;
        }
        RaycastHit2D right = Physics2D.Raycast(position, Vector2.right, distance, layerMask);
        if (right.collider != null)
        {
            GameObject gameObject = right.collider.gameObject;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(CharacterMovement.currentVelocity.x * -0.3f,
                CharacterMovement.currentVelocity.y * -0.3f);
            return true;
        }

        return false;
    }
}
