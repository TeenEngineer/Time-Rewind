using UnityEngine;
using System.Collections.Generic;


public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    
    public void TriggerDialogue()
    {
        Object.FindAnyObjectByType<DialogueManager>().StartDialogue(dialogue);
    }

}
