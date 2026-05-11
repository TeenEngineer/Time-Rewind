using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
    public AudioSource crashAudio;
    private Animator anim;
    public Friend friend;
    [SerializeField] private GameObject panel;
    [SerializeField] private Image panelImage;
    [SerializeField] private float fadeDuration = 4f; // how long the fade takes, in seconds
    public DialogueTrigger trigger;
    public DialogueTrigger trigger2;
    [SerializeField] private DialogueManager manager;
    bool dialogued = false;
    bool rewinded = false;
    bool checking = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        trigger = GetComponent<DialogueTrigger>();
    }

    public void Crash(bool rewindedd)
    {
        if (!rewindedd)
        {
            anim.SetBool("Crash", true);
            crashAudio.Play();
            Invoke(nameof(fall), 0.1f);
            panel.SetActive(true);
            Invoke(nameof(StartFade), 1.5f);
            Invoke(nameof(dialogue), 7f);
        }
        else
        {
            anim.SetBool("Crash2", true);
            crashAudio.Play();
            Invoke(nameof(dialogue2), 1.5f);
            rewinded = true;
        }
    }

    void dialogue2()
    {
        trigger2.TriggerDialogue();
        dialogued = true;
    }

    void dialogue()
    {
        trigger.TriggerDialogue();
        dialogued = true;
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
    void fall()
    {
        friend.Fall();
    }

    public void SetAlpha(float alpha)
    {
        Color c = panelImage.color;
        c.a = Mathf.Clamp01(alpha);
        panelImage.color = c;
    }

    void Update()
    {
        if(dialogued && !manager.dialogueActive && !rewinded)
        {
            UnFade();
            Invoke(nameof(RestartScene), 6);
            dialogued = false;
            Permanent.rewinded = true;
        }
        else if (dialogued && !manager.dialogueActive && rewinded)
        {
            friend.walkAway();
            Invoke(nameof(prologue), 2.5f);
            dialogued = false;
        }
        if(checking && !manager.dialogueActive)
        {
            GoToMain();
        }
    }

    void prologue()
    {
        Object.FindAnyObjectByType<ProloguePlayer>().dialogue();
    }

    public void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void StartCheck()
    {
        checking = true;
    }

    public void GoToMain()
    {
        panel.SetActive(true);
        UnFade();
        Invoke(nameof(ToMain), 4f);
    }

    void ToMain()
    {
        SceneManager.LoadScene("Main");
    }
}
