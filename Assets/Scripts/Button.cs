using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private float pressDepth = 0.1f;
    [SerializeField] private AudioClip buttonClip;
    [SerializeField] private float releaseDelay = 0.1f;

    private Vector3 upPosition;
    private Vector3 downPosition;
    private int touchingCount = 0;
    public bool isPressed = false;
    private float releaseTimer = 0f;

    void Start()
    {
        upPosition = transform.position;
        downPosition = upPosition + Vector3.down * pressDepth;
    }

    void Update()
    {
        if (touchingCount == 0 && isPressed)
        {
            releaseTimer += Time.deltaTime;
            if (releaseTimer >= releaseDelay)
            {
                isPressed = false;
                transform.position = upPosition;
            }
        }
        else
        {
            releaseTimer = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Cube")) return;

        touchingCount++;

        if (!isPressed)
        {
            isPressed = true;
            transform.position = downPosition;
            if (buttonClip != null) AudioManager.Instance.PlaySound(buttonClip);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Cube")) return;

        touchingCount--;
        if (touchingCount < 0) touchingCount = 0;
    }
}