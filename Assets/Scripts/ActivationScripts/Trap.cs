using UnityEngine;

public class Trap : MonoBehaviour
{
    public bool isActive = true;
    public Collider2D collider;

    private Sprite startSprite;

    private void Start()
    {
        startSprite = GetComponent<SpriteRenderer>().sprite;
    }

    public void SwitchActivation()
    {
        if (isActive)
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = startSprite;
            isActive = false;
        }
        else
        {
            GetComponent<Animator>().enabled = true;
            isActive = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!isActive) return;
        if(collider == null) return;
        if (!col.rigidbody.CompareTag("Player")) return;
        DamageSystem damageSystem = col.rigidbody.GetComponent<DamageSystem>();

        damageSystem.Damage();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!isActive) return;
        if(collider == null) return;
        if (!col.CompareTag("Player")) return;
        DamageSystem damageSystem = col.GetComponent<DamageSystem>();

        damageSystem.Damage();
    }
}
