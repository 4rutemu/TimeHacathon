
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RevertTime : MonoBehaviour
{

    public Image button;
    public Sprite nonPressed;
    public Sprite pressed;
    
    public static int savedTransformsLimit = 100;
    public static List<TimeObject> revertedObjects = new List<TimeObject>();
    
    public static bool isReverting = false;
    
    private void Update()
    {
        
        if (!isReverting && Input.GetKeyDown(KeyCode.R))
        {
            button.sprite = pressed;
            Revert();
        }
    }

    private void Revert()
    {
        isReverting = true;
        foreach (TimeObject revertedObject in revertedObjects)
        {
            StartCoroutine(revertPositions(revertedObject));
        }

        isReverting = false;
    }

    private IEnumerator revertPositions(TimeObject revertedObject)
    {
        Animator animator = null;
        if (revertedObject.gameObject.tag.Equals("Player"))
        {
            animator = revertedObject.GetComponent<Animator>();
            animator.SetBool("isReverting", true);
            animator.Play("Revert");
        }
        foreach (Vector3 revertedObjectPosition in revertedObject.positions)
        {
            revertedObject.transform.position = revertedObjectPosition;
            yield return new WaitForSeconds(0.01f);
        }
        revertedObject.positions.Clear();
        if(animator != null) animator.SetBool("isReverting",false);
        button.sprite = nonPressed;
    }
}
