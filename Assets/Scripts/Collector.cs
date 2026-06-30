using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private DialogueTrigger trigger;
    [SerializeField] private DialogueManager manager;
    [SerializeField] private float interactionRange = 2f;
    private bool dialoguing = false;
    private bool isOnce = true;

    private Transform player;
    private bool playerInRange = false;
    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        bool inRange = distance <= interactionRange;

        // Show/hide prompt only when state changes (avoids setting active every frame)
        if (inRange != playerInRange && isOnce)
        {
            playerInRange = inRange;
            if(playerInRange) trigger.TriggerDialogue();
            Debug.Log("in range");
        }
        if (manager.dialogueActive && isOnce)
        {
            dialoguing = true;
            isOnce = false;
        }
        if (!manager.dialogueActive && dialoguing)
        {
            dialoguing = false;
        }
    }
}
