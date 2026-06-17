using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI nameText;
    [SerializeField] private TypeWriterEffect typewriter;
    [SerializeField] private GameObject continueIndicator;
    [SerializeField] private GameObject dialogueBox;

    public Queue<string> sentences;
    private bool isTextRevealed = false;
    public bool dialogueActive = false;

    void Start()
    {
        sentences = new Queue<string>();
    }

    void OnEnable()
    {
        TypeWriterEffect.CompleteTextRevealed += OnTextRevealed;
    }

    void OnDisable()
    {
        TypeWriterEffect.CompleteTextRevealed -= OnTextRevealed;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueActive = true;
        dialogueBox.SetActive(true);
        sentences.Clear();
        Debug.Log("Starting dialogue");
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        HideContinueIndicator();
        isTextRevealed = false;
        string sentence = sentences.Dequeue();

        int periodIndex = sentence.IndexOf('.');
        if (periodIndex >= 0)
        {
            string speakerName = sentence.Substring(0, periodIndex).Trim();
            string actualSentence = sentence.Substring(periodIndex + 1).Trim();

            nameText.text = speakerName;
            typewriter.SetText(actualSentence);
        }
        else
        {
            typewriter.SetText(sentence);
        }
    }

    void EndDialogue()
    {
        dialogueActive = false;
        HideContinueIndicator();
        dialogueBox.SetActive(false);
        Debug.Log("End of Conversation.");
    }

    private void OnTextRevealed()
    {
        isTextRevealed = true;
        ShowContinueIndicator();
    }

    private void ShowContinueIndicator()
    {
        if (continueIndicator != null)
            continueIndicator.SetActive(true);
    }

    private void HideContinueIndicator()
    {
        if (continueIndicator != null)
            continueIndicator.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isTextRevealed)
            {
                typewriter.Skip();
            }
            else
            {
                DisplayNextSentence();
            }
        }
    }
}