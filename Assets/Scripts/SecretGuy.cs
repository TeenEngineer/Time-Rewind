using UnityEngine;

public class SecretGuy : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private DialogueManager manager;
    [SerializeField] private DialogueTrigger trigger;
    [SerializeField] private DialogueTrigger trigger2;
    bool turnedAround = false;
    bool dialoguing = false;
    bool disappeared = false;
    void Start()
    {
        trigger = GetComponent<DialogueTrigger>();
        anim = GetComponent<Animator>();
    }

    public void walk()
    {
        anim.SetBool("walk", true);
        Invoke(nameof(TurnAroundPlayer), 2.1f);
    }

    void TurnAroundPlayer()
    {
        Object.FindAnyObjectByType<ProloguePlayer>().turnAround();
        turnedAround = true;
    }

    void Update()
    {
        if(turnedAround && !manager.dialogueActive)
        {
            turnedAround = false;
            Invoke(nameof(dialogue), 1f);
            dialoguing = true;
        }
        if(dialoguing && !manager.dialogueActive)
        {
            dialoguing = false;
            Invoke(nameof(disappear), 1f);
        }
        if(disappeared && !manager.dialogueActive)
        {
            disappeared = false;
            Invoke(nameof(dialogue2), 1f);
        }
    }

    void disappear()
    {
        disappeared = true;
    }

    void dialogue()
    {
        trigger.TriggerDialogue();
    }
    void dialogue2()
    {
        gameObject.SetActive(false);
        trigger2.TriggerDialogue();
        Object.FindAnyObjectByType<Car>().StartCheck();
    }
}
