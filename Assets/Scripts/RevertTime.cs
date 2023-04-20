
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RevertTime : MonoBehaviour
{
    public Image entityButton;
    public Sprite entityNonPressed;
    public Sprite entityPressed;
    
    public Image objectsButton;
    public Sprite objectsNonPressed;
    public Sprite objectsPressed;
    
    public static int savedTransformsLimit = 100;
    public static List<TimeObject> revertedObjects = new List<TimeObject>();
    public static List<TimeObject> destroyedObjects = new List<TimeObject>();
     
    public static bool isReverting = false;
    
    private void Update()
    {
        
        if (!isReverting && Input.GetKeyDown(KeyCode.R))
        {
            entityButton.sprite = entityPressed;
            Revert("Entity", entityButton, entityNonPressed);
        }
        if (!isReverting && Input.GetKeyDown(KeyCode.E))
        {
            objectsButton.sprite = objectsPressed;
            Revert("Object", objectsButton, objectsNonPressed);
        }
    }

    private void Revert(string tag, Image button, Sprite nonPressed)
    {
        isReverting = true;
        foreach (TimeObject revertedObject in revertedObjects.FindAll(obj => obj.CompareTag(tag) || (tag == "Entity" && obj.CompareTag("Player"))))
        {
            StartCoroutine(revertPositions(revertedObject, button, nonPressed));
        }
        
        foreach (TimeObject destroyedObject in destroyedObjects.FindAll(obj => obj.CompareTag(tag)))
        {
            destroyedObject.Restore();
        }
        destroyedObjects.RemoveAll(obj => obj.CompareTag(tag));

        isReverting = false;
    }

    private IEnumerator revertPositions(TimeObject revertedObject, Image button, Sprite nonPressed)
    {
        Animator animator = null;
        if (revertedObject.gameObject.tag.Equals("Player"))
        {
            animator = revertedObject.GetComponent<Animator>();
            animator.SetBool("isReverting", true);
            animator.Play("Revert");
            
            foreach (Vector3 revertedObjectPosition in revertedObject.positions.GetRange(0, 100))
            {
                revertedObject.transform.position = revertedObjectPosition;
                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            foreach (Vector3 revertedObjectPosition in revertedObject.positions)
            {
                revertedObject.transform.position = revertedObjectPosition;
                yield return new WaitForSeconds(0.01f);
            } 
        }
        revertedObject.positions.Clear();
        if(animator != null) animator.SetBool("isReverting",false);
        button.sprite = nonPressed;
    }
}
