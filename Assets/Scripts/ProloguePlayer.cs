using UnityEngine;

public class ProloguePlayer : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private DialogueTrigger trigger;
    [SerializeField] private DialogueManager manager;
    bool dialogue1 = false;

    public void dialogue()
    {
        trigger.TriggerDialogue();
        dialogue1 = true;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        trigger = GetComponent<DialogueTrigger>();
        Invoke(nameof(Idle), 2.5f);
    }

    // Update is called once per frame
    void Idle()
    {
        animator.SetBool("Idle", true);
    }

    public void turnAround()
    {
        Vector3 scale = transform.localScale;
        scale.x = -scale.x;
        transform.localScale = scale;
    }

    void Update()
    {
        if(dialogue1 && !manager.dialogueActive)
        {
            Object.FindAnyObjectByType<SecretGuy>().walk();
            dialogue1 = false;
        }
    }
}
