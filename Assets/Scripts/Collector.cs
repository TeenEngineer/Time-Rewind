using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Collector : MonoBehaviour
{
    [SerializeField] private DialogueTrigger trigger;
    [SerializeField] private DialogueManager manager;
    [SerializeField] private float interactionRange = 2f;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private AudioClip clip;
    private bool dialoguing = false;
    private bool isOnce = true;

    private Transform player;
    private bool playerInRange = false;

    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image panelImage;
    [SerializeField] private float fadeDuration = 6.4f; // how long the fade takes, in seconds
    [SerializeField] private float fadeDurationText = 2f; // how long the fade takes, in seconds
    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    public void SetAlpha(float alpha)
    {
        Color c = panelImage.color;
        c.a = Mathf.Clamp01(alpha);
        panelImage.color = c;
    }

    void StartFade()
    {
        StartCoroutine(FadePanel());
    }

    IEnumerator FadePanel()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            SetAlpha(alpha);
            yield return null;
        }
        SetAlpha(0f);
    }

    void UnFade()
    {
        StartCoroutine(UnFadePanel());
    }

    IEnumerator UnFadePanel()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            SetAlpha(alpha);
            yield return null;
        }
        SetAlpha(1f);
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
            movement.canMove = false;
            movement.body.linearVelocity = new Vector2(0f, movement.body.linearVelocity.y);
            movement.anim.SetBool("Run", false);
            movement.anim.SetBool("Walk", false);
            movement.anim.SetBool("Jump", false);
        }
        if (inRange && manager.dialogueActive && isOnce)
        {
            dialoguing = true;
            isOnce = false;
        }
        if (!manager.dialogueActive && dialoguing)
        {
            dialoguing = false;
            AudioManager.Instance.PlaySound(clip);
            UnFade();
            Invoke(nameof(FadeIn), 8.5f);
        }
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInRoutine());
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutRoutine());
    }

    IEnumerator FadeInRoutine()
    {
        float t = 0f;
        Color c = text.color;
        c.a = 0f;
        text.color = c;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0f, 1f, t / fadeDurationText);
            text.color = c;
            yield return null;
        }

        c.a = 1f;
        text.color = c;
    }

    IEnumerator FadeOutRoutine()
    {
        float t = 0f;
        Color c = text.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(1f, 0f, t / fadeDurationText);
            text.color = c;
            yield return null;
        }

        c.a = 0f;
        text.color = c;
    }
}
