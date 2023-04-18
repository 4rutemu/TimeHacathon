using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class TimeObject : MonoBehaviour
{
    [HideInInspector] public List<Vector3> positions = new List<Vector3>();
    [SerializeField] private ParticleSystem particlesPrefab;


    private bool nonRecord = false;
    void Start()
    {
        RevertTime.revertedObjects.Add(this);
        
        if(particlesPrefab == null) return;
    }
    
    void FixedUpdate()
    {   
        if(RevertTime.isReverting) return;
        if(nonRecord) return;
        if(positions.Contains(transform.position)) return;
        if (positions.Count >= RevertTime.savedTransformsLimit)
        {
            positions.RemoveAt(99);
        }
        positions.Insert(0, transform.position);
    }

    public void DestroyTimeObject()
    {
        ParticleSystem particles = Instantiate(particlesPrefab, gameObject.transform.position, Quaternion.identity);
        particles.Play();
        
        RevertTime.destroyedObjects.Add(this);
        
        StartCoroutine(TeleportObject());
        StartCoroutine(RemoveParticles(particles));
        StartCoroutine(RemoveFromList());
        StartCoroutine(RemoveObject());
        nonRecord = true;
    }

    private void OnDestroy()
    {
        RevertTime.revertedObjects.Remove(this);
        foreach (Times times in SwapTime.instance.times.FindAll(x => x.objects.Contains(gameObject)))
        {
            times.objects.Remove(gameObject);
        }
    }

    public void Restore()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, y, gameObject.transform.position.z);
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        nonRecord = false;
    }

    float y = 0;
    private IEnumerator TeleportObject()
    {
        y = gameObject.transform.position.y;
        yield return new WaitForSeconds(0.3f);
        
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 100000f, gameObject.transform.position.z);
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

    private IEnumerator RemoveObject()
    {
        yield return new WaitForSeconds(10.1f);
        if(!RevertTime.destroyedObjects.Contains(this)) Destroy(gameObject);
    }
    
    private IEnumerator RemoveParticles(ParticleSystem particleSystem)
    {
        yield return new WaitForSeconds(1f);
        Destroy(particleSystem.gameObject);
    }

    private IEnumerator RemoveFromList()
    {
        yield return new WaitForSeconds(10f);
        RevertTime.destroyedObjects.Remove(this);
    }
}
