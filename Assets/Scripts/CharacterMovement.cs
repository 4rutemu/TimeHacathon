using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	public static Transform currentTransform;
	public static Vector2 side;
	public static Vector2 currentVelocity;
	public static GameObject playerObject;

	public static bool canMove = true;
	public static bool canCrouch = true;
	
	public float moveSpeed = 5f;
	public float jumpForce = 20f;
	public float rollForce = 5f;
	public LayerMask groundLayerMask;


	private Rigidbody2D _rb;
	private Animator _animator;
	private SpriteRenderer _spriteRenderer;
	private CapsuleCollider2D _collider = null;
	
	
	void Start()
	{
		_rb = gameObject.GetComponent<Rigidbody2D>();
		_animator = gameObject.GetComponent<Animator>();
		_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		_collider = gameObject.GetComponent<CapsuleCollider2D>();
		playerObject = gameObject;
	}


	private float mx;
	private void Update()
	{
		if(!canMove) return;
		mx = Input.GetAxisRaw("Horizontal");
		
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
		{
			Jump();
		}
		else if (isGrounded())
		{
			_animator.SetBool("isJumping", false);
		}

		if (Input.GetKey(KeyCode.S) && canCrouch)
		{
			mx *= 0.5f;
			_animator.SetBool("isCrouch", true);

			_collider.size = new Vector2(0.1f, 0.2f);
			_collider.offset = new Vector2(0.0f, -0.05f);
			
			if (mx == 0) _animator.Play("CrouchIdle");
			else _animator.Play("CrouchRun");
		}
		else if (Input.GetKeyUp(KeyCode.S))
		{
			_collider.size = new Vector2(0.1f, 0.32f);
			_collider.offset = new Vector2(0.0f, 0f);
			_animator.SetBool("isCrouch", false);
		}
		else if (!canCrouch)
		{
			_collider.size = new Vector2(0.1f, 0.32f);
			_collider.offset = new Vector2(0.0f, 0f);
			_animator.SetBool("isCrouch", false);
		}

		currentTransform = transform;
		side = (transform.right * mx).x < 0f ? Vector2.left : Vector2.right;
	}

	void FixedUpdate()
	{
		if(!canMove) return;
		if (mx == 0f)
		{
			_animator.SetBool("isRunning", false);
			return;
		}
		
		Vector2 movement = new Vector2(mx * moveSpeed, _rb.velocity.y);
		currentVelocity = movement;

		_rb.velocity = movement;

		_spriteRenderer.flipX = side == Vector2.left;
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
		Vector2 position = _rb.position;
		Vector2 direction = Vector2.down;
		float distance = 0.25f;
    
		RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayerMask);
		if (hit.collider != null) {
			_animator.SetBool("isJumping", true);
			return true;
		}
    
		return false;
	}
}
