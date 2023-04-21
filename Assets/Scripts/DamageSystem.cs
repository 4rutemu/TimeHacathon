using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageSystem : MonoBehaviour
{
    public int health = 3;
    public bool canDamageble = true;
    public TextMeshProUGUI healthText;
    
    private Animator _animator;

    public void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Damage()
    {
        if(!canDamageble) return;
        string currentAnimation = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        
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
        canDamageble = false;
        _animator.Play("Death");
        
        StartCoroutine(restartLevel());
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
            }

            SceneManager.LoadScene("Level");
            CharacterMovement.canMove = true;
        }
        
        RevertTime.isReverting = false;
    }
}
