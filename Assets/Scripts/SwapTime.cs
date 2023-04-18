using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapTime : MonoBehaviour
{
    public static SwapTime instance;
    
    public Image button;
    public Sprite nonPressed;
    public Sprite pressed;
    
    [SerializeField] public List<Times> times = new List<Times>();
    public int currentTime = 0;

    public GameObject timePicker;
    public GameObject selector;
    
    
    private void Start()
    {
        instance = this;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            timePicker.SetActive(true);
            StartCoroutine(pressButtonAnimation());
        }
    }


    public void Swap(int time)
    {
        StartCoroutine(swapObjects(times.FindAll(x => x != times[time]), times[time]));
        selector.transform.position = times[time].button.transform.position;
        timePicker.SetActive(false);
    }

    private IEnumerator pressButtonAnimation()
    {
        button.sprite = pressed; 
        yield return new WaitForSeconds(0.3f);
        button.sprite = nonPressed;
    }

    private IEnumerator swapObjects(List<Times> oldTimes, Times newTimes)
    {
        foreach (Times oldTime in oldTimes)
        {
            foreach (GameObject oldTimesObject in oldTime.objects)
            {
                oldTimesObject.SetActive(false);
                yield return new WaitForSeconds(0.1f);
            }
        }

        foreach (GameObject newTimesObject in newTimes.objects)
        {
            newTimesObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
        
    }
}

[Serializable]
public class Times
{
    [SerializeField] public GameObject button;
    [SerializeField] public List<GameObject> objects = new List<GameObject>();
    
}
