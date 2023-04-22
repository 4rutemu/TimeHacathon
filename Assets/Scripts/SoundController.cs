using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public GameObject audioSourcePrefab;
    public AudioClip walk;
    public AudioClip jump;
    public AudioClip hit;
    public AudioClip attack;
    public AudioClip crouch;


    public List<AudioSource> audioSources = new List<AudioSource>();
    public void play(AudioClip clip)
    {
        GameObject obj = Instantiate(audioSourcePrefab, transform.position, Quaternion.identity);
        AudioSource source = obj.GetComponent<AudioSource>();
        audioSources.Add(source);
        
        source.clip = clip;
        source.Play();
        
        StartCoroutine(remove(source));
    }

    private IEnumerator remove(AudioSource obj)
    {
        yield return new WaitWhile(() => obj.isPlaying);
        audioSources.Remove(obj);
        Destroy(obj.gameObject);
    }
}
