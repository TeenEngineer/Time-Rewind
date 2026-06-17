using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;
    public GameObject respawnPoint;
    public GameObject player;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void Respawn()
    {
        if (player != null && respawnPoint != null)
        {
            player.transform.position = respawnPoint.transform.position;

            // Reset velocity so the player doesn't keep falling
            Rigidbody2D body = player.GetComponent<Rigidbody2D>();
            if (body != null) body.linearVelocity = Vector2.zero;
        }
    }
}