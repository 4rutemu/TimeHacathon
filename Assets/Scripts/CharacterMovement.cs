using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	public static Transform currentTransform;
	public static Vector2 side;
	public static Vector2 currentVelocity;
	public static GameObject playerObject;
	
	public float moveSpeed = 5f;
	public float jumpForce = 20f;
	public float rollForce = 5f;
	public LayerMask groundLayerMask;


	private Rigidbody2D _rb;
	private Animator _animator;
	private SpriteRenderer _spriteRenderer;
	
	
	void Start()
	{
		_rb = gameObject.GetComponent<Rigidbody2D>();
		_animator = gameObject.GetComponent<Animator>();
		_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		playerObject = gameObject;
	}


	private float mx;
	private void Update()
	{
		mx = Input.GetAxisRaw("Horizontal");
		
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
		{
			Jump();
		}
		else if (isGrounded())
		{
			_animator.SetBool("isJumping", false);
		}

		currentTransform = transform;
		side = (transform.right * mx).x < 0f ? Vector2.left : Vector2.right;
	}

	void FixedUpdate()
	{
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
