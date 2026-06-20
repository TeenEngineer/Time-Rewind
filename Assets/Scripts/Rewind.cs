using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Rewind : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    private bool rewinding = false;
    private bool allowedToRewind = false;

    [Header("NoRewind text")]

    [SerializeField] private GameObject target;
    [SerializeField] private float duration = 5f;
    [SerializeField] private AudioClip textClip;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && allowedToRewind && !rewinding)
        {
            rewinding = true;
            allowedToRewind = false;
            AudioManager.Instance.PlaySound(clip);
            Invoke("Rewinder", 1.23f);
        }
        else if (Input.GetKeyDown(KeyCode.R) && !allowedToRewind)
        {
            Appear();
        }
    }

    void Rewinder()
    {
        CheckpointManager.Instance.Respawn();
        rewinding = false;
    }

    public void Appear()
    {
        if (target != null)
        {
            StopAllCoroutines(); // prevent overlap if called multiple times
            AudioManager.Instance.PlaySound(textClip);
            StartCoroutine(AppearRoutine());
        }
    }

    IEnumerator AppearRoutine()
    {
        target.SetActive(true);
        yield return new WaitForSeconds(duration);
        target.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("AllowRewind"))
        {
            allowedToRewind = true;
        }
    }
}
