using UnityEngine;
using UnityEngine.Events;

public class Activator : MonoBehaviour
{
    
    public bool activated;
    
    [SerializeField] Sprite activatedSprite;
    [SerializeField] Sprite deactivatedSprite;
    [SerializeField] UnityEvent activationAction;
    [SerializeField] UnityEvent deactivationAction;
    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        activated = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = activatedSprite;
        activationAction.Invoke();
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        activated = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = deactivatedSprite;
        deactivationAction.Invoke();
    }
}
