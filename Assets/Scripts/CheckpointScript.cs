using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CheckpointManager.Instance.respawnPoint = this.gameObject;
            Debug.Log("Checkpoint set: " + gameObject.name);
        }
    }
}