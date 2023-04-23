using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DamageSystem : MonoBehaviour
{
    public int health = 3;
    public bool canDamageble = true;
    public TextMeshProUGUI healthText;
    
    private Animator _animator;
    public AudioSource audioSource;
    
    public Image button;
    public Sprite nonPressed;
    public Sprite pressed;
    
    private SoundController _soundController;
    public void Start()
    {
        _animator = GetComponent<Animator>();
        _soundController = gameObject.GetComponent<SoundController>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Death();
        }
    }

    public void Damage()
    {
        if(!canDamageble) return;
        string currentAnimation = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        _soundController.play(_soundController.hit);
        
        _animator.Play("Hurt");
        health -= 1;
        healthText.text = health.ToString();

        if(health <= 0)
        {
            Death();
            return;
        }

        if(currentAnimation != "Hurt") StartCoroutine(playLastAnim(currentAnimation));
    }

    private IEnumerator playLastAnim(string anim)
    {
        yield return new WaitForSeconds(0.5f);
        _animator.Play(anim);
    }

    public void Death()
    {
        button.sprite = pressed;
        audioSource.pitch = -3f;
        canDamageble = false;
        _animator.Play("Death");
        
        StartCoroutine(restartLevel());
        button.sprite = nonPressed;
    }
    
    private IEnumerator restartLevel()
    {
        yield return new WaitForSeconds(1);
        _animator.Play("Revert");
        _animator.SetBool("isReverting",true);
        CharacterMovement.canMove = false;
        TimeObject timeObject = gameObject.GetComponent<TimeObject>();
        RevertTime.isReverting = true;

        if (Checkpoint.lastCheckpoint != null)
        {
            Vector3 pos = Checkpoint.lastCheckpoint.playerTransformWhileSave;
            int i = 0;
            foreach (Vector3 position in timeObject.positions)
            {
                gameObject.transform.position = position;
                yield return new WaitForSeconds(0.01f);
                if(position.x <= pos.x + 0.15 && position.x >= pos.x - 0.15 &&
                   position.y <= pos.y + 0.15 && position.y >= pos.y - 0.15) break;
                i++;
                audioSource.pitch += 0.01f;
            }
            CharacterMovement.canMove = true;
            canDamageble = true;
            _animator.SetBool("isReverting",false);
            
            health = 3;
            healthText.text = health.ToString();
            
            timeObject.positions.RemoveRange(0, i);
            Checkpoint.lastCheckpoint = null;
        }
        else
        {
            foreach (Vector3 position in timeObject.positions)
            {
                gameObject.transform.position = position;
                yield return new WaitForSeconds(0.01f);
                audioSource.pitch += 0.01f;
            }

            SceneManager.LoadScene("MainLVL");
            CharacterMovement.canMove = true;
        }
        
        RevertTime.isReverting = false;
        audioSource.pitch = 1f;
    }
}
