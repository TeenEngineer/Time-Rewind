using UnityEngine;

public class ShowKey : MonoBehaviour
{
    [SerializeField] private GameObject promptUI;
    [SerializeField] private float interactionRange = 2f;

    private Transform player;
    private bool playerInRange = false;
    public static ShowKey Instance;
    public bool disabled;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;

        if (promptUI != null) promptUI.SetActive(false);
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        bool inRange = distance <= interactionRange;

        // Show/hide prompt only when state changes (avoids setting active every frame)
        if (inRange != playerInRange && !disabled)
        {
            playerInRange = inRange;
            if (promptUI != null) promptUI.SetActive(playerInRange);
        }
        if(disabled)
        {
            promptUI.SetActive(false);
        }
    }
}
