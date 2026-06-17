using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RewindManager : MonoBehaviour
{
    public static RewindManager Instance { get; private set; }

    private static List<RewindObject> rewindObjects = new List<RewindObject>();
    public static bool IsRewinding { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;
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
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "Main")
        {
            /*if (Input.GetKeyDown(KeyCode.R)) StartRewindAll();
            if (Input.GetKeyUp(KeyCode.R)) StopRewindAll();*/
        }
    }
}