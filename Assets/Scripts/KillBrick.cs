using UnityEngine;

public class KillBrick : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CheckpointManager.Instance.Respawn();
        }
    }
}