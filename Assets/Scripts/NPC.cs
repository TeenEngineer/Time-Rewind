using UnityEngine;
using static Unity.VisualScripting.Member;

public class NPC : MonoBehaviour
{
    [SerializeField] private GameObject promptUI;
    [SerializeField] private float interactionRange = 2f;
    [SerializeField] private KeyCode interactionKey = KeyCode.F;
    [SerializeField] private DialogueTrigger dialogueTrigger;
    [SerializeField] private DialogueManager dialogueManager;

    private Transform player;
    private bool playerInRange = false;
    

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;

        if (promptUI != null) promptUI.SetActive(false);

        dialogueManager = GameObject.FindAnyObjectByType<DialogueManager>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        bool inRange = distance <= interactionRange;

        // Show/hide prompt only when state changes (avoids setting active every frame)
        if (inRange != playerInRange)
        {
            playerInRange = inRange;
            if (promptUI != null) promptUI.SetActive(playerInRange);
        }

        // Trigger interaction when player presses key while in range
        if (playerInRange && Input.GetKeyDown(interactionKey) && !dialogueManager.dialogueActive)
        {
            if (dialogueTrigger != null) dialogueTrigger.TriggerDialogue();
            if (promptUI != null) promptUI.SetActive(false); // hide prompt during dialogue
        }
        else if(playerInRange && dialogueManager.dialogueActive)
        {
            if (promptUI != null) promptUI.SetActive(false); // hide prompt during dialogue
        }
    }
}