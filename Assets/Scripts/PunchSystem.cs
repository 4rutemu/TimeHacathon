using UnityEngine;

public class PunchSystem : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private SoundController _soundController;
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _soundController = gameObject.GetComponent<SoundController>();
    }

    private void Update()
    {
        if(SwapTime.instance.timePicker.activeSelf) return;
        if (Input.GetMouseButtonDown(0) && Input.GetAxisRaw("Horizontal") == 0)
        {
            _animator.Play("Punch");
            _soundController.play(_soundController.attack);
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
