using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prolog : MonoBehaviour
{
    public GameObject button;
    
    public TextMeshProUGUI text;
    public string replica;


    public void Start()
    {
        StartCoroutine(StartMoveText());
    }

    public void game()
    {
        SceneManager.LoadScene("MainLVL");
    }
    

    IEnumerator StartMoveText()
    {
        StartCoroutine(PrintText());
        yield return new WaitForSeconds(19f);
        text.pageToDisplay = 2;
        yield return new WaitForSeconds(20f);
        text.pageToDisplay = 3;
        yield return new WaitForSeconds(19f);
        button.SetActive(true);
    }

    IEnumerator PrintText()
    {
        for (int i = 0; i < replica.Length; i++)
        {
            text.text += replica[i];
            yield return new WaitForSeconds(0.07f);
        }
    }
}
