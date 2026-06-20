using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RewindManager : MonoBehaviour
{
    public static RewindManager Instance { get; private set; }

    private static List<RewindObject> rewindObjects = new List<RewindObject>();
    public static bool IsRewinding { get; private set; }

    [Header("NoRewind text")]

    [SerializeField] private GameObject target;
    [SerializeField] private float duration = 5f;
    [SerializeField] private AudioClip clip;

    void Awake()
    {
        if (Instance == null) Instance = this;
        if (target != null) target.SetActive(false); // start hidden
        else Destroy(gameObject);
    }

    public static void Register(RewindObject obj)
    {
        if (!rewindObjects.Contains(obj)) rewindObjects.Add(obj);
    }

    public static void Unregister(RewindObject obj)
    {
        rewindObjects.Remove(obj);
    }

    public void StartRewindAll()
    {
        IsRewinding = true;
        foreach (var obj in rewindObjects)
        {
            obj.StartRewind();
        }
    }

    public void StopRewindAll()
    {
        IsRewinding = false;
        foreach (var obj in rewindObjects)
        {
            obj.StopRewind();
        }
    }

    void Update()
    {
        /*string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "Main")
        {
            if (Input.GetKeyDown(KeyCode.R)) StartRewindAll();
            if (Input.GetKeyUp(KeyCode.R)) StopRewindAll();
            if (Input.GetKeyDown(KeyCode.R)) Appear();
        }*/
    }

    public void Appear()
    {
        if (target != null)
        {
            StopAllCoroutines(); // prevent overlap if called multiple times
            AudioManager.Instance.PlaySound(clip);
            StartCoroutine(AppearRoutine());
        }
    }

    IEnumerator AppearRoutine()
    {
        target.SetActive(true);
        yield return new WaitForSeconds(duration);
        target.SetActive(false);
    }
}