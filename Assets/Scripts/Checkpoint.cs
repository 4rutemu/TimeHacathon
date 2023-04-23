using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Checkpoint lastCheckpoint;

    private bool isUsed = false;
    public Vector3 playerTransformWhileSave;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(isUsed) return;
        if(!col.CompareTag("Player")) return;

        playerTransformWhileSave = col.transform.position;
        lastCheckpoint = this;
        isUsed = true;
    }
}
