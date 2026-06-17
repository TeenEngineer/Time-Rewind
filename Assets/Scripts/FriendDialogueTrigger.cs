using UnityEngine;

public class FriendDialogueTrigger : MonoBehaviour
{
    public DialogueTrigger trigger;
    public DialogueTrigger trigger2;
    bool triggered = false;
    public DialogueManager dialogueManager;
    public Car car;
    bool crashed = false;
    bool rewinded = false;

    void Start()
    {
        trigger = GetComponent<DialogueTrigger>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;
        Invoke(nameof(Trigger), 1.5f);
    }

    void Trigger()
    {
        if (!Permanent.rewinded) trigger.TriggerDialogue();
        else
        {
            rewinded = true;
            trigger2.TriggerDialogue();
        }
        triggered = true;
    }

    void Update()
    {
        if (triggered && !dialogueManager.dialogueActive && !crashed && !rewinded)
        {
            car.Crash(false);
            crashed = true;
        }
        else if (triggered && !dialogueManager.dialogueActive && !crashed && rewinded)
        {
            Object.FindAnyObjectByType<Friend>().GoAway();
            car.Crash(true);
            crashed = true;
        }
    }
}
