using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string replica;


    public void Start()
    {
        StartCoroutine(StartMoveText());
    }
    

    IEnumerator StartMoveText()
    {
        StartCoroutine(PrintText());
        yield return new WaitForSeconds(22.5f);
        SceneManager.LoadScene("Start");
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
