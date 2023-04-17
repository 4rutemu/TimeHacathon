using System;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	public float moveSpeed = 5f;
	public float jumpForce = 20f;
	public float rollForce = 5f;


	private Rigidbody2D _rb;
	private Animator _animator;
	private SpriteRenderer _spriteRenderer;
	
	
	void Start()
	{
		_rb = gameObject.GetComponent<Rigidbody2D>();
		_animator = gameObject.GetComponent<Animator>();
		_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	}


	private float mx;
	private void Update()
	{
		mx = Input.GetAxisRaw("Horizontal");
		
		if (Input.GetButtonDown("Jump") && isGrounded())
		{
			Jump();
		}
		else if (isGrounded())
		{
			_animator.SetBool("isJumping", false);
		}
	}

	void FixedUpdate()
	{
		if (mx == 0f)
		{
			_animator.SetBool("isRunning", false);
			return;
		}
		
		Vector2 movement = new Vector2(mx * moveSpeed, _rb.velocity.y);

		_rb.velocity = movement;

		_spriteRenderer.flipX = (transform.right * mx).x < 0f;
		_animator.SetBool("isRunning", true);
	}

	void Jump()
	{
		Vector2 movement = new Vector2(_rb.velocity.x, jumpForce);

		_rb.velocity = movement;
		_animator.SetBool("isJumping", true);
	}
	
	public bool isGrounded()
	{
		Vector2 position = gameObject.transform.position;
		Vector2 direction = Vector2.down;
		float distance = 0.3f;
    
		RaycastHit2D hit = Physics2D.Raycast(position, direction, distance);
		if (hit.collider != null) {
			_animator.SetBool("isJumping", false);
			return true;
		}
    
		return false;
	}
}
